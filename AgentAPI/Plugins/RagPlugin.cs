using AgentAPI.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AgentAPI.Plugins
{
	public class RagPlugin
	{
		private readonly IRetriever _retriever;
		private readonly IChatCompletionService _completion;

		public RagPlugin(IRetriever retriever, IChatCompletionService completion)
		{
			_retriever = retriever;
			_completion = completion;
		}

		[KernelFunction]
		public async Task<string> AskWithContextAsync(string input)
		{
			var context = await _retriever.RetrieveRelevantContextAsync(input);

			string prompt = $"Soru: {input}\nBağlamsal bilgi: {context}\nYanıt ver:";

			var result = await _completion.GetChatMessageContentAsync(
				new ChatHistory { new ChatMessageContent(AuthorRole.User, prompt) });

			return result?.Content ?? "Cevap üretilemedi.";
		}
	}
}
