using CodebustersAppWMU2.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Newtonsoft.Json;
using System.Collections;

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


            var listTask = await client.GetRequest<TaskDto>("tasks");
            
                foreach (var item in listTask)
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

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }
    }
}
