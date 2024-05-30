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
    /// Lógica de interacción para ActualizarEntretenimiento.xaml
    /// </summary>
    public partial class ActualizarEntretenimiento : UserControl
    {
        string imagen;
        int idJuego;
        public ActualizarEntretenimiento(int idEntreten)
        {
            InitializeComponent();
            idJuego = idEntreten;
            LlenarCMBS();
            CargarDatos(idEntreten);
        }

        public void LlenarCMBS()
        {
            ServicioEntretenimiento movie = new ServicioEntretenimiento();
            List<string> genero = movie.MostrarGenero();
            List<string> plataforma = movie.MostrarPlataforma();
            List<string> tipo = movie.MostrarTipo();

            cmbTipo.ItemsSource = tipo;
            cmbGenero.ItemsSource = genero;
            cmbPlataforma.ItemsSource = plataforma;
        }

        public void CargarDatos(int GameACambiar)
        {
            ServicioEntretenimiento servicioEntretenimiento = new ServicioEntretenimiento();

            List<Entretenimiento> entretenimientos = servicioEntretenimiento.getEntretenimientoPorId(GameACambiar);
            Entretenimiento movie = entretenimientos[0];

            txbNombre.Text = movie.Nombre;
            imagen = movie.Poster;
            txbDescripcion.Text = movie.Descripcion;
            DateEstreno.SelectedDate = movie.FechaEstreno;
            cmbTipo.SelectedIndex = movie.IdTipoEntreten - 1;
            cmbGenero.SelectedIndex = movie.IdGeneroEntreten - 1;
            cmbPlataforma.SelectedIndex = movie.IdPlataformaEntreten - 1;


            byte[] binaryData = Convert.FromBase64String(movie.Poster);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            movie.Imagen = bi;
            ImagenLugar.ImageSource = movie.Imagen;
        }

        public void GuardarLugar()
        {
            ServicioEntretenimiento Registro = new ServicioEntretenimiento();
            Entretenimiento movie = new Entretenimiento();


            if (string.IsNullOrEmpty(DateEstreno.SelectedDate.ToString()) == false || string.IsNullOrEmpty(txbNombre.Text)
                || string.IsNullOrEmpty(txbDescripcion.Text) || cmbPlataforma.SelectedIndex != -1
                || cmbGenero.SelectedIndex != -1 || cmbTipo.SelectedIndex != -1 || string.IsNullOrEmpty(imagen))
            {
                string originalDate = DateEstreno.SelectedDate.ToString();
                string fechaSinHora = originalDate.Substring(0, 10);
                DateTime dt = DateTime.ParseExact(fechaSinHora, "dd/MM/yyyy", null);
                string newDate = dt.ToString("yyyy-MM-dd"); // Resultado: "2021-03-08"

                movie.Id = idJuego;
                movie.Nombre = txbNombre.Text;
                movie.Descripcion = txbDescripcion.Text;
                movie.Poster = imagen;
                movie.FechaEstreno = Convert.ToDateTime(newDate);
                movie.IdGeneroEntreten = cmbGenero.SelectedIndex + 1;
                movie.IdPlataformaEntreten = cmbPlataforma.SelectedIndex + 1;
                movie.IdTipoEntreten = cmbTipo.SelectedIndex + 1;

                bool res = Registro.Actualizar(movie);
                if (res == false)
                {
                    string fotografiaPreview = movie.Poster != null && movie.Poster.Length >= 10
                    ? movie.Poster.Substring(0, 10)
                    : movie.Poster;
                    MessageBox.Show(
                        $"Nombre: {movie.Nombre}\n" +
                        $"Descripción: {movie.Descripcion}\n" +
                        $"Poster: {fotografiaPreview}\n" +
                        $"ID Genero: {movie.IdGeneroEntreten}\n" +
                        $"ID Plataforma: {movie.IdPlataformaEntreten}\n" +
                        $"ID Tipo: {movie.IdTipoEntreten}"
                        );


                }
                else if (res == true)
                {
                    MessageBox.Show("Editado Correctamente!");
                    MainWindow main = new MainWindow();
                    main.VerEntretenimiento();
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
