using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Integrador.Vistas
{
    /// <summary>
    /// Lógica de interacción para VerInfoGame.xaml
    /// </summary>
    public partial class VerInfoGame : UserControl
    {
        private ServicioVideogames controlGames;
        public Videojuegos Juego { get; set; }
        public int idDelJuego;
        public VerInfoGame(int idDelGame)
        {
            InitializeComponent();
            idDelJuego = idDelGame;
            VaciarGlobalesResenas();
            CargarInfo(idDelGame);

        }

        public void CargarInfo(int juego)
        {
            controlGames = new ServicioVideogames();
            List<Videojuegos> videojuegos = controlGames.getVideojuegosPorId(juego);

            idJuego.Content = juego;

            ResenaVideoGame ultima = controlGames.UltimaResena(juego);
            double prom = controlGames.ObtenerPromedioResenas(juego);
            txbCalPromedio.Text = prom.ToString();

            if (ultima == null || string.IsNullOrEmpty(ultima.Alias))
            {
                stckResenas.Visibility = Visibility.Collapsed;
                stckNoResenas.Visibility = Visibility.Visible;
                AliasUltima.Text = "";
                CalificacionUltima.Text = "";
                fotoperfil.Source = null;
            }
            else
            {
                stckResenas.Visibility = Visibility.Visible;
                stckNoResenas.Visibility = Visibility.Collapsed;
                AliasUltima.Text = ultima.Alias + ":";
                CalificacionUltima.Text = ultima.Calificacion.ToString();

                ServicioUsuarios usuario = new ServicioUsuarios();
                BitmapImage bit = usuario.ImagenUsuario(ultima.Alias);
                fotoperfil.Source = bit;
            }

            if (videojuegos.Count > 0)
            {
                Videojuegos game = videojuegos[0];
                txtTitulo.Text = game.Nombre;               
                txbDescripcion.Text = game.Descripcion;
                txtGenero.Content = controlGames.getGenero(game.IdGeneroVid);
                txtPlataforama.Content = controlGames.getPlataforma(game.IdPlataforma);
                txtDescarga.Content = controlGames.getDescarga(game.IdPlataformaDescarga);
                txtFecha.Text = game.FechaEstreno.ToString();

                try
                {

                    var imageBrush = BordeImagenLugar.Background as ImageBrush;
                    string base64String = game.Poster;

                    byte[] binaryData = Convert.FromBase64String(base64String);

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(binaryData);
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.EndInit();
                    bi.Freeze();

                    if (imageBrush != null)
                    {
                        imageBrush.ImageSource = bi;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar imagen: " + ex.Message);
                    //ImagenLugar.Source = null;
                }
            }

            DataContext = videojuegos;
        }

        public void VaciarGlobalesResenas()
        {
            Global.TipoResenar = 0;
            Global.IdAResenar = 0;
        }

        private void btnresenar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
