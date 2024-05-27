namespace Proyecto_Integrador.Globales
{
    public static class Global
    {
        public static string UsuarioActual { get; set; }
        public static int IdAResenar { get; set; } // Id de lo que se desea resenar
        public static int TipoResenar { get; set; } // Tipo de cosa que se va a resenar 1-Lugar 2-Entretenimiento 3-Videojuegos

        private static string AdminAutorizado = "ItsMrCodeX";

        public static bool VerificarAdmin (string user)
        {
            if (user == AdminAutorizado)
                return true;
            else 
                return false;
        }
    }
}
