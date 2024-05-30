using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos
{
    public class Lugar
    {
        private int id;
        private string nombre;
        private string descripcion;
        private string direccion;
        private Int64 contacto;
        private double latitud;
        private double longitud;
        private string fotografia;
        private int idTipoLugar;
        private int idRegion;
        private BitmapImage imagen;


        public Lugar()
        {
        }

        public Lugar(int id, string nombre, string descripcion, string direccion, Int64 contacto, double latitud, double longitud, string fotografia, int idTipoLugar, int idRegion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Direccion = direccion;
            Contacto = contacto;
            Latitud = latitud;
            Longitud = longitud;
            Fotografia = fotografia;
            IdTipoLugar = idTipoLugar;
            IdRegion = idRegion;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public Int64 Contacto { get => contacto; set => contacto = value; }
        public double Latitud { get => latitud; set => latitud = value; }
        public double Longitud { get => longitud; set => longitud = value; }
        public string Fotografia { get => fotografia; set => fotografia = value; }
        public int IdTipoLugar { get => idTipoLugar; set => idTipoLugar = value; }
        public int IdRegion { get => idRegion; set => idRegion = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }
    }
}
