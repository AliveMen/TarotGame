using TarotGame.Shared.Enums;

namespace TarotGame.Shared.Games;

public class QuestionPredictionGame : BaseGame
{
    public QuestionPredictionGame()
    {
        Type = GameType.QuestionPrediction;
        MinCards = 3;
        MaxCards = 3;
        State = GameStates.Idle;
        Name = "Предсказание на вопрос";
    }
}