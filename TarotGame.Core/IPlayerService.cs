namespace TarotGame.Shared;

public interface IPlayerService
{
    Task<Player> SaveAsync(Player player);
    Task<Player?> GetByUserIdAsync(long userId);
}