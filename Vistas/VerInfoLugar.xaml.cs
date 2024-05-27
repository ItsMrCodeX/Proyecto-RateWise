using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Vistas
{
    /// <summary>
    /// Lógica de interacción para VerInfoLugar.xaml
    /// </summary>
    public partial class VerInfoLugar : UserControl
    {
        private ServiciosLugar controlLugares;
        public Lugar Lugar { get; set; }
        public int idDelLugar;
        public double lat, lon;
        public VerInfoLugar(int lugar)
        {
            InitializeComponent();
            idDelLugar = lugar;
            VaciarGlobalesResenas();
            CargarInfo(lugar);


        }

        public void CargarInfo(int lugar)
        {
            controlLugares = new ServiciosLugar();
            List<Lugar> lugares = controlLugares.getLugarePorId(lugar);

            idLugar.Content = lugar;

            ResenaLugares ultima = controlLugares.UltimaResena(lugar);
            double prom = controlLugares.ObtenerPromedioResenas(lugar);
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

            if (lugares.Count > 0)
            {
                Lugar place = lugares[0];
                txtNombre.Text = place.Nombre;
                lat = place.Latitud;
                lon = place.Longitud;
                txbDescripcion.Text = place.Descripcion;
                txbContacto.Text = place.Contacto.ToString();
                txbTipo.Text = controlLugares.getTipoLugar(place.IdTipoLugar);
                //txb_Region.Text = controlLugares.getRegion(place.IdRegion);

                try
                {

                    var imageBrush = BordeImagenLugar.Background as ImageBrush;
                    string base64String = place.Fotografia;

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

            DataContext = lugares;
        }

        public void VaciarGlobalesResenas()
        {
            Global.TipoResenar = 0;
            Global.IdAResenar = 0;
        }
        public bool VerificarUsuario()
        {

            string user = Global.UsuarioActual;

            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            return true;
        }


        private void verenmapa_Click(object sender, RoutedEventArgs e)
        {
            string latitud = lat.ToString();
            string longitud = lon.ToString();
            string url = $"https://www.google.com/maps/?q={latitud},{longitud}";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.VerResenasLugar(idDelLugar);
        }

        private void btnresenar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarUsuario() == true)
            {
                MainWindow main = new MainWindow();
                main.ResenarLugar(Convert.ToInt16(idLugar.Content), txtNombre.Text);
            }
            else if (VerificarUsuario() == false)
            {
                MainWindow main = new MainWindow();
                Global.TipoResenar = 1;
                Global.IdAResenar = idDelLugar;
                main.IrAIniciar();
            }
        }

    }
}
