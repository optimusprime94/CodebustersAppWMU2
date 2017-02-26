using CodebustersAppWMU2.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

                    Tasklist.Items.Add(new TaskResponsibilityDto()
                    {
                        TaskId = item.TaskId,
                        Assigned = hasResponsible,
                        BeginDateTime = "Start:" + item.BeginDateTime.Substring(0, 10),
                        DeadlineDateTime = "Deadline:" + item.DeadlineDateTime.Substring(0, 10),
                        Requirements = item.Requirements,
                        Title = item.Title
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
    }
}
