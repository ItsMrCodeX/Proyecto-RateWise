using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos.ModelosResenas
{
    public class ResenaEntretenimiento
    {
        private int id;
        private double calificacion;
        private string alias;
        private string texto;
        private int identreten;
        private BitmapImage imagen;
        private string nombre;

        public int Id { get => id; set => id = value; }
        public double Calificacion { get => calificacion; set => calificacion = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Texto { get => texto; set => texto = value; }
        public int IdEntreten { get => identreten; set => identreten = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public ResenaEntretenimiento(int id, double calificacion, string alias, string texto, int idLugar)
        {
            Id = id;
            Calificacion = calificacion;
            Alias = alias;
            Texto = texto;
            IdEntreten = idLugar;

        }
        public ResenaEntretenimiento()
        {

        }
    }
}
