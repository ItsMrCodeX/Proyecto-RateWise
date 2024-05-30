using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.VistasExtra
{
    /// <summary>
    /// Lógica de interacción para VerMisResenas.xaml
    /// </summary>
    public partial class VerMisResenas : UserControl
    {
        public List<Lugar> Lugares { get; set; }
        private ServiciosLugar controlLugares;
        public List<Videojuegos> Videojuegos { get; set; }
        private ServicioVideogames controlVideojuegos;
        public List<Entretenimiento> Entretenimientos { get; set; }
        private ServicioEntretenimiento controlEntretenimiento;
        int IdDelLugar;
        public VerMisResenas()
        {
            InitializeComponent();
            cargarLugares();
            cargarVideojuego();
            cargarEntretenimiento();
        }

        public class ResenaInfo
        {
            public string Nombre { get; set; }
            public double Calificacion { get; set; }
            public string Texto { get; set; }
            public BitmapImage Imagen { get; set; }
        }


        public void cargarLugares()
        {
            controlLugares = new ServiciosLugar();
            List<ResenaLugares> lugares = controlLugares.MostrarResenasPorUsuario(Global.UsuarioActual);
            List<ResenaInfo> Nombrelugares = new List<ResenaInfo>();
            ServiciosLugar buscador = new ServiciosLugar();

            foreach (ResenaLugares place in lugares)
            {
                
                List<Lugar> buscarlugares = buscador.getLugarePorId(place.IdLugar);
                if (buscarlugares.Count > 0)
                {
                    ResenaInfo resena = new ResenaInfo
                    {
                        Nombre = buscarlugares[0].Nombre,
                        Calificacion = place.Calificacion,
                        Texto = place.Texto,
                        Imagen = base64AImagen(buscarlugares[0].Fotografia)
                    };
                   Nombrelugares.Add(resena);
                }
            }

            MiListBox.ItemsSource = Nombrelugares;
        }

        public BitmapImage base64AImagen(string texto)
        {
            try
            {
                string base64String = texto;
                byte[] binaryData = Convert.FromBase64String(base64String);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
                bi.Freeze();

                return bi;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar imagen para ");
                return null;
            }
        }

        public void cargarVideojuego()
        {
            controlVideojuegos = new ServicioVideogames();
            List<ResenaVideoGame> lugares = controlVideojuegos.MostrarResenasPorUsuario(Global.UsuarioActual);
            List<ResenaInfo> Nombrelugares = new List<ResenaInfo>();
            ServicioVideogames buscador = new ServicioVideogames();

            foreach (ResenaVideoGame place in lugares)
            {

                List<Videojuegos> buscarlugares = buscador.getVideojuegosPorId(place.IdGame);
                if (buscarlugares.Count > 0)
                {
                    ResenaInfo resena = new ResenaInfo
                    {
                        Nombre = buscarlugares[0].Nombre,
                        Calificacion = place.Calificacion,
                        Texto = place.Texto,
                        Imagen = base64AImagen(buscarlugares[0].Poster)
                    };
                    Nombrelugares.Add(resena);
                }
            }

            ListBoxVideojuegos.ItemsSource = Nombrelugares;
        }

        public void cargarEntretenimiento()
        {
            controlEntretenimiento = new ServicioEntretenimiento();
            List<ResenaEntretenimiento> lugares = controlEntretenimiento.MostrarResenasPorUsuario(Global.UsuarioActual);
            List<ResenaInfo> Nombrelugares = new List<ResenaInfo>();
            ServicioEntretenimiento buscador = new ServicioEntretenimiento();

            foreach (ResenaEntretenimiento place in lugares)
            {

                List<Entretenimiento> buscarlugares = buscador.getEntretenimientoPorId(place.IdEntreten);
                if (buscarlugares.Count > 0)
                {
                    ResenaInfo resena = new ResenaInfo
                    {
                        Nombre = buscarlugares[0].Nombre,
                        Calificacion = place.Calificacion,
                        Texto = place.Texto,
                        Imagen = base64AImagen(buscarlugares[0].Poster)
                    };
                    Nombrelugares.Add(resena);
                }
            }

            ListEntretenimiento.ItemsSource = Nombrelugares;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.VerInfoUsuario();
        }
    }
}
