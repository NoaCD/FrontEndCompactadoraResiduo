namespace CreativeReduction.Model.DTOS
{
    public class LoginerDTO
    {
        public string? cNombreUsuario;
        public string? cContrasenia;

        public LoginerDTO()
        { }

        ~LoginerDTO()
        {
        }

        public string? Usuario
        { get { return cNombreUsuario; } set { cNombreUsuario = value; } }
        public string? Password
        { get { return cContrasenia; } set { cContrasenia = value; } }
    }
}