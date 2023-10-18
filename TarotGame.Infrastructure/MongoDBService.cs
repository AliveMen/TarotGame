using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TarotGame.Shared;

namespace TarotGame.Infrastructure;

public class MongoDBService
{
    private readonly IMongoCollection<Player> _playerCollection;

    public MongoDBService(IOptions<MongoDbSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _playerCollection = database.GetCollection<Player>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Player>> GetAsync()
    {
        return await _playerCollection.Find(player => true).ToListAsync();
    }

    public async Task CreateAsync(Player playlist)
    {
        await _playerCollection.InsertOneAsync(playlist);
    }

    public async Task AddToPlaylistAsync(string id, string movieId)
    {
    }

    public async Task DeleteAsync(string id)
    {
    }
}