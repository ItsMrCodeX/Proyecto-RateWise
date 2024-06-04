using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos
{
    public class Entretenimiento
    {
        private int id;
        private string nombre;
        private string descripcion;
        private DateTime fechaEstreno;
        private string poster;
        private int idTipoEntreten;
        private int idGeneroEntreten;
        private int idPlataformaEntreten;
        private BitmapImage imagen;
        private double? calificacion;
        private List<string> estrellas;

        public Entretenimiento()
        {

        }

        public Entretenimiento(int id, string nombre, string descripcion, DateTime fechaEstreno, string poster, int idTipoEntreten, int idGeneroEntreten, int idPlataformaEntreten)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaEstreno = fechaEstreno;
            Poster = poster;
            IdTipoEntreten = idTipoEntreten;
            IdGeneroEntreten = idGeneroEntreten;
            IdPlataformaEntreten = idPlataformaEntreten;
        }

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaEstreno { get => fechaEstreno; set => fechaEstreno = value; }
        public string Poster { get => poster; set => poster = value; }
        public int IdTipoEntreten { get => idTipoEntreten; set => idTipoEntreten = value; }
        public int IdGeneroEntreten { get => idGeneroEntreten; set => idGeneroEntreten = value; }
        public int IdPlataformaEntreten { get => idPlataformaEntreten; set => idPlataformaEntreten = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }
        public double? Calificacion { get => calificacion; set => calificacion = value; }
        public List<string> Estrellas { get => estrellas; set => estrellas = value; }
    }

}
