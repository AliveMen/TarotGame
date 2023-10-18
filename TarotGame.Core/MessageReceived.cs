using MediatR;
using Telegram.Bot.Types;

namespace TarotGame.Shared;

public record MessageReceived(Message Message) : IRequest;
public record MessageSend(long PlayerId, string Message) : IRequest;
