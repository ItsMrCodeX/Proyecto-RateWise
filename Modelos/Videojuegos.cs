using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos
{
    public class Videojuegos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
        public int IdPlataforma { get; set; }
        public int IdGeneroVid { get; set; }
        public int IdPlataformaDescarga { get; set; }
        public BitmapImage Imagen { get; set; }
        public Videojuegos()
        {
        }

        public Videojuegos(int id, string nombre, string descripcion, DateTime fechaEstreno, string poster, int idPlataforma, int idGeneroVid, int idPlataformaDescarga)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaEstreno = fechaEstreno;
            Poster = poster;
            IdPlataforma = idPlataforma;
            IdGeneroVid = idGeneroVid;
            IdPlataformaDescarga = idPlataformaDescarga;
        }
    }
}
