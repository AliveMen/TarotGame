namespace TarotGame.Shared;

public class TarotCard
{
    public int Number { get; set; }
    public string Name { get; set; }
}

public class TarotDeck
{
    public TarotCard[] Cards { get; set; }

    public TarotDeck()
    {
        Cards = TarotCardHelper.GetTarotDeck();
    }

    //Generate a function to shuffle the deck
    public void Shuffle()
    {
        //Create a new random object
        Random random = new Random();
        //Loop through all the cards
        for (int i = 0; i < Cards.Length; i++)
        {
            //Get a random index
            int randomIndex = random.Next(0, Cards.Length);
            //Swap the current card with the random card
            (Cards[randomIndex], Cards[i]) = (Cards[i], Cards[randomIndex]);
        }
    }
}

public class TarotCardHelper
{
    public static TarotCard[] GetTarotDeck()
    {
        TarotCard[] tarotCards = new TarotCard[78];

        for (int i = 0; i < tarotCards.Length; i++)
        {
            tarotCards[i] = new TarotCard() { Number = i + 1, Name = GetCardName(i + 1) };
        }

        return tarotCards;
    }

    private static string GetCardName(int cardNumber)
    {
        switch (cardNumber)
        {
            case 1:
                return "The Fool";
            case 2:
                return "The Magician";
            case 3:
                return "The High Priestess";
            case 4:
                return "The Empress";
            case 5:
                return "The Emperor";
            case 6:
                return "The Hierophant";
            case 7:
                return "The Lovers";
            case 8:
                return "The Chariot";
            case 9:
                return "Strength";
            case 10:
                return "The Hermit";
            case 11:
                return "Wheel of Fortune";
            case 12:
                return "Justice";
            case 13:
                return "The Hanged Man";
            case 14:
                return "Death";
            case 15:
                return "Temperance";
            case 16:
                return "The Devil";
            case 17:
                return "The Tower";
            case 18:
                return "The Star";
            case 19:
                return "The Moon";
            case 20:
                return "The Sun";
            case 21:
                return "Judgement";
            case 22:
                return "The World";
            case 23:
                return "Ace of Wands";
            case 24:
                return "Two of Wands";
            case 25:
                return "Three of Wands";
            case 26:
                return "Four of Wands";
            case 27:
                return "Five of Wands";
            case 28:
                return "Six of Wands";
            case 29:
                return "Seven of Wands";
            case 30:
                return "Eight of Wands";
            case 31:
                return "Nine of Wands";
            case 32:
                return "Ten of Wands";
            case 33:
                return "Page of Wands";
            case 34:
                return "Knight of Wands";
            case 35:
                return "Queen of Wands";
            case 36:
                return "King of Wands";
            case 37:
                return "Ace of Cups";
            case 38:
                return "Two of Cups";
            case 39:
                return "Three of Cups";
            case 40:
                return "Four of Cups";
            case 41:
                return "Five of Cups";
            case 42:
                return "Six of Cups";
            case 43:
                return "Seven of Cups";
            case 44:
                return "Eight of Cups";
            case 45:
                return "Nine of Cups";
            case 46:
                return "Ten of Cups";
            case 47:
                return "Page of Cups";
            case 48:
                return "Knight of Cups";
            case 49:
                return "Queen of Cups";
            case 50:
                return "King of Cups";
            case 51:
                return "Ace of Swords";
            case 52:
                return "Two of Swords";
            case 53:
                return "Three of Swords";
            case 54:
                return "Four of Swords";
            case 55:
                return "Five of Swords";
            case 56:
                return "Six of Swords";
            case 57:
                return "Seven of Swords";
            case 58:
                return "Eight of Swords";
            case 59:
                return "Nine of Swords";
            case 60:
                return "Ten of Swords";
            case 61:
                return "Page of Swords";
            case 62:
                return "Knight of Swords";
            case 63:
                return "Queen of Swords";
            case 64:
                return "King of Swords";
            case 65:
                return "Ace of Pentacles";
            case 66:
                return "Two of Pentacles";
            case 67:
                return "Three of Pentacles";
            case 68:
                return "Four of Pentacles";
            case 69:
                return "Five of Pentacles";
            case 70:
                return "Six of Pentacles";
            case 71:
                return "Seven of Pentacles";
            case 72:
                return "Eight of Pentacles";
            case 73:
                return "Nine of Pentacles";
            case 74:
                return "Ten of Pentacles";
            case 75:
                return "Page of Pentacles";
            case 76:
                return "Knight of Pentacles";
            case 77:
                return "Queen of Pentacles";
            case 78:
                return "King of Pentacles";
            default:
                return "";
        }
    }
}