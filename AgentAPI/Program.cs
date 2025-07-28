using AgentAPI.Plugins;
using AgentAPI.Services;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy
			.WithOrigins("http://localhost:5173")
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
	});
});
builder.Services.AddSignalR();

// Kernel ve ChatCompletion servisini ekle
builder.Services.AddSingleton<Kernel>(sp =>
{
	var httpClient = new HttpClient();
	httpClient.Timeout = TimeSpan.FromMinutes(5);

	var b = Kernel.CreateBuilder();

#pragma warning disable SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
	var chatService = new OpenAIChatCompletionService(
		modelId: "gemma-3-12b",
		apiKey: "sk-no-key-required",
		endpoint: new Uri("http://localhost:1234/v1"),
		httpClient: httpClient
	);
#pragma warning restore SKEXP0010 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

	b.Services.AddSingleton<IChatCompletionService>(chatService);

	// Pluginleri ekle
	b.Plugins.AddFromType<WeatherPlugin>();
	b.Plugins.AddFromType<JokePlugin>();
	b.Plugins.AddFromType<TranslatePlugin>();
	b.Plugins.AddFromType<MathPlugin>();
	b.Plugins.AddFromType<SymptomPlugin>();
	b.Plugins.AddFromType<WebSearchPlugin>();

	return b.Build();
});


builder.Services.AddSingleton(new CustomHuggingFaceEmbeddingService("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"));

// Ayrýca ayrý olarak IChatCompletionService register et
builder.Services.AddSingleton<IChatCompletionService>(sp =>
{
	return sp.GetRequiredService<Kernel>().GetRequiredService<IChatCompletionService>();
});

builder.Services.AddSingleton<IRetriever, InMemoryRetriever>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
