namespace gymAPI.Comunes.Classes.Contracts
{
    public class DetalleRutinaContract
    {
        public string? Id { get; set; }
        public string? idRutina { get; set; }
        
        public string? idUsuario { get; set; }
        
        public string? ejercicio { get; set; }
        public string? maquina { get; set; }
        public int peso { get; set; }
        public int unidadPeso { get; set; }
        public int repeticiones { get; set; }
        public string? ilustracion { get; set; }
    }
}