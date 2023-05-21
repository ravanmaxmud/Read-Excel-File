using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Read_Excel_File.Services.Abstracts;

namespace Read_Excel_File.Services.Concretes
{
    public class JiraService : IJiraService, IDisposable
    {
        private readonly HttpClient _httpClient;

        public JiraService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://caucasustech.atlassian.net");

            string username = "admin@caucasustech.az";
            string password = "i3YRfi0tMgjPG0zVqSDjCC7D";
            string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
        }

        public async Task<string> PostRequestJsonFormatAsync(string modelName, string modelCount)
        {
            try
            {
                var request = new
                {
                    fields = new
                    {
                        project = new { id = "10022" },
                        summary = "TEST",
                        issuetype = new { id = "10002" },
                        customfield_10198 = modelCount,
                        customfield_10199 = modelName
                    }
                };

                var jsonRequest = JsonSerializer.Serialize(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                string url = "/rest/api/3/issue";

                using (var response = await _httpClient.PostAsync(url, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return $"Запрос выполнен успешно. Ответ: {jsonResponse}";
                    }
                    else
                    {
                        return $"Произошла ошибка при выполнении запроса. Статус код: {response.StatusCode}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Произошла ошибка при выполнении запроса: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}