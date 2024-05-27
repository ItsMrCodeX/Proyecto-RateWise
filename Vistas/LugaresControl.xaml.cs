using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador
{
    /// <summary>
    /// Lógica de interacción para LugaresControl.xaml
    /// </summary>
    public partial class LugaresControl : UserControl
    {
        public List<Lugar> Lugares { get; set; }
        public MainWindow Main { get; set; }

        private ServiciosLugar controlLugares;
        public LugaresControl()
        {
            InitializeComponent();
            controlLugares = new ServiciosLugar();
            List<Lugar> lugares = controlLugares.getLugares();

            foreach (Lugar place in lugares)
            {
                if (!string.IsNullOrEmpty(place.Fotografia))
                {
                    try
                    {
                        string base64String = place.Fotografia;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        place.Imagen = bi;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar imagen para {place.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"La fotografía está vacía o es nula para {place.Nombre}.");
                }
            }

            MiListBox.ItemsSource = lugares;
        }

        private void MiListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;
            Lugar lugar = (Lugar)listBox.SelectedItem;
            if (lugar != null)
            {
                int id = lugar.Id;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.AbrirVerInfoLugar(id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.AgregarLugar();
        }
    }
}
