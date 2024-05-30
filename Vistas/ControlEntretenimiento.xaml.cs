using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Vistas
{
    /// <summary>
    /// Lógica de interacción para ControlEntretenimiento.xaml
    /// </summary>
    public partial class ControlEntretenimiento : UserControl
    {
        public List<Entretenimiento> Entretenimiento { get; set; }

        private ServicioEntretenimiento controlEntretenimiento;
        public ControlEntretenimiento()
        {
            InitializeComponent();
            controlEntretenimiento = new ServicioEntretenimiento();
            List<Entretenimiento> entretenimientos = controlEntretenimiento.getEntretenimiento();

            foreach (Entretenimiento entreten in entretenimientos)
            {
                if (!string.IsNullOrEmpty(entreten.Poster))
                {
                    try
                    {
                        string base64String = entreten.Poster;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        entreten.Imagen = bi;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar imagen para {entreten.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"La fotografía está vacía o es nula para {entreten.Nombre}.");
                }
            }

            MiListBox.ItemsSource = entretenimientos;
        }

        private void MiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            Entretenimiento entreten = (Entretenimiento)listBox.SelectedItem;
            if (entreten != null)
            {
                int id = entreten.Id;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.AbrirVerEntretenimiento(id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.RegistrarEntretenimiento();
        }
    }
}
