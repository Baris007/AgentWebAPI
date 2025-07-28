using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AgentAPI.Plugins
{
	public class SymptomPlugin
	{
		[KernelFunction, Description("Semptomlara göre olası hastalık(lar)ı Türkçe olarak kısa şekilde önerir.")]
		public async Task<string> CheckSymptoms(
			[Description("Hastanın semptomlarını virgülle yaz. Örn: ateş, öksürük, baş ağrısı")] string symptoms,
			Kernel kernel)
		{
			
			string prompt = $@"
Aşağıda bir hastanın semptomları verilmiştir: {symptoms}
Yalnızca olası 1–2 hastalık ismi öner, başka açıklama ekleme.
Yanıt örneği: grip, soğuk algınlığı
";
			var result = await kernel.InvokePromptAsync(prompt);
			return result.ToString().Trim();
		}
	}
}
