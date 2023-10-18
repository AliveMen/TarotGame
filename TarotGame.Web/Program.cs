using System.Reflection;
using System.Text;
using TarotGame.Infrastructure;
using TarotGame.Infrastructure.OpenAI;
using TarotGame.Shared;
using TarotGame.Shared.TaroReader;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(VkBotMessageHandler).Assembly);
        cfg.RegisterServicesFromAssembly(typeof(VkBot).Assembly);
    }
);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddSingleton<IVkBot, VkBot>();
builder.Services.AddTransient<IPredictor, OpenAiPredictor>();
builder.Services.AddTransient<ITaroReader, TaroReader>();
//TODO: remove singleton and use mongoDbService
builder.Services.AddSingleton<IPlayerService, FakePlayerService>();




var app = builder.Build();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/hello", () => "Hello World!")
    .WithName("hello")
    .WithOpenApi();

app.MapGet("/player", async (MongoDBService mongoDbService) => await mongoDbService.GetAsync())
    .WithName("player")
    .WithOpenApi();

app.MapPost("/player", async (MongoDBService mongoDbService, Player player) => await mongoDbService.CreateAsync(player))
    .WithName("CreatePlayer")
    .WithOpenApi();

app.MapGet("/tarot/day/{id}", async (IPredictor predictor, int id) =>
    {
        var taroDeck = new TarotDeck();
        taroDeck.Shuffle();
        var card = taroDeck.Cards[id];
        var answer = new StringBuilder();
         return answer.ToString();

    })
    .WithName("TarotOfADay")
    .WithOpenApi();

app.MapGet("/bot/start", async (IVkBot vkbot) => await vkbot.StartAsync()).WithName("StartBot").WithOpenApi();

app.Run();


