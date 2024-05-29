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
    }
}
