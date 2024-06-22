using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class PerfilUsuarioEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? idUsuario { get; set;}
        public int edad { get; set; }
        public double peso { get; set; }
        public string? sexo { get; set; }
    }
}