using TarotGame.Shared.Enums;

namespace TarotGame.Shared.Games;

public class DayPredictionGame : BaseGame
{
    public DayPredictionGame()
    {
        Type = GameType.DayPrediction;
        MinCards = 1;
        MaxCards = 1;
        State = GameStates.Idle;
        Name = "Предсказание на день";
    }

    /// <summary>
    /// Only one question is allowed in this game
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    public override string AskQuestion(string question)
    {
        return base.AskQuestion("Что меня ожидает сегодня?");
    }
    
     
}