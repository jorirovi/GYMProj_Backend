using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class OrganizadorREntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? iD { get; set; }
        public string? idrutina { get; set; }	
        public string? idusuario { get; set; }
        public string? idtabata { get; set; }
        public string? idejercicio { get; set; }	
    }
}