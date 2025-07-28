using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AgentAPI.Plugins
{
	public class JokePlugin
	{
		[KernelFunction, Description("Türkçe, kısa ve komik bir bilgisayar şakası döndürür.")]
		public string GetJoke()
		{
			return "Neden bilgisayarlar asla acıkmaz? Çünkü hep çerezleri (cookie) vardır!";
		}
	}
}
