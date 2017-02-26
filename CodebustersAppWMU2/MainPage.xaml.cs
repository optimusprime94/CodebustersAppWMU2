using CodebustersAppWMU2.Services;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
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

        public async void PopulateTasks(){

            RequestHelper client = new RequestHelper();

            _taskDtos = await client.GetRequest<TaskDto>("tasks");
            
                foreach (var item in _taskDtos)
                {

                if (item != null)
                    {
                        Tasklist.Items.Add(item);
                        Tasklist.ItemClick += Tasklist_ItemClick;
                    }
                    
                }
            }

        private void Tasklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            /* We pass the values from the clicked task to the next page */
            TaskDto item = (TaskDto)e.ClickedItem;
            this.Frame.Navigate(typeof(Details), item);

        }
    }
}
