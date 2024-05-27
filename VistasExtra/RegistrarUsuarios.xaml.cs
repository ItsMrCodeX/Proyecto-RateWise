using Microsoft.Win32;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Proyecto_Integrador.VistasExtra
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuarios.xaml
    /// </summary>
    public partial class RegistrarUsuarios : UserControl
    {
        public RegistrarUsuarios()
        {
            InitializeComponent();
        }
        string imagen;

        private void AgregarImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Seleccionar imagen";
                openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    string rutaImagenSeleccionada = openFileDialog.FileName;

                    imagen = ConvertirImagenABase64(rutaImagenSeleccionada);

                }
                AgregarImagen.Foreground = Brushes.Black;

                AgregarImagen.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen");
                AgregarImagen.Foreground = Brushes.White;
                AgregarImagen.IsEnabled = true;
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
                AgregarImagen.IsEnabled = false;
            }

            return imagenBase64;
        }

        private void GuardarUsuario()
        {
            ServicioUsuarios Registro = new ServicioUsuarios();
            Usuarios usuario = new Usuarios();

            if (txtAlias.Text == string.Empty || txtNombre.Text == string.Empty || txtApellidoPaterno.Text == string.Empty ||
                txtApellidoMaterno.Text == string.Empty || txtCorreo.Text == string.Empty || imagen == string.Empty ||
                imagen == "" || txtContraseña.Password == string.Empty || imagen == null)
            {
                MessageBox.Show("Todos los campos son requeridos \nRevisa que todo este lleno correctamente");
            }
            else
            {
                if (Registro.UsuarioDisponible(txtAlias.Text) == true)
                {
                    usuario.Alias = txtAlias.Text;
                    usuario.Nombre = txtNombre.Text;
                    usuario.Apaterno = txtApellidoPaterno.Text;
                    usuario.Amaterno = txtApellidoMaterno.Text;
                    usuario.Correo = txtCorreo.Text;
                    usuario.Avatar = imagen;
                    usuario.Secreto = txtContraseña.Password;
                    bool res = Registro.Insertar(usuario);

                    if (res == true)
                    {
                        Global.UsuarioActual = txtAlias.Text;
                        MessageBox.Show("Usuario guardado con exito!");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.VerLugares();
                    }
                    else if (res == false)
                    {
                        MessageBox.Show("Hubo un error al guardar el usuario \nRevisa tus datos");
                    }

                }
                else if (Registro.UsuarioDisponible(txtAlias.Text) == false)
                {
                    MessageBox.Show("Ese nombre de usuario ya esta en uso!");
                }
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GuardarUsuario();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.IrAIniciar();
        }
    }
}
