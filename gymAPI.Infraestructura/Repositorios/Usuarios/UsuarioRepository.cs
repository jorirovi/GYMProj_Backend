using gymAPI.Infraestructura.Database.Entidades;
using gymAPI.Infraestructura.Repositorios.General;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace gymAPI.Infraestructura.Repositorios.Usuarios
{
    public class UsuarioRepository : ICrudRepository<UsuariosEntity>, IUsuarioRepository
    {
        private readonly IMongoCollection<UsuariosEntity> _collection;
        public UsuarioRepository(IConfiguration config)
        {
            var settings = config.GetSection("GymDataBase").Get<GymDBSettings>();
            var client = new MongoClient(settings.connectionString);
            var database = client.GetDatabase(settings.databaseName);
            _collection= database.GetCollection<UsuariosEntity>("usuarios");
        }

        public async Task<UsuariosEntity> CreateAsync(UsuariosEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<List<UsuariosEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task<UsuariosEntity> GetUserByEmailPass(string email, string password)
        {
            return _collection.Find(u => u.email == email && u.password == password).FirstOrDefaultAsync();
        }

        public async Task<UsuariosEntity> GetUserByID(string id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task<UsuariosEntity> GetUsuarioByEmail(string email)
        {
            return _collection.Find(u => u.email == email).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(UsuariosEntity entity)
        {
            await _collection.DeleteOneAsync(u => u.Id == entity.Id);
        }

        public async Task<UsuariosEntity> UpdateAsync(UsuariosEntity entity)
        {
            await _collection.ReplaceOneAsync(u => u.Id == entity.Id, entity);
            return entity;
        }
    }
}