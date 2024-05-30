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
    /// Lógica de interacción para VerInfoEntretenimiento.xaml
    /// </summary>
    public partial class VerInfoEntretenimiento : UserControl
    {
        private ServicioEntretenimiento controlEntreten;
        public Videojuegos Juego { get; set; }
        public int idDelJuego;
        public VerInfoEntretenimiento(int idDelEntreten)
        {
            InitializeComponent();
            VerificarPermisos();
            idDelJuego = idDelEntreten;
            VaciarGlobalesResenas();
            CargarInfo(idDelEntreten);
        }
        public void CargarInfo(int entreten)
        {
            controlEntreten = new ServicioEntretenimiento();
            List<Entretenimiento> entretenimientos = controlEntreten.getEntretenimientoPorId(entreten);

            idJuego.Content = entreten;

            ResenaEntretenimiento ultima = controlEntreten.UltimaResena(entreten);
            double prom = controlEntreten.ObtenerPromedioResenas(entreten);
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

            if (entretenimientos.Count > 0)
            {
                Entretenimiento entretenimiento = entretenimientos[0];
                txtTitulo.Text = entretenimiento.Nombre;
                txbDescripcion.Text = entretenimiento.Descripcion;
                txtGenero.Content = controlEntreten.getGenero(entretenimiento.IdGeneroEntreten);
                txtPlataforama.Content = controlEntreten.getPlataforma(entretenimiento.IdPlataformaEntreten);
                txtTipo.Content = controlEntreten.getTipo(entretenimiento.IdTipoEntreten);
                txtFecha.Text = entretenimiento.FechaEstreno.ToString();

                try
                {

                    var imageBrush = BordeImagenLugar.Background as ImageBrush;
                    string base64String = entretenimiento.Poster;

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

            DataContext = entretenimientos;
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
        public bool VerificarPermisos()
        {

            string user = Global.UsuarioActual;
            if (Global.VerificarAdmin(user))
            {
                stckAdmins.Visibility = Visibility.Visible;
            }
            else
            {
                stckAdmins.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(user))
            {
                return false;
            }
            return true;
        }

        public bool VerificarResena()
        {
            ServicioEntretenimiento verificador = new ServicioEntretenimiento();
            if (verificador.VerificarResenaPrevia(Global.UsuarioActual, idDelJuego))
            {
                return true;
            }
            return false;
        }

        public void VaciarGlobalesResenas()
        {
            Global.TipoResenar = 0;
            Global.IdAResenar = 0;
        }

        private void btnresenar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarResena())
            {
                if (VerificarUsuario() == true)
                {
                    MainWindow main = new MainWindow();
                    // Cambiar esto
                    main.ResenarEntretenimiento(Convert.ToInt16(idJuego.Content), txtTitulo.Text);
                }
                else if (VerificarUsuario() == false)
                {
                    MainWindow main = new MainWindow();
                    Global.TipoResenar = 3;
                    Global.IdAResenar = idDelJuego;
                    main.IrAIniciar();
                }
            }
            else
            {
                MessageBox.Show("Ya has reseñado este juego");
            }

        }


        private void BorrarCnPermiso_Click_1(object sender, RoutedEventArgs e)
        {
            if (VerificarPermisos())
            {
                if (controlEntreten.Borrar(idDelJuego))
                {
                    MessageBox.Show("Borrado Con Exito!");
                    MainWindow main = new MainWindow();
                    main.VerEntretenimiento();
                }
                else
                {
                    MessageBox.Show("Revisa que este lugar no tenga Reseñas!");
                }


            }
            else
            {
                MessageBox.Show("Como es que ves esto?");
                MainWindow main = new MainWindow();
                main.AbrirVerInfoJuego(idDelJuego);
            }
        }

        private void BorrarRsCnPermiso_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarPermisos())
            {
                if (controlEntreten.BorrarResenas(idDelJuego))
                {
                    MessageBox.Show("Reseñas Borradas Con Exito!");

                }
                else
                {
                    MessageBox.Show("No se encontraron Reseñas! \n A ocurrido un error");
                }


            }
            else
            {
                MessageBox.Show("Como es que ves esto?");
                MainWindow main = new MainWindow();
                main.AbrirVerEntretenimiento(idDelJuego);
            }
        }

        private void ActualizarCnPermiso_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.ActualizarEntretenimiento(idDelJuego);
        }

        private void verResenasGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.VerResenasEntretenimiento(idDelJuego);
        }
    }
}
