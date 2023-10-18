using TarotGame.Shared;

namespace TarotGame.Infrastructure;

public class FakePlayerService : IPlayerService
{
    public Dictionary<long, Player> Players { get; set; } = new();
    public Task<Player> SaveAsync(Player player)
    {
        Players.TryAdd(player.Id, player);
        return Task.FromResult(player);
    }


    public Task<Player?> GetByUserIdAsync(long userId)
    {
        Players.TryGetValue(userId, out var player);
        return Task.FromResult(player);
    }
}