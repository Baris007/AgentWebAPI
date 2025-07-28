using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AgentAPI.Plugins
{
	public class TranslatePlugin
	{
		[KernelFunction, Description("Girilen Türkçe cümleyi İngilizceye çevirir.")]
		public string TranslateToEnglish(
			[Description("Çevrilecek Türkçe metin")] string text)
		{
			// Test için sabit bir dönüş
			return "This is the translated sentence.";
		}
	}
}
