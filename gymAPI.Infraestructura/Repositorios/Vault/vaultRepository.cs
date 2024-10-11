using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.Vault
{
    public class vaultRepository : ICrudRepository<vaultEntity>, IVaultRepository
    {
        private readonly IMongoCollection<vaultEntity> _collection;
        public vaultRepository(IConfiguration config)
        {
var settings = config.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<vaultEntity>("vault");
        }

        public async Task<vaultEntity> CreateAsync(vaultEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<vaultEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<vaultEntity> GetUserByID(string id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<vaultEntity> GetVaultByTitle(string title)
        {
            return await _collection.Find(v => v.tituloV == title).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(vaultEntity entity)
        {
            await _collection.DeleteOneAsync(u => u.Id == entity.Id);
        }

        public async Task<vaultEntity> UpdateAsync(vaultEntity entity)
        {
            await _collection.ReplaceOneAsync(u => u.Id == entity.Id, entity);
            return entity;
        }
    }
}