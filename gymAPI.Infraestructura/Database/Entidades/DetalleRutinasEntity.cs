using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class DetalleRutinasEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? idRutina { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? idUsuario { get; set; }
        public int zonaCorporal { get; set; } = 0;
        public string? ejercicio { get; set; }
        public string? maquina { get; set; }
        public int peso { get; set; }
        public int unidadPeso { get; set; }
        public int repeticiones { get; set; }
        public string? ilustracion { get; set; }
    }
}