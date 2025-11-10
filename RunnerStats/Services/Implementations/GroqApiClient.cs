using System.Text;
using Newtonsoft.Json.Linq;
using RunnerStats.Services.Interfaces;

namespace RunnerStats.Services.Implementations
{
    public class GroqApiClient : IGroqApiClient
    {
        private  HttpClient _client = new HttpClient();
        public GroqApiClient(string apiKey)
        {
            _client.DefaultRequestHeaders.Add("Authorization","Bearer " + apiKey);
        }


        public async Task<JObject> GetResponse(JObject request)
        {
            StringContent httpContent = new StringContent(request.ToString(),Encoding.UTF8,"application/json");

            Console.WriteLine(await httpContent.ReadAsStringAsync());


            HttpResponseMessage response = await _client.PostAsync("https://api.groq.com/openai/v1/chat/completions", httpContent);
            string responseString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error from Groq API: " + responseString);
                return null; // o lanza excepción personalizada
            }
            JObject responseJson = JObject.Parse(responseString);
            return responseJson;
        }
    }
}
