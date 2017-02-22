using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using CodebustersAppWMU2.Models;
using Newtonsoft.Json;

namespace CodebustersAppWMU2.Services
{
    class RequestHelper
    {
        public IEnumerable<Type> Type { get; set; }
        public async void GetRequest(string requestTo)
        {

            HttpClient client = TaskManagerHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/" + requestTo);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var elements = JsonConvert.DeserializeObject<IEnumerable<TaskDto>>(content);
            }
 


        }
    }
}
