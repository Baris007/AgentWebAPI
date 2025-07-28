using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AgentAPI.Services
{
	public class CustomHuggingFaceEmbeddingService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;
		private readonly string _model;

		public CustomHuggingFaceEmbeddingService(string apiKey, string model = "sentence-transformers/all-MiniLM-L6-v2")
		{
			_apiKey = apiKey;
			_model = model;

			_httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://api-inference.huggingface.co/")
			};
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
		}

		public async Task<float[]> EmbedAsync(string input)
		{
			
			var requestBody = JsonSerializer.Serialize(new { inputs = new[] { input } });
			Console.WriteLine(" Gönderilen istek:");
			Console.WriteLine(requestBody);

			var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

			var url = $"models/{_model}";
			Console.WriteLine($" İstek URL'si: {url}");

			var response = await _httpClient.PostAsync(url, content);

			
			Console.WriteLine($" Yanıt: {(int)response.StatusCode} - {response.ReasonPhrase}");

			
			if (!response.IsSuccessStatusCode)
			{
				var errorDetails = await response.Content.ReadAsStringAsync();
				Console.WriteLine(" Hata yanıtı:");
				Console.WriteLine(errorDetails);
			}

			
			response.EnsureSuccessStatusCode();

			
			var responseString = await response.Content.ReadAsStringAsync();

			Console.WriteLine(" Embed yanıtı:");
			Console.WriteLine(responseString);

			var data = JsonSerializer.Deserialize<List<List<float>>>(responseString);
			return data?.FirstOrDefault()?.ToArray() ?? [];
		}
	}
}
