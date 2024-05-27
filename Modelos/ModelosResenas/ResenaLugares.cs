using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos.ModelosResenas
{
    public class ResenaLugares
    {
        private int idResenaLugares;
        private double calificacion;
        private string alias;
        private string texto;
        private int idLugar;
        private BitmapImage imagen;


        public int IdResenaLugares { get => idResenaLugares; set => idResenaLugares = value; }
        public double Calificacion { get => calificacion; set => calificacion = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Texto { get => texto; set => texto = value; }
        public int IdLugar { get => idLugar; set => idLugar = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }

        public ResenaLugares(int idResenaLugares, double calificacion, string alias, string texto, int idLugar)
        {
            IdResenaLugares = idResenaLugares;
            Calificacion = calificacion;
            Alias = alias;
            Texto = texto;
            IdLugar = idLugar;

        }
        public ResenaLugares()
        {

        }
    }
}
