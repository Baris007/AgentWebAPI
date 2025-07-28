using Microsoft.SemanticKernel;
using System.Net.Http;
using System.Text.Json;
using System.ComponentModel;

namespace AgentAPI.Plugins
{
	public class WebSearchPlugin
	{
		private static readonly HttpClient _http = new HttpClient();

		[KernelFunction, Description("DuckDuckGo’dan internetten anlık bilgi getirir.")]
		public async Task<string> SearchInstantAsync([Description("Aranacak konu")] string query)
		{
			var url = $"https://api.duckduckgo.com/?q={Uri.EscapeDataString(query)}&format=json&no_html=1";
			var resp = await _http.GetStringAsync(url);
			using var doc = JsonDocument.Parse(resp);
			var answer = doc.RootElement.GetProperty("AbstractText").GetString();
			return string.IsNullOrWhiteSpace(answer) ? "Web'de bilgi bulunamadı." : answer;
		}
	}
}
