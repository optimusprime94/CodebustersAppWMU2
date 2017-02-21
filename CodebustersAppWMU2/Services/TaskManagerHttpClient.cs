using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace CodebustersAppWMU2.Services
{
 
        /* 
         * This class helps us initiate our client. This is a good way to be able to reuse this
         * code across this application. We initiate a new client instance, and we set our base adress as stated
         * to our web-api's currently local uri, next we clear the headers and state that we accept data in json format. 
         */
        public static class TaskManagerHttpClient
        {

            public static HttpClient GetClient()
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:53805/") };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }

        }

    }

