using Microsoft.Win32;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
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

namespace Proyecto_Integrador.VistasExtra.EditoresAdministradores
{
    /// <summary>
    /// Lógica de interacción para ActualizarLugar.xaml
    /// </summary>
    public partial class ActualizarLugar : UserControl
    {
        private int LugarCambio;
        public string imagen;
        public ActualizarLugar(int idLugarACambiar)
        {
            InitializeComponent();
            LugarCambio = idLugarACambiar;
            LlenarCMBS();
            CargarDatos();
            
            
        }

        public void CargarDatos()
        {
            ServiciosLugar serviciosLugar = new ServiciosLugar();

            List<Lugar> Lugares = serviciosLugar.getLugarePorId(LugarCambio);
            Lugar lugar = Lugares[0];

            txtcontacto.Text = lugar.Contacto.ToString();
            txbDescripcion.Text = lugar.Descripcion.ToString();
            txtLatitud.Text = lugar.Latitud.ToString();
            txtLongitud.Text= lugar.Longitud.ToString();
            txbDireccion.Text=lugar.Direccion.ToString();
            txbNombre.Text=lugar.Nombre.ToString();


            string string64 = lugar.Fotografia.Substring(10);
            byte[] binaryData = Convert.FromBase64String(lugar.Fotografia);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();
            lugar.Imagen = bi;
            ImagenLugar.ImageSource = lugar.Imagen;

            cmbRegion.SelectedIndex = lugar.IdRegion-1;
            cmbTipoLugar.SelectedIndex = lugar.IdTipoLugar-1;

        }

        public void LlenarCMBS()
        {
            ServiciosLugar lugar = new ServiciosLugar();
            List<string> regiones = lugar.MostrarRegiones();
            List<string> tipolugar = lugar.MostrarTipoLugar();

            cmbRegion.ItemsSource = regiones;
            cmbTipoLugar.ItemsSource = tipolugar;
        }
        public bool VerificarPermisos()
        {

            string user = Global.UsuarioActual;
            
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            return true;
        }
        public void GuardarLugar()
        {
            if (VerificarPermisos())
            {
                ServiciosLugar Actualizar = new ServiciosLugar();
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
                        lugar.Id = LugarCambio;
                        lugar.Nombre = txbNombre.Text;
                        lugar.Direccion = txbDireccion.Text;
                        lugar.Descripcion = txbDescripcion.Text;
                        lugar.Fotografia = imagen;
                        lugar.IdRegion = cmbRegion.SelectedIndex + 1;
                        lugar.IdTipoLugar = cmbTipoLugar.SelectedIndex + 1;
                        bool res = Actualizar.Actualizar(lugar);
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
                            MessageBox.Show("Lugar Actualizado Correctamente!");
                            MainWindow main = new MainWindow();
                            main.VerLugares();
                        }


                    }


                }
            }
            else
            {
                MessageBox.Show("Como es que ves esto??");
                MainWindow main = new MainWindow();
                main.VerLugares();

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
            

            return imagenBase64;
        }

        private void btnActualizarImagen_Click(object sender, RoutedEventArgs e)
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

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            GuardarLugar();
        }
    }

    

}
