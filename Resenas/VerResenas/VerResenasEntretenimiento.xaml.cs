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
    /// Lógica de interacción para VerResenasEntretenimiento.xaml
    /// </summary>
    public partial class VerResenasEntretenimiento : UserControl
    {

        public List<ResenaEntretenimiento> Entretenimientos { get; set; }

        private ServicioEntretenimiento controlEntretenimiento;
        public VerResenasEntretenimiento(int idEntreten)
        {
            InitializeComponent();
            controlEntretenimiento = new ServicioEntretenimiento();
            List<ResenaEntretenimiento> resenas = controlEntretenimiento.MostrarResenas(idEntreten);
            ServicioUsuarios resenado = new ServicioUsuarios();

            foreach (ResenaEntretenimiento resena in resenas)
            {
                Usuarios user = resenado.InfoUsuario(resena.Alias);
                if (resena.Texto == "" || resena.Texto == " ")
                {
                    resena.Texto = "Sin Opinion";
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
    }
}
