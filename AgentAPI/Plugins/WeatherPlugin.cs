using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgentAPI.Plugins
{
	public class WeatherPlugin
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey = "ed97f47331594d4eb1d102314253006"; // Buraya WeatherAPI.com'dan aldığın key

		public WeatherPlugin()
		{
			_httpClient = new HttpClient();
		}

		[KernelFunction, Description("Belirtilen şehir için gerçek hava durumunu verir.")]
		public async Task<string> GetWeatherAsync(
			[Description("Şehir ismi (örn. Edirne)")] string city)
		{
			try
			{
				var url = $"https://api.weatherapi.com/v1/current.json?key={_apiKey}&q={city}&lang=tr";

				var response = await _httpClient.GetAsync(url);

				if (!response.IsSuccessStatusCode)
					return $"{city} için hava durumu alınamadı.";

				var json = await response.Content.ReadAsStringAsync();
				using var doc = JsonDocument.Parse(json);

				var root = doc.RootElement;
				var condition = root.GetProperty("current").GetProperty("condition").GetProperty("text").GetString();
				var temp = root.GetProperty("current").GetProperty("temp_c").GetDecimal();

				return $"{city} için hava {condition?.ToLower()}, sıcaklık {temp}°C.";
			}
			catch
			{
				return $"{city} için hava durumu alınamadı.";
			}
		}
	}
}
