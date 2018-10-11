using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WinMemoryGame
{
    class Global
    {
        public static bool exit()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(@"http://localhost:58141/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync($"deleteUser/{Properties.Settings.Default.userName}").Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
