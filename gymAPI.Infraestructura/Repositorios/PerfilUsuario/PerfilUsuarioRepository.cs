using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.PerfilUsuario
{
    public class PerfilUsuarioRepository : ICrudRepository<PerfilUsuarioEntity>, IPerfilUsuarioRepository
    {
        private readonly IMongoCollection<PerfilUsuarioEntity> _collection;
        public PerfilUsuarioRepository(IConfiguration configuration){
            var settings = configuration.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<PerfilUsuarioEntity>("perfilUsuario");
        }

        public async Task<PerfilUsuarioEntity> CreateAsync(PerfilUsuarioEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<PerfilUsuarioEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<PerfilUsuarioEntity> GetPerfilByIDU(string idU)
        {
            return _collection.Find(pU => pU.idUsuario == idU).FirstOrDefaultAsync();
        }

        public Task<PerfilUsuarioEntity> GetUserByID(string id)
        {
            return _collection.Find(pU => pU.Id == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(PerfilUsuarioEntity entity)
        {
            await _collection.DeleteOneAsync(pU => pU.Id == entity.Id);
        }

        public async Task<PerfilUsuarioEntity> UpdateAsync(PerfilUsuarioEntity entity)
        {
            await _collection.ReplaceOneAsync(pU => pU.Id == entity.Id, entity);
            return entity;
        }
    }
}