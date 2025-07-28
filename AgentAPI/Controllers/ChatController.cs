using AgentAPI.Models;
using AgentAPI.Services; // IRetriever burada
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AgentAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ChatController : ControllerBase
	{
		private readonly Kernel _kernel;
		private readonly IChatCompletionService _chatService;
		private readonly IRetriever _retriever;

		public ChatController(Kernel kernel, IChatCompletionService chatService, IRetriever retriever)
		{
			_kernel = kernel;
			_chatService = chatService;
			_retriever = retriever;
		}

		[HttpPost]
		public async Task<IActionResult> Chat([FromBody] ChatRequest req)
		{
			string userMessage = req.Message ?? "";
			string context = await _retriever.RetrieveRelevantContextAsync(userMessage);

			var prompt = $"""
			Aşağıda kullanıcının mesajıyla ilgili ön bilgi (bağlam) verilmiştir:
			{context}

			Kullanıcının mesajı: {userMessage}

			Bağlamı dikkate alarak kısa, Türkçe cevap ver. Gerekirse uygun fonksiyonu çağır.
			""";

			var settings = new OpenAIPromptExecutionSettings
			{
				ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
			};

			string response = "";
			await foreach (var msg in _chatService.GetStreamingChatMessageContentsAsync(prompt, settings, _kernel))
			{
				if (!string.IsNullOrWhiteSpace(msg.Content))
					response += msg.Content;
			}

			return Ok(new { response = response.Trim() });
		}
	}
}
