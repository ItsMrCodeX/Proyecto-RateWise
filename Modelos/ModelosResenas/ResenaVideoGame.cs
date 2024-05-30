using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos.ModelosResenas
{
    public class ResenaVideoGame
    {
        private int idResenaVideoGames;
        private double calificacion;
        private string alias;
        private string texto;
        private int idGame;
        private BitmapImage imagen;
        private string nombre;
        public int IdResenaVideoGames { get => idResenaVideoGames; set => idResenaVideoGames = value; }
        public double Calificacion { get => calificacion; set => calificacion = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Texto { get => texto; set => texto = value; }
        public int IdGame { get => idGame; set => idGame = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }
        public string Nombre { get => nombre; set => nombre = value; }

        public ResenaVideoGame(int idResenaVideoGames, double calificacion, string alias, string texto, int idGame)
        {
            IdResenaVideoGames = idResenaVideoGames;
            Calificacion = calificacion;
            Alias = alias;
            Texto = texto;
            IdGame = idGame;

        }
        public ResenaVideoGame()
        {

        }
    }
}
