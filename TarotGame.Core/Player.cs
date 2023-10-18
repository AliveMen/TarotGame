using TarotGame.Shared.Games;
using Telegram.Bot.Types;

namespace TarotGame.Shared;

public class Player
{
    public long Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? UserName { get; set; }
    
    public BaseGame? Game { get; set; }
    
    public static Player Create(Message message)
    {
        if (message.From?.Id == null)
            throw new ArgumentNullException(nameof(message.From.Id));
        
        return new Player
        {
            Id = message.From.Id,
            FirstName = message.Chat.FirstName,
            LastName = message.Chat.LastName,
            UserName = message.From.Username,
        };
    }
}