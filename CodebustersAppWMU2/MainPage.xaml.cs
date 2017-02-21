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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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

            HttpClient client = TaskManagerHttpClient.GetClient();
            
            HttpResponseMessage response = await client.GetAsync("api/tasks");

            if (response.IsSuccessStatusCode)
            {
                
                string content = await response.Content.ReadAsStringAsync();
                var listTask = JsonConvert.DeserializeObject < IEnumerable <TaskDto>>(content);
                foreach (var item in listTask)
                {
                    //Tasklist.ItemsSource = listTask;
                    Tasklist.Items.Add(item);
                    Tasklist.ItemClick += Tasklist_ItemClick;
                }
            }


           
        }

        private void Tasklist_ItemClick(object sender, ItemClickEventArgs e)
        {
            TaskDto item = (TaskDto)e.ClickedItem;
            this.Frame.Navigate(typeof(Details), item);

        }
    }
}
