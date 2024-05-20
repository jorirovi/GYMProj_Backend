using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class RutinasgymEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? iD { get; set; }
        public string? rutina { get; set; }	
        public string? imagen  { get; set; }
        public double peso { get; set; }
        public string? equipo { get; set; }
        public int? repeticiones { get; set; }
        public string? idusuario { get; set; }
    }
}