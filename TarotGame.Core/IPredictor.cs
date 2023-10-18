using TarotGame.Shared.Games;

namespace TarotGame.Shared;

public interface IPredictor
{
    IAsyncEnumerable<string> PredictDynamicAsync(ITarotGame game);
    
    Task<string> PredictAsync(ITarotGame game);
}