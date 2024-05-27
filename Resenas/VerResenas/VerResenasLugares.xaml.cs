using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Resenas.VerResenas
{
    /// <summary>
    /// Lógica de interacción para VerResenasLugares.xaml
    /// </summary>
    public partial class VerResenasLugares : UserControl
    {
        public List<ResenaLugares> Lugares { get; set; }
        public MainWindow Main { get; set; }

        private ServiciosLugar controlLugares;
        public VerResenasLugares(int lugarid)
        {
            InitializeComponent();
            controlLugares = new ServiciosLugar();
            List<ResenaLugares> resenas = controlLugares.MostrarResenas(lugarid);
            ServicioUsuarios resenado = new ServicioUsuarios();

            foreach (ResenaLugares resena in resenas)
            {
                Usuarios user = resenado.InfoUsuario(resena.Alias);
                if (resena.Texto == "" || resena.Texto == " ")
                {
                    resena.Texto = "__Sin Opinion__";
                }
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    try
                    {
                        string base64String = user.Avatar;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        resena.Imagen = bi; // Asigna la imagen al modelo de la reseña
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar imagen para {user.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"La fotografía está vacía o es nula para {user.Nombre}.");
                }
            }

            MiListBox.ItemsSource = resenas;
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.AgregarLugar();
        }
    }
}
