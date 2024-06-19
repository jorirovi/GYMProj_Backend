using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.ZonaCorporal
{
    public class ZonaCorporalRepository : ICrudRepository<ZonaCorporalEntity>
    {
        private readonly IMongoCollection<ZonaCorporalEntity> _collection;
        public ZonaCorporalRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<ZonaCorporalEntity>("zonacorporal");
        }

        public Task<ZonaCorporalEntity> CreateAsync(ZonaCorporalEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<ZonaCorporalEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ZonaCorporalEntity> GetUserByID(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ZonaCorporalEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<ZonaCorporalEntity> UpdateAsync(ZonaCorporalEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}