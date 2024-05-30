using Microsoft.Win32;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.VistasExtra
{
    /// <summary>
    /// Lógica de interacción para RegistrarEntretenimiento.xaml
    /// </summary>
    public partial class RegistrarEntretenimiento : UserControl
    {
        string imagen;
        public RegistrarEntretenimiento()
        {
            InitializeComponent();
            LlenarCMBS();
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

        public void GuardarLugar()
        {
            ServicioEntretenimiento Registro = new ServicioEntretenimiento();
            Entretenimiento game = new Entretenimiento();


            if (string.IsNullOrEmpty(DateEstreno.SelectedDate.ToString()) == false || string.IsNullOrEmpty(txbNombre.Text)
                || string.IsNullOrEmpty(txbDescripcion.Text) || cmbPlataforma.SelectedIndex != -1
                || cmbGenero.SelectedIndex != -1 || cmbTipo.SelectedIndex != -1 || string.IsNullOrEmpty(imagen))
            {
                string originalDate = DateEstreno.SelectedDate.ToString();
                string fechaSinHora = originalDate.Substring(0, 10);
                DateTime dt = DateTime.ParseExact(fechaSinHora, "dd/MM/yyyy", null);
                string newDate = dt.ToString("yyyy-MM-dd"); // Resultado: "2021-03-08"


                var x = Convert.ToDateTime(newDate);

                game.Nombre = txbNombre.Text;
                game.Descripcion = txbDescripcion.Text;
                game.Poster = imagen;
                game.FechaEstreno = Convert.ToDateTime(newDate);
                game.IdGeneroEntreten = cmbGenero.SelectedIndex + 1;
                game.IdPlataformaEntreten = cmbPlataforma.SelectedIndex + 1;
                game.IdTipoEntreten = cmbTipo.SelectedIndex + 1;

                bool res = Registro.Insertar(game);
                if (res == false)
                {
                    string fotografiaPreview = game.Poster != null && game.Poster.Length >= 10
                    ? game.Poster.Substring(0, 10)
                    : game.Poster;
                    MessageBox.Show(
                        $"Nombre: {game.Nombre}\n" +
                        $"Descripción: {game.Descripcion}\n" +
                        $"Poster: {fotografiaPreview}\n" +
                        $"ID Genero: {game.IdGeneroEntreten}\n" +
                        $"ID Plataforma: {game.IdPlataformaEntreten}\n" +
                        $"ID Tipo: {game.IdTipoEntreten}"
                        );


                }
                else if (res == true)
                {
                    MessageBox.Show("Agregado Correctamente!");
                    MainWindow main = new MainWindow();
                    main.VerEntretenimiento();
                }



            }
            else
            {
                MessageBox.Show("Todos los datos son requeridos \nRevisa que todo este llenado correctamente");
            }

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
    }
}
