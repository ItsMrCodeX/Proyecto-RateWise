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
    /// Lógica de interacción para RegistrarLugar.xaml
    /// </summary>
    public partial class RegistrarLugar : UserControl
    {
        public string imagen;
        public RegistrarLugar()
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

        public void GuardarLugar()
        {
            ServiciosLugar Registro = new ServiciosLugar();
            Lugar lugar = new Lugar();

            if (string.IsNullOrEmpty(txbNombre.Text) || string.IsNullOrEmpty(txbDireccion.Text) || string.IsNullOrEmpty(txtLatitud.Text)
                || string.IsNullOrEmpty(txtLongitud.Text) || string.IsNullOrEmpty(txbDescripcion.Text) || string.IsNullOrEmpty(imagen)
                || cmbTipoLugar.SelectedIndex == -1 || cmbRegion.SelectedIndex == -1 || string.IsNullOrEmpty(txtcontacto.Text))
            {
                MessageBox.Show("Todos los campos son requeridos \nRevisa que todo este lleno correctamente");
            }
            else
            {


                bool resconv;
                try
                {
                    lugar.Latitud = Convert.ToDouble(txtLatitud.Text);
                    lugar.Longitud = Convert.ToDouble(txtLongitud.Text);
                    lugar.Contacto = Convert.ToInt64(txtcontacto.Text);
                    resconv = true;
                }
                catch
                {
                    MessageBox.Show("Los campos Latitud,Longitud tienen que ser numero \nContacto tiene que ser numero entero");
                    resconv = false;
                }
                if (resconv == true)
                {
                    lugar.Nombre = txbNombre.Text;
                    lugar.Direccion = txbDireccion.Text;
                    lugar.Descripcion = txbDescripcion.Text;
                    lugar.Fotografia = imagen;
                    lugar.IdRegion = cmbRegion.SelectedIndex + 1;
                    lugar.IdTipoLugar = cmbTipoLugar.SelectedIndex + 1;
                    bool res = Registro.Insertar(lugar);
                    if (res == false)
                    {
                        string fotografiaPreview = lugar.Fotografia != null && lugar.Fotografia.Length >= 10
                        ? lugar.Fotografia.Substring(0, 10)
                        : lugar.Fotografia;
                        MessageBox.Show(
                            $"Nombre: {lugar.Nombre}\n" +
                            $"Dirección: {lugar.Direccion}\n" +
                            $"Latitud: {lugar.Latitud}\n" +
                            $"Longitud: {lugar.Longitud}\n" +
                            $"Descripción: {lugar.Descripcion}\n" +
                            $"Fotografía: {fotografiaPreview}\n" +
                            $"ID Región: {lugar.IdRegion}\n" +
                            $"ID Tipo de Lugar: {lugar.IdTipoLugar}"
                            );


                    }
                    else if (res == true)
                    {
                        MessageBox.Show("Lugar Agregado Correctamente!");
                        MainWindow main = new MainWindow();
                        main.VerLugares();
                    }


                }


            }
        }

        public void LlenarCMBS()
        {
            ServiciosLugar lugar = new ServiciosLugar();
            List<string> regiones = lugar.MostrarRegiones();
            List<string> tipolugar = lugar.MostrarTipoLugar();

            cmbRegion.ItemsSource = regiones;
            cmbTipoLugar.ItemsSource = tipolugar;
        }

        private void AgregarImagen_Copiar_Click(object sender, RoutedEventArgs e)
        {
            GuardarLugar();
        }
    }
}
