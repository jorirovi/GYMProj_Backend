using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.DetalleRutinas
{
    public class DetalleRutinaRepository : ICrudRepository<DetalleRutinasEntity>, IDetalleRRepository
    {
        private readonly IMongoCollection<DetalleRutinasEntity> _collection;
        public DetalleRutinaRepository(IConfiguration configuration)
        {
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<DetalleRutinasEntity>("DetalleRutina");
        }

        public async Task<DetalleRutinasEntity> CreateAsync(DetalleRutinasEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<DetalleRutinasEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<DetalleRutinasEntity>> GetDRbyIdRutina(string idRutina)
        {
            return await _collection.Find(dr => dr.idRutina == idRutina).ToListAsync();
        }

        public async Task<List<DetalleRutinasEntity>> GetDRByIdUsuario(string idUsuario)
        {
            return await _collection.Find(dr => dr.idUsuario == idUsuario).ToListAsync();
        }

        public Task<List<DetalleRutinasEntity>> GetDRbyZonaCorporal(int zCorporal)
        {
            return _collection.Find(dr => dr.zonaCorporal == zCorporal).ToListAsync();
        }

        public async Task<DetalleRutinasEntity> GetUserByID(string id)
        {
            return await _collection.Find(rd => rd.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(DetalleRutinasEntity entity)
        {
            await _collection.DeleteOneAsync(rd => rd.Id == entity.Id);
        }

        public async Task<DetalleRutinasEntity> UpdateAsync(DetalleRutinasEntity entity)
        {
            await _collection.ReplaceOneAsync(rd => rd.Id == entity.Id, entity);
            return entity;
        }
    }
}