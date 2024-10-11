using MongoDB.Bson.Serialization.Attributes;

namespace gymAPI.Infraestructura.Database.Entidades
{
    public class vaultEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string tituloV { get; set; } = string.Empty;
        public string urlV  { get; set; } = string.Empty;
        public string usuarioV { get; set; } = string.Empty;
        public string passV { get; set; } = string.Empty;
    }
}