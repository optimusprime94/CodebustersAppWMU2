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
         * Hopefully this code works. When calling this method you need to specify which type is intended
         * for the request, and return type. Don't forget to use await when calling this method as it returns
         * a Task, also the parameter takes in the url request: ...api/{request}.
         * Call example: var elements = await GetRequest<TaskDto>("tasks");
         */
        public async Task<IEnumerable<T>> GetRequest<T>(string request)
        {
            try
            {
                HttpClient client = TaskManagerHttpClient.GetClient();

                HttpResponseMessage response = await client.GetAsync("api/" + request);

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
