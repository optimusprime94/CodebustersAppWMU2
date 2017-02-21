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

namespace CodebustersAppWMU2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        public Details()
        {
            this.InitializeComponent();
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
                Startdate.Text = task.BeginDateTime;
                Deadline.Text = task.DeadlineDateTime;
            }
            base.OnNavigatedTo(e);
        }
    }
}
