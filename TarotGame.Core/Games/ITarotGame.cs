namespace TarotGame.Shared.Games;

public interface ITarotGame
{
    public void ShuffleDeck();
    
    public string AskQuestion(string question);
    
    public string AskedQuestion { get; }
    
    public HashSet<TarotCard> SelectedCards { get; init; }
}