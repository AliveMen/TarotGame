using MediatR;
using TarotGame.Shared;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TarotGame.Infrastructure;

public class VkBot : IVkBot, IRequestHandler<MessageSend>
{
    private readonly IMediator _mediator;
    private const string Token = "";
    private static TelegramBotClient _bot;
    private User _me;


    public VkBot(IMediator mediator)
    {
        _mediator = mediator;
        _bot = new TelegramBotClient(Token);
    }

    async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken arg3)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => BotOnMessageReceived(update.Message), _ => UnknownUpdateHandlerAsync(update)
        };

        try
        {
            await handler;
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(botClient, exception, new CancellationToken());
        }
    }

    private async Task BotOnMessageReceived(Message message)
    {
        await _mediator.Send(new MessageReceived(message));
    }


    private static async Task UnknownUpdateHandlerAsync(Update update)
    {
        Console.WriteLine($"Unknown update type: {update.Type}");
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken arg3)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }


    public async Task Handle(MessageSend request, CancellationToken cancellationToken)
    {
        await _bot.SendTextMessageAsync(request.PlayerId, request.Message, cancellationToken: cancellationToken);
    }

    public async Task StartAsync()
    {
        _me = await _bot.GetMeAsync();
        _bot.StartReceiving(new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync));
    }
}
