using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class UsuariosEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? nombre { get; set; }
        public string? apellidos { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}