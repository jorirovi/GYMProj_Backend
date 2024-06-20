using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.ZonaCorporal
{
    public class ZonaCorporalRepository : ICrudRepository<ZonaCorporalEntity>, IZonaCorporalRepository
    {
        private readonly IMongoCollection<ZonaCorporalEntity> _collection;
        public ZonaCorporalRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<ZonaCorporalEntity>("zonacorporal");
        }

        public async Task<ZonaCorporalEntity> CreateAsync(ZonaCorporalEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<ZonaCorporalEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<ZonaCorporalEntity> GetByNZC(int numero)
        {
            return _collection.Find(zC => zC.numeroZC == numero).FirstOrDefaultAsync();
        }

        public Task<ZonaCorporalEntity> GetByZC(string zonaCorporal)
        {
            return _collection.Find(zC => zC.zonaCorporal == zonaCorporal).FirstOrDefaultAsync();
        }

        public async Task<ZonaCorporalEntity> GetUserByID(string id)
        {
            return await _collection.Find(zC => zC.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(ZonaCorporalEntity entity)
        {
            await _collection.DeleteOneAsync(zC => zC.Id == entity.Id);
        }

        public async Task<ZonaCorporalEntity> UpdateAsync(ZonaCorporalEntity entity)
        {
            await _collection.ReplaceOneAsync(zC => zC.Id == entity.Id, entity);
            return entity;
        }
    }
}