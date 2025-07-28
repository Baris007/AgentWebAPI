using System.Text.Json;
using Microsoft.SemanticKernel;

namespace AgentAPI.Services
{
	public class EmbeddingRetriever : IRetriever
	{
		private readonly List<KnowledgeItem> _knowledge;

		public EmbeddingRetriever()
		{
			var json = File.ReadAllText("knowledgeBase.json");
			_knowledge = JsonSerializer.Deserialize<List<KnowledgeItem>>(json)!;
		}

		public async Task<string> RetrieveRelevantContextAsync(string query)
		{
			// Burada basit bir manuel embedding yapalım (örnek: kelime sayısı)
			var queryVec = ManualEmbed(query);

			// En yakın vektörü bul
			var top = _knowledge
				.Select(item => new
				{
					item.Text,
					Score = CosineSimilarity(queryVec, item.Embedding)
				})
				.OrderByDescending(x => x.Score)
				.FirstOrDefault();

			return top?.Text ?? "İlgili bilgi bulunamadı.";
		}

		private float[] ManualEmbed(string input)
		{
			// Çok basit bir örnek: karakter sayısı üzerinden 3 boyutlu vektör
			return new float[]
			{
				input.Length,
				input.Count(char.IsLetter),
				input.Count(char.IsWhiteSpace)
			};
		}

		private float CosineSimilarity(float[] a, float[] b)
		{
			float dot = 0, magA = 0, magB = 0;
			for (int i = 0; i < a.Length; i++)
			{
				dot += a[i] * b[i];
				magA += a[i] * a[i];
				magB += b[i] * b[i];
			}
			return (float)(dot / (Math.Sqrt(magA) * Math.Sqrt(magB) + 1e-8));
		}

		private class KnowledgeItem
		{
			public string Text { get; set; }
			public float[] Embedding { get; set; }
		}
	}
}
