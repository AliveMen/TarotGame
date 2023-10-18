using System.Text;
using OpenAI_API;
using OpenAI_API.Chat;
using TarotGame.Shared;
using TarotGame.Shared.Games;

namespace TarotGame.Infrastructure.OpenAI;

public class OpenAiPredictor : IPredictor
{
    private readonly OpenAIAPI _openAiApi;
    private readonly Conversation _chat;

    private const string ChatSetting =
        "Ты таролог который говорит с клиентом. Трактуй карты и давай ответы на вопросы.";

    private const string ApiKey = "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";

    public OpenAiPredictor()
    {
        _openAiApi = new OpenAIAPI(new APIAuthentication(ApiKey));
        _chat = _openAiApi.Chat.CreateConversation();
        _chat.AppendSystemMessage(ChatSetting);
    }


    public async IAsyncEnumerable<string> PredictDynamicAsync(ITarotGame game)
    {
        var question = PrepearQuestion(game);

        _chat.AppendUserInput(question.ToString());
        await foreach (var res in _chat.StreamResponseEnumerableFromChatbotAsync())
        {
            yield return res;
        }
    }

    public async Task<string> PredictAsync(ITarotGame game)
    {
        var question = PrepearQuestion(game);

        _chat.AppendUserInput(question.ToString());

        return await _chat.GetResponseFromChatbotAsync();
    }


    private static StringBuilder PrepearQuestion(ITarotGame game)
    {
        var question = new StringBuilder();
        question.AppendLine(game.AskedQuestion);
        question.AppendLine("Мне вывпали следующие карты:");
        foreach (var card in game.SelectedCards)
        {
            question.AppendLine(card.Name);
        }

        return question;
    }
}