using CodebustersAppWMU2.Services;
using System;
using Windows.UI.Xaml.Controls;
using CodebustersAppWMU2.Models;


namespace CodebustersAppWMU2
{
    
    public sealed partial class MainPage : Page
    {
      
        public MainPage()
        {
            this.InitializeComponent();

             Test();
            
        }

        public async void Test(){

            RequestHelper client = new RequestHelper();
            //string assigned;

            var listTask = await client.GetRequest<TaskDto>("tasks");
            
                foreach (var item in listTask)
                {
                //var assignment = await client.GetRequest<TaskDto>("assignments/" + item.TaskId);

                //    var task = new TaskResponsibility()
                //    {
                //        Task = item
                //    };
                //if (assignment.Any())
                //{
                //    task.Assigned = "Assigned";
                //}

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
