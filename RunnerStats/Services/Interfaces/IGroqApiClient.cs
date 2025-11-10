using Newtonsoft.Json.Linq;

namespace RunnerStats.Services.Interfaces
{
    public interface IGroqApiClient
    {
        public Task<JObject> GetResponse(JObject request);
    }
}
