using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Net.Http;
using WPF_admin.Schemas;
using System.Text.Json;

namespace WPF_admin
{

    public class Competitions
    {
        [JsonPropertyName("iD_Competition")]
        public int ID_Competition { get; set; }

        [JsonPropertyName("competitionType")]
        public string CompetitionType { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("competitionYear")]
        public string CompetitionYear { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = "ManWoman";

        [JsonIgnore]
        public string SearchTerm { get { return $"{ID_Competition} {CompetitionType} {Gender} {Location} {CompetitionYear}"; } }
    }

    public static class CompetitionHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string tekmovanjeBaseUrl = APIConnector.baseURL + "Competitions\\";

        public static async Task<List<Competitions>> GetAll()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringTekmovanja = await client.GetStreamAsync(tekmovanjeBaseUrl);
            List<Competitions> tekmovanja = await JsonSerializer.DeserializeAsync<List<Competitions>>(stringTekmovanja);
            tekmovanja.Reverse();
            return tekmovanja;
        }

        public static async Task<bool> Post(Competitions tekmovanje)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(tekmovanje));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PostAsync(tekmovanjeBaseUrl, vsebina);

            return true;
        }

        public static async Task<bool> Update(Competitions tekmovanje)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent vsebina = new StringContent(JsonSerializer.Serialize(tekmovanje));
            vsebina.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage httpResponse = await client.PutAsync(tekmovanjeBaseUrl + tekmovanje.ID_Competition, vsebina);

            return true;
        }

        public static async Task<bool> Delete(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage httpResponse = await client.DeleteAsync(tekmovanjeBaseUrl + id);

            return true;
        }
    }
}
