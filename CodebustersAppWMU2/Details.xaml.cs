using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CodebustersAppWMU2.Models;
using CodebustersAppWMU2.Services;

namespace CodebustersAppWMU2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        RequestHelper client = new RequestHelper();
        public Details()
        {
            
            this.InitializeComponent();
            ComboBoxSeed();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            /* We pass the task we clicked on to this action, then we check if the
             * the parameter passed is null, if not we pass the values to the UI. */
            if (e.Parameter != null)
            {
                TaskDto task = (TaskDto)e.Parameter;

                TaskDetail.Text = task.Title;
                Description.Text = task.Requirements;
                Startdate.Text = task.BeginDateTime.Substring(0, 10);
                Deadline.Text = task.DeadlineDateTime.Substring(0, 10);
                RequestTest(task);
            }
            base.OnNavigatedTo(e);
        }
        public async void RequestTest(TaskDto task)
        {
            
            string assignment = "assignments", user = "users", name = "";
            var listassignment = await client.GetRequest<AssignmentDto>(assignment);
            var listuser = await client.GetRequest<UserDto>(user);
            foreach (var item in listassignment)
            {
                foreach (var items in listuser)
                {
                    if (task.TaskId == item.TaskId)
                    {
                        if (item.UserId == items.UserId)
                        {

                            name = String.Concat(name, items.FirstName + " " + items.LastName + ", ");

                        }
                    }
                }
            }
            Owner.Text = name;
            //if (listuser != null)
            //{
            //    var test = listuser.First(B => listassignment.Equals(B.UserId));

            //    Owner.Text = test.ToString();
            //}
        }

        private async void ComboBoxSeed()
        {
            var users = await client.GetRequest<UserDto>("users");

            foreach (var user in users)
            {
                AssingmentBox.Items.Add(user);
            }
            

        }
    }
}

