using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AS_Assessment.Services.Implementation
{
    public class GroqAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GroqAiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Groq:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> EnhanceInventoryItemAsync(string name, int quantity, string stockStatus, string whatNeeded, string categoryName)
        {
            var userPrompt = $@"
                You are a helpful assistant that improves inventory item entries.

                Given these fields:

                Name: {name}
                Quantity: {quantity}
                Stock Status: {stockStatus}
                What is Needed: {whatNeeded}
                Category Name: {categoryName}

                Improve and enhance **all fields**. Provide:

                **Improved Name:** A better, clearer name.

                **Improved Quantity:** Suggest an optimal quantity or confirm the quantity.

                **Improved Stock Status:** Suggest a stock status or confirm the current.

                **Improved What Needed:** Clear explanation of what is needed.

                ";

            var requestBody = new
            {
                model = "llama3-8b-8192",
                messages = new[]
                {
                    new { role = "system", content = "You are an assistant that improves and enhances inventory item entries." },
                    new { role = "user", content = userPrompt }
                },
                max_tokens = 500
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
            }

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString()
                ?.Trim();
        }

    }
}
