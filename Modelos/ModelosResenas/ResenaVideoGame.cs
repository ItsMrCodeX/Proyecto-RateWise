using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos.ModelosResenas
{
    public class ResenaVideoGame
    {
        private int idResenaVideoGames;
        private double calificacion;
        private string alias;
        private string texto;
        private int idLugar;
        private BitmapImage imagen;

        public int IdResenaVideoGames { get => idResenaVideoGames; set => idResenaVideoGames = value; }
        public double Calificacion { get => calificacion; set => calificacion = value; }
        public string Alias { get => alias; set => alias = value; }
        public string Texto { get => texto; set => texto = value; }
        public int IdLugar { get => idLugar; set => idLugar = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }

        public ResenaVideoGame(int idResenaVideoGames, double calificacion, string alias, string texto, int idLugar)
        {
            IdResenaVideoGames = idResenaVideoGames;
            Calificacion = calificacion;
            Alias = alias;
            Texto = texto;
            IdLugar = idLugar;

        }
        public ResenaVideoGame()
        {

        }
    }
}
