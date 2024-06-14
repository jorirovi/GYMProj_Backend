namespace gymAPI.Comunes.Classes.Contracts
{
    public class PerfilUTDOContract
    {
        public string? Id { get; set; }
        public string? idUsuario { get; set;}
        public UsuarioTDOContract? datosUsuario { get; set; }
        public int edad { get; set; }
        public double peso { get; set; }
        public string? sexo { get; set; }
    }
}