using System.Text;
using MediatR;
using TarotGame.Shared.Enums;
using TarotGame.Shared.Games;
using Telegram.Bot.Types;

namespace TarotGame.Shared.TaroReader;

public class TaroReader : ITaroReader
{
    private readonly IPlayerService _playerService;
    private readonly IPredictor _predictor;
    private readonly IMediator _mediator;

    private const string HelpMessage =
        "Я таролог. Для начала работы введите /start. Для получения помощи введите /help.";

    private const string StartMessage =
        "Добро пожаловать. Я предсказываю будущее. Для начала выберите предсказание на день /command1. Или свой вопрос /command2.";

    public TaroReader(IPlayerService playerService, IPredictor predictor, IMediator mediator)
    {
        _playerService = playerService;
        _predictor = predictor;
        _mediator = mediator;
    }


    public async Task MessageReceived(Message message)
    {
        var player = await _playerService.GetByUserIdAsync(message.Chat.Id) ??
                     await _playerService.SaveAsync(Player.Create(message));


        switch (message.Text)
        {
            case "/start":
                await SendToClient(message.Chat.Id, StartMessage);
                await _playerService.SaveAsync(Player.Create(message));
                break;
            case "/help":
                await SendToClient(message.Chat.Id, HelpMessage);
                break;
            case "/cart":
                await SendToClient(message.Chat.Id, "You cart is: ");
                break;
            case "/command1":
                //ToDo: use factory
                player.Game = new DayPredictionGame();
                await SendToClient(message.Chat.Id, player.Game.Name);
                await SendToClient(message.Chat.Id, "Тусуем колоду...");
                player.Game.ShuffleDeck();
                await DayPrediction(message, player);
                break;
            case "/command2":
                //ToDo: use factory
                player.Game = new QuestionPredictionGame();
                await SendToClient(message.Chat.Id, player.Game.Name);
                await SendToClient(message.Chat.Id, "Тусуем колоду...");
                player.Game.ShuffleDeck();
                await SendToClient(message.Chat.Id, "Задайте свой вопрос");
                await QuestionPrediction(message, player);

                break;
            default:
                await HandleIncomingMessage(message);
                break;
        }
    }


    private async Task HandleIncomingMessage(Message message)
    {
        var player = await _playerService.GetByUserIdAsync(message.Chat.Id);

        //ToDo: Refactor this
        if (player.Game is DayPredictionGame)
        {
            await DayPrediction(message, player);

            if (player.Game.State == GameStates.PredictionReady)
            {
                player.Game = null;
            }
        }

        if (player.Game is QuestionPredictionGame)
        {
            await QuestionPrediction(message, player);

            if (player.Game.State == GameStates.PredictionReady)
            {
                player.Game = null;
            }
        }
    }

    private async Task QuestionPrediction(Message message, Player player)
    {
        if (player.Game.State == GameStates.DeckShuffled)
        {
            player.Game.AskQuestion(message.Text);
            await SendToClient(message.Chat.Id, "Выберите первую карту от 1 до 78");
            return;
        }


        if (player.Game.State == GameStates.QuestionAsked)
        {
            while (player.Game.SelectedCards.Count < player.Game.MaxCards)
            {
                if (int.TryParse(message.Text, out var cardNumber))
                {
                    var card = player.Game.SelectCard(cardNumber);
                    await SendToClient(message.Chat.Id, "Вы выбрали карту " + card.Name);


                    var answer = await _predictor.PredictAsync(player.Game);
                    await SendToClient(message.Chat.Id, answer);
                }
                else
                {
                    await SendToClient(message.Chat.Id, "Вы ввели не число");
                }
            }
        }

        if (player.Game.State == GameStates.CardsSelected)
        {
            await SendToClient(message.Chat.Id, "Предсказываем...");

            var answer = await _predictor.PredictAsync(player.Game);
            await SendToClient(message.Chat.Id, answer);
        }
    }

    private async Task DayPrediction(Message message, Player player)
    {
        if (player.Game.State == GameStates.DeckShuffled)
        {
            player.Game.AskQuestion(String.Empty);
            await SendToClient(message.Chat.Id, "Выберите карту от 1 до 78");
            return;
        }

        if (player.Game.State == GameStates.QuestionAsked)
        {
            if (int.TryParse(message.Text, out var cardNumber))
            {
                var card = player.Game.SelectCard(cardNumber);
                await SendToClient(message.Chat.Id, "Вы выбрали карту " + card.Name);
                await SendToClient(message.Chat.Id, "Предсказываем...");

                var answer = await _predictor.PredictAsync(player.Game);
                await SendToClient(message.Chat.Id, answer);
            }
            else
            {
                await SendToClient(message.Chat.Id, "Вы ввели не число");
            }
        }
    }

    private async Task SendToClient(long chatId, string text)
    {
        await _mediator.Send(new MessageSend(chatId, text));
    }
}