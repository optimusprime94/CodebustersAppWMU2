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
        /*
         * Hopefully this code works
         * 
         */
        public async Task<IEnumerable<T>> GetRequest<T>(string requestTo)
        {
            try
            {
                HttpClient client = TaskManagerHttpClient.GetClient();

                HttpResponseMessage response = await client.GetAsync("api/" + requestTo);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var jsonContent = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                    return jsonContent;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }


        }
    }
}
