using CodebustersAppWMU2.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CodebustersAppWMU2.Models;


namespace CodebustersAppWMU2
{

    public sealed partial class MainPage : Page
    {
        private IEnumerable<TaskDto> _taskDtos;
        public MainPage()
        {
            this.InitializeComponent();
            PopulateTasks();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

        }

        public async void PopulateTasks()
        {

            RequestHelper client = new RequestHelper();

            _taskDtos = await client.GetRequest<TaskDto>("tasks");
            if (_taskDtos.ToList().Any() && !_taskDtos.Equals(App.GlobalTaskDtos))
            {
                ResetPageCache();
                App.GlobalTaskDtos = _taskDtos;
                
            }

            _taskDtos = App.GlobalTaskDtos;
            foreach (var item in _taskDtos)
            {

                if (item != null)
                {
                    var assignment = await client.GetRequest<AssignmentDto>("assignments/" + item.TaskId);

                    var hasResponsible = assignment.Any() ? "Assigned" : "Not assigned";

                    int start, end;
                    string color;
                    LineProperties(item.BeginDateTime, item.DeadlineDateTime, out start, out end, out color);


                    Tasklist.Items.Add(new TaskResponsibilityDto()
                    {
                        TaskId = item.TaskId,
                        Assigned = hasResponsible,
                        BeginDateTime = item.BeginDateTime.Substring(0, 10),
                        DeadlineDateTime = item.DeadlineDateTime.Substring(0, 10),
                        Requirements = item.Requirements,
                        Title = item.Title,
                        From = new Point(start, 15), // Change later
                        To = new Point(end, 15),
                        DurationColor = color
                        
                    });
                    Tasklist.ItemClick += Tasklist_ItemClick;
                }
            }
        }

        private void Tasklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* We pass the values from the clicked task to the next page */
            var item = (TaskResponsibilityDto)e.ClickedItem;

            var taskDto = new TaskDto()
            {
                TaskId = item.TaskId,
                BeginDateTime = item.BeginDateTime,
                DeadlineDateTime = item.DeadlineDateTime,
                Requirements = item.Requirements,
                Title = item.Title
            };
            this.Frame.Navigate(typeof(Details), taskDto);

        }

        private void ResetPageCache()
        {
            var cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;
        }

        private static void LineProperties(string startdate, string deadline, out int start, out int end, out string color)
        {
            // Parsing out the date into day, month and year integers.
            int sDay = Int32.Parse(startdate.Substring(8, 2));
            int dDay = Int32.Parse(deadline.Substring(8, 2));
            int sMonth = Int32.Parse(startdate.Substring(5, 2));
            int dMonth = Int32.Parse(deadline.Substring(5, 2));
            int sYear = Int32.Parse(startdate.Substring(0, 4));
            int dYear = Int32.Parse(deadline.Substring(0, 4));

            // Calculation for appropriate point coordinates.
            start = (sMonth * 100) + (sDay);
            end = (dMonth * 100) + (dDay);

            if (dYear - sYear > 0)
            {
                color = "Red";
            }
            else if (end - start > 100)
            {
                color = "Orange";
            }
            else
            {
                color = "Green";
            }
        }
    }
}
