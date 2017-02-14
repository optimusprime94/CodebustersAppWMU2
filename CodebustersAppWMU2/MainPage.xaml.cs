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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CodebustersAppWMU2
{
    
    public sealed partial class MainPage : Page
    {
        private List<Task> _tasks = new List<Task>();
        public MainPage()
        {
            this.InitializeComponent();

            _tasks.Add(new Task { TaskName = "Create The UWP app", Responsible = "Elvir Dzeko"});
            _tasks.Add(new Task { TaskName = "Do something else"});
            _tasks.Add(new Task { TaskName = "Design the Background", Responsible = "Elvir Dzeko"});
            _tasks.Add(new Task { TaskName= "Fix the website", Responsible = "Elvir Dzeko"});

            Tasklist.ItemsSource = _tasks;
        }
    }
}
