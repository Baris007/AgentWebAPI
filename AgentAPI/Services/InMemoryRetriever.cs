using System.Collections.Generic;
using System.Linq;

namespace AgentAPI.Services
{
	public class InMemoryRetriever : IRetriever
	{
		private readonly Dictionary<string, string> _knowledgeBase = new()
{
	{ "d vitamini", "D vitamini, güneş ışığı aracılığıyla ciltte sentezlenir ve bağışıklık sistemini destekler." },
	{ "grip", "Grip, influenza virüsünün neden olduğu bulaşıcı bir hastalıktır. Belirtileri arasında ateş, öksürük, halsizlik bulunur." },
	{ "ateş", "Ateş, genellikle bir enfeksiyona karşı vücudun verdiği tepkidir." }
    
};

		public Task<string> RetrieveRelevantContextAsync(string query)
		{
			var lower = query.ToLower();
			foreach (var pair in _knowledgeBase)
			{
				if (lower.Contains(pair.Key.ToLower()))
					return Task.FromResult(pair.Value);
			}

			return Task.FromResult("Bilgi bulunamadı.");
		}
	}
}
