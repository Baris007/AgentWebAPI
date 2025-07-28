namespace AgentAPI.Services
{
	public interface IRetriever
	{
		Task<string> RetrieveRelevantContextAsync(string query);
	}
}
