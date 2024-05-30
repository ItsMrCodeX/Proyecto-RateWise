using Microsoft.Win32;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.VistasExtra
{
    /// <summary>
    /// Lógica de interacción para InfoUsuario.xaml
    /// </summary>
    public partial class InfoUsuario : UserControl
    {
        public InfoUsuario()
        {
            InitializeComponent();
            cargarInformacion();
            stckbtnVerResenas.IsEnabled = true;
            stckbtnGuardar.IsEnabled = false;
            stckLeft.IsEnabled = false;
            stckRight.IsEnabled = false;
            stckbtnVerResenas.IsEnabled = true;
            AgregarImagen.Visibility = Visibility.Collapsed;
        }

        public void cargarInformacion()
        {
            ServicioUsuarios cargar = new ServicioUsuarios();
            Usuarios actual = cargar.InfoUsuario(Global.UsuarioActual);

            if (actual != null)
            {
                txtAlias.Text = actual.Alias;
                txtNombre.Text = actual.Nombre;
                txtAPaterno.Text = actual.Apaterno;
                txtAMaterno.Text = actual.Amaterno;
                txtCorreo.Text = actual.Correo;
                txtContraseña.Password = actual.Secreto;

                if (actual.Avatar != null)
                {
                    ImagenUsuario.Source = actual.Imagen;
                }

            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            stckbtnGuardar.IsEnabled = true;
            stckLeft.IsEnabled = true;
            stckRight.IsEnabled = true;
            stckbtnVerResenas.IsEnabled = true;
            AgregarImagen.Visibility = Visibility.Visible;
        }

        private void AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Todos los archivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Obtén la ruta del archivo seleccionado
                string rutaImagen = openFileDialog.FileName;

                // Carga la imagen en tu control Image
                ImagenUsuario.Source = new BitmapImage(new Uri(rutaImagen));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ServicioUsuarios guardar = new ServicioUsuarios();
            Usuarios usuarios = new Usuarios();
            if (txtAlias.Text.Equals(Global.UsuarioActual, StringComparison.OrdinalIgnoreCase))
            {
                usuarios.Alias = Global.UsuarioActual;
                usuarios.Nombre = txtNombre.Text;
                usuarios.Amaterno = txtAMaterno.Text;
                usuarios.Apaterno = txtAPaterno.Text;
                usuarios.Correo = txtCorreo.Text;
                usuarios.Secreto = txtContraseña.Password;

                string imagenString = ConvertImageToBase64(ImagenUsuario);
                usuarios.Avatar = imagenString;

                bool res = guardar.ActualizarMenosAlias(usuarios);
                if (res)
                {
                    MessageBox.Show("Usuario Guardado Correctamente");
                    InfoUsuario mainWindow = new InfoUsuario();
                    mainWindow.cargarInformacion();
                }
            }
            else
            {
                MessageBox.Show("Por el momento no podemos actualizar tu Usuario");
                if (guardar.UsuarioDisponible(txtAlias.Text) == true)
                {
                    txtAlias.Text = Global.UsuarioActual;
                    usuarios.Alias = Global.UsuarioActual;
                    usuarios.Nombre = txtNombre.Text;
                    usuarios.Amaterno = txtAMaterno.Text;
                    usuarios.Apaterno = txtAPaterno.Text;
                    usuarios.Correo = txtCorreo.Text;
                    usuarios.Secreto = txtContraseña.Password;

                    string imagenString = ConvertImageToBase64(ImagenUsuario);
                    usuarios.Avatar = imagenString;

                    bool res = guardar.ActualizarMenosAlias(usuarios);
                    if (res)
                    {
                        MessageBox.Show("Usuario Guardado Correctamente");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.VerInfoUsuario();
                    }

                }
                else
                {
                    MessageBox.Show("Usuario NO disponible");
                    usuarios.Alias = Global.UsuarioActual;
                }
            }



        }

        private string ConvertImageToBase64(System.Windows.Controls.Image image)
        {
            BitmapSource bitmapSource = image.Source as BitmapSource;

            if (bitmapSource == null)
            {
                return null;
            }

            byte[] binaryData;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                binaryData = stream.ToArray();
            }

            // Convierte el arreglo de bytes a una cadena Base64
            string base64String = Convert.ToBase64String(binaryData);

            return base64String;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Global.UsuarioActual = "";
            Global.TipoResenar = -1;
            Global.IdAResenar = -1;
            MessageBox.Show("Sesion Cerrada");
            MainWindow main = new MainWindow();
            main.ActualizaUsuario();
            main.VerInicio();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.VerResenasdeUsuario();
        }
    }
}

