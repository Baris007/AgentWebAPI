using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AgentAPI.Plugins
{
	public class MathPlugin
	{
		[KernelFunction, Description("Kısa matematiksel işlemleri çözer. Örn: 4+7, 12*5, 100/2, kök(16).")]
		public async Task<string> Calculate(
			[Description("Çözülecek matematiksel ifade.")] string expression,
			Kernel kernel)
		{
			
			string prompt = $@"Aşağıdaki matematiksel işlemi çöz, sadece sonucu ver: {expression}";
			var result = await kernel.InvokePromptAsync(prompt);
			return result.ToString().Trim();
		}
	}
}
