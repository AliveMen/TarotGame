using TarotGame.Shared.Enums;

namespace TarotGame.Shared.Games;

public abstract class BaseGame : ITarotGame
{
    public string Name { get; init; } = string.Empty;
    public string Question { get; set; } = string.Empty;
    public GameStates State { get; set; }
    public GameType Type { get; set; }

    protected int MinCards { get; init; }
    
    public int MaxCards { get; init; }

    public HashSet<TarotCard> SelectedCards { get; init; } = new();

    private TarotDeck Deck { get; set; } = new();
    
    public virtual void ShuffleDeck()
    {
        Deck.Shuffle();
        State = GameStates.DeckShuffled;
    }

    public virtual string AskQuestion(string question)
    {
        Question = question;
        State = GameStates.QuestionAsked;
        return Question;
    }

    public string AskedQuestion => Question;

    public virtual TarotCard SelectCard(int cardNumber)
    {
        if (State != GameStates.QuestionAsked)
        {
            throw new InvalidOperationException("You must ask a question first");
        }
        
        if(cardNumber is < 1 or > 78)
        {
            throw new ArgumentOutOfRangeException(nameof(cardNumber), "Card number must be between 1 and 78");
        }
        
        if(SelectedCards.Count >= MaxCards)
        {
            throw new InvalidOperationException("You have already selected the maximum number of cards");
        }
        
        var card = Deck.Cards[cardNumber - 1];
        var success = SelectedCards.Add(card);
        
        if(!success)
        {
            throw new InvalidOperationException("You have already selected this card");
        }
        
        if(SelectedCards.Count == MaxCards)
        {
            State = GameStates.CardsSelected;
        }

        return card;
    }
}