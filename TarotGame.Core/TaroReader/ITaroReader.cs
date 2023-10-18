using Telegram.Bot.Types;

namespace TarotGame.Shared;

public interface ITaroReader
{
    Task MessageReceived(Message message);
}