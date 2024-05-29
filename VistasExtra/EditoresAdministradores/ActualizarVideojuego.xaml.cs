using Microsoft.Win32;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.VistasExtra.EditoresAdministradores
{
    /// <summary>
    /// Lógica de interacción para ActualizarVideojuego.xaml
    /// </summary>
    public partial class ActualizarVideojuego : UserControl
    {
        string imagen;
        int idJuego;
        public ActualizarVideojuego(int idGame)
        {

            InitializeComponent();
            idJuego = idGame;
            LlenarCMBS();
            CargarDatos(idGame);

        }

        public void LlenarCMBS()
        {
            ServicioVideogames game = new ServicioVideogames();
            List<string> genero = game.MostrarGenero();
            List<string> plataforma = game.MostrarPlataforma();
            List<string> descarga = game.MostrarDescarga();

            cmbDescarga.ItemsSource = descarga;
            cmbGenero.ItemsSource = genero;
            cmbPlataforma.ItemsSource = plataforma;
        }

        public void CargarDatos(int GameACambiar)
        {
            ServicioVideogames servicioVideogames = new ServicioVideogames();

            List<Videojuegos> Juegos = servicioVideogames.getVideojuegosPorId(GameACambiar);
            Videojuegos juego = Juegos[0];

            txbNombre.Text = juego.Nombre;
            imagen = juego.Poster;
            txbDescripcion.Text = juego.Descripcion;
            DateEstreno.SelectedDate = juego.FechaEstreno;
            cmbDescarga.SelectedIndex = juego.IdPlataformaDescarga - 1;
            cmbGenero.SelectedIndex = juego.IdGeneroVid - 1;
            cmbPlataforma.SelectedIndex = juego.IdPlataforma - 1;


            byte[] binaryData = Convert.FromBase64String(juego.Poster);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            juego.Imagen = bi;
            ImagenLugar.ImageSource = juego.Imagen;
        }

        public void GuardarLugar()
        {
            ServicioVideogames Registro = new ServicioVideogames();
            Videojuegos game = new Videojuegos();


            if (string.IsNullOrEmpty(DateEstreno.SelectedDate.ToString()) == false || string.IsNullOrEmpty(txbNombre.Text)
                || string.IsNullOrEmpty(txbDescripcion.Text) || cmbPlataforma.SelectedIndex != -1
                || cmbGenero.SelectedIndex != -1 || cmbDescarga.SelectedIndex != -1 || string.IsNullOrEmpty(imagen))
            {
                string originalDate = DateEstreno.SelectedDate.ToString();
                string fechaSinHora = originalDate.Substring(0, 10);
                DateTime dt = DateTime.ParseExact(fechaSinHora, "dd/MM/yyyy", null);
                string newDate = dt.ToString("yyyy-MM-dd"); // Resultado: "2021-03-08"


                MessageBox.Show(x.ToString());


                game.Id = idJuego;
                game.Nombre = txbNombre.Text;
                game.Descripcion = txbDescripcion.Text;
                game.Poster = imagen;
                game.FechaEstreno = Convert.ToDateTime(newDate);
                game.IdGeneroVid = cmbGenero.SelectedIndex + 1;
                game.IdPlataforma = cmbPlataforma.SelectedIndex + 1;
                game.IdPlataformaDescarga = cmbDescarga.SelectedIndex + 1;

                bool res = Registro.Actualizar(game);
                if (res == false)
                {
                    string fotografiaPreview = game.Poster != null && game.Poster.Length >= 10
                    ? game.Poster.Substring(0, 10)
                    : game.Poster;
                    MessageBox.Show(
                        $"Nombre: {game.Nombre}\n" +
                        $"Descripción: {game.Descripcion}\n" +
                        $"Poster: {fotografiaPreview}\n" +
                        $"ID Genero: {game.IdGeneroVid}\n" +
                        $"ID Plataforma: {game.IdPlataforma}\n" +
                        $"ID Descarga: {game.IdPlataformaDescarga}"
                        );


                }
                else if (res == true)
                {
                    MessageBox.Show("Videojuego Editado Correctamente!");
                    MainWindow main = new MainWindow();
                    main.VerVideoGames();
                }



            }
            else
            {
                MessageBox.Show("Todos los datos son requeridos \nRevisa que todo este llenado correctamente");
            }

        }

        private void AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Seleccionar imagen";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                string rutaImagenSeleccionada = openFileDialog.FileName;
                ImagenLugar.ImageSource = new BitmapImage(new Uri(rutaImagenSeleccionada));
                imagen = ConvertirImagenABase64(rutaImagenSeleccionada);

            }
        }
        private string ConvertirImagenABase64(string rutaImagen)
        {
            // Leer la imagen desde el archivo
            byte[] binaryData = File.ReadAllBytes(rutaImagen);

            // Convertir a base64
            string imagenBase64 = Convert.ToBase64String(binaryData);
            if (imagenBase64 == string.Empty)
            {
                MessageBox.Show("Error al cargar imagen");
            }
            else
            {
                btnagregar.IsEnabled = false;
            }

            return imagenBase64;
        }

        private void btnagregargame_Click(object sender, RoutedEventArgs e)
        {
            GuardarLugar();
        }
    }
}
