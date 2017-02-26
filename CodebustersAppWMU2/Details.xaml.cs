using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        private readonly RequestHelper _client = new RequestHelper();
        private IEnumerable<AssignmentDto> _thisTaskAssignments;
        private TaskDto _detailsTask;
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
                _detailsTask = (TaskDto)e.Parameter;

                TaskDetail.Text = _detailsTask.Title;
                Description.Text = _detailsTask.Requirements;
                Startdate.Text = _detailsTask.BeginDateTime;
                Deadline.Text = _detailsTask.DeadlineDateTime;
                GetResponsible(_detailsTask);
            }
            base.OnNavigatedTo(e);
        }
        public async void GetResponsible(TaskDto task)
        {

            string assignment = "assignments/", user = "users", name = "";
            _thisTaskAssignments = await _client.GetRequest<AssignmentDto>(assignment + _detailsTask.TaskId);
            var listuser = await _client.GetRequest<UserDto>(user);
            var userDtos = listuser as IList<UserDto> ?? listuser.ToList();
            foreach (var item in _thisTaskAssignments)
            {
                foreach (var items in userDtos)
                {
                    if (task.TaskId == item.TaskId)
                    {
                        if (item.UserId == items.UserId)
                        {

                            name = String.Concat(name, items.FirstName + " " + items.LastName + "\n");

                        }
                    }
                }
            }
            Owner.Text = name;
        }

        private async void ComboBoxSeed()
        {
            var users = await _client.GetRequest<UserDto>("users");

            foreach (var user in users)
            {
                AssingmentBox.Items.Add(user);
            }
        }
        private async void Resign(AssignmentDto assignmentToHandle)
        {

            var status =
                await _client.DeleteRequest<AssignmentDto>(
                    "assignments/" + assignmentToHandle.TaskId + "/" + assignmentToHandle.UserId, assignmentToHandle);
            this.Frame.Navigate(typeof(Details), _detailsTask);

        }


        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDto user = (UserDto)this.AssingmentBox.SelectedItem;

            if (user == null)
            {
                return;
            }
            // We create the assignment by combining the taskId and userId.
            var assignmentToHandle = new AssignmentDto()
            {
                TaskId = _detailsTask.TaskId,
                UserId = user.UserId
            };
            // Use FirstOrDefault instead of First, because then we get a null if the list is empty
            // instead of an exception.
            var value = _thisTaskAssignments.FirstOrDefault(a => a.UserId == user.UserId);

            // We don't do anything if the assignment doesn't exist.
            if (value == null)
            {
                return;
            }
            else
            {
                Resign(assignmentToHandle);
            }
        }

        private void AssignBtn_OnClick_Click(object sender, RoutedEventArgs e)
        {
            UserDto user = (UserDto)this.AssingmentBox.SelectedItem;

            if (user == null)
            {
                return;
            }
            // We create the new assignment by combining the taskId and userId.
            var assignmentToHandle = new AssignmentDto()
            {
                TaskId = _detailsTask.TaskId,
                UserId = user.UserId
            };

            var value = _thisTaskAssignments.FirstOrDefault(a => a.UserId == user.UserId);

            // If the assignment already exists, we don't do anything.
            if (value != null)
            {
                return;
            }
            else
            {
                Assign(assignmentToHandle);
            }
        }

        private async void Assign(AssignmentDto assignmentToHandle)
        {
            // The await operator is applied to a task in an asynchronous method to suspend the execution 
            // of the method until the awaited task completes.The task represents ongoing work.
           var status = await _client.AssignRequest<AssignmentDto>("assignments/create", assignmentToHandle);
           this.Frame.Navigate(typeof(Details), _detailsTask);
        }

    }
}

