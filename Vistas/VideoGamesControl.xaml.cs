using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Vistas
{
    /// <summary>
    /// Lógica de interacción para VideoGamesControl.xaml
    /// </summary>
    public partial class VideoGamesControl : UserControl
    {
        public List<Videojuegos> Videojuegos { get; set; }

        private ServicioVideogames controlVideojuegos;
        public VideoGamesControl()
        {
            InitializeComponent();
            LlenarCMBS();
            LlenarLista();

        }

        private void LlenarLista()
        {
            controlVideojuegos = new ServicioVideogames();
            List<Videojuegos> videojuegos = controlVideojuegos.getVideojuegos();

            foreach (Videojuegos juego in videojuegos)
            {
                if (!string.IsNullOrEmpty(juego.Poster))
                {
                    try
                    {
                        string base64String = juego.Poster;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        juego.Imagen = bi;
                        double calid = Convert.ToDouble(controlVideojuegos.ObtenerPromedioResenas(juego.Id));
                        if (calid > 0)
                        {
                            juego.Calificacion = calid;
                        }
                        else
                        {
                            juego.Calificacion = null;
                        }
                        juego.Estrellas = ObtenerEstrellas(calid);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar imagen para {juego.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"La fotografía está vacía o es nula para {juego.Nombre}.");
                }
            }

            MiListBox.ItemsSource = videojuegos;
        }

        private void MiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            Videojuegos juegos = (Videojuegos)listBox.SelectedItem;
            if (juegos != null)
            {
                int id = juegos.Id;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.AbrirVerInfoJuego(id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.RegistrarVideojuego();
        }

        public void LlenarListaFiltrada()
        {
            string? nombreVideojuego = !string.IsNullOrEmpty(txbBusqueda.Text) ? txbBusqueda.Text : null;
            int? plataforma = cmbPlataforma.SelectedIndex != -1 ? (int?)(cmbPlataforma.SelectedIndex + 1) : null;
            int? genero = cmbGenero.SelectedIndex != -1 ? (int?)(cmbGenero.SelectedIndex + 1) : null;
            int? plataformaDescarga = cmbDescarga.SelectedIndex != -1 ? (int?)(cmbDescarga.SelectedIndex + 1) : null;
            ServicioVideogames controlVideojuegos = new ServicioVideogames();
            List<Videojuegos> videojuegos;

            videojuegos = controlVideojuegos.getVideojuegosFiltrado(plataforma, genero, plataformaDescarga, nombreVideojuego);

            if (videojuegos == null || videojuegos.Count == 0)
            {
                MessageBox.Show("No se encontraron videojuegos con los filtros especificados.");
                return;
            }

            foreach (Videojuegos videojuego in videojuegos)
            {
                if (!string.IsNullOrEmpty(videojuego.Poster))
                {
                    try
                    {
                        string base64String = videojuego.Poster;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        videojuego.Imagen = bi;
                        double calid = Convert.ToDouble(controlVideojuegos.ObtenerPromedioResenas(videojuego.Id));
                        if (calid > 0)
                        {
                            videojuego.Calificacion = calid;
                        }
                        else
                        {
                            videojuego.Calificacion = null;
                        }
                        videojuego.Estrellas = ObtenerEstrellas(calid);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar poster para {videojuego.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"El poster está vacío o es nulo para {videojuego.Nombre}.");
                }
            }

            MiListBox.ItemsSource = videojuegos;
        }

        public void LlenarCMBS()
        {
            ServicioVideogames game = new ServicioVideogames();
            List<string> genero = game.MostrarGenero();
            List<string> plataforma = game.MostrarPlataforma();
            List<string> descarga = game.MostrarDescarga();

            
            cmbGenero.ItemsSource = genero;
            cmbPlataforma.ItemsSource = plataforma;
            cmbDescarga.ItemsSource = descarga;
        }

        private void btbVaciar_Click(object sender, RoutedEventArgs e)
        {
            txbBusqueda.Text = string.Empty;
            cmbGenero.SelectedIndex = -1;
            cmbDescarga.SelectedIndex = -1;
            cmbPlataforma.SelectedIndex = -1;
            LlenarLista();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            LlenarListaFiltrada();
        }

        private List<string> ObtenerEstrellas(double calificacion)
        {
            List<string> estrellas = new List<string>();

            if (calificacion == 0)
            {

                return estrellas;
            }

            int estrellasCompletas = (int)calificacion;
            bool mediaEstrella = calificacion - estrellasCompletas >= 0.1;
            int estrellasVacias = 5 - estrellasCompletas - (mediaEstrella ? 1 : 0);

            for (int i = 0; i < estrellasCompletas; i++)
            {
                estrellas.Add("\uf005");
            }
            if (mediaEstrella)
            {
                estrellas.Add("\uf5c0");
            }
            for (int i = 0; i < estrellasVacias; i++)
            {

            }

            return estrellas;
        }

    }
}
