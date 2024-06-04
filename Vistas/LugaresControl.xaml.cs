using FontAwesome.WPF;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Servicios;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador
{
    /// <summary>
    /// Lógica de interacción para LugaresControl.xaml
    /// </summary>
    public partial class LugaresControl : UserControl
    {
        public List<Lugar> Lugares { get; set; }

        private ServiciosLugar controlLugares;
        public LugaresControl()
        {
            InitializeComponent();
            LlenarCMBS();
            LlenarLista();


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
            main.RegistrarLugare();
        }

        public void LlenarCMBS()
        {
            ServiciosLugar lugar = new ServiciosLugar();
            List<string> regiones = lugar.MostrarRegiones();
            List<string> tipolugar = lugar.MostrarTipoLugar();

            cmbRegion.ItemsSource = regiones;
            cmbTipo.ItemsSource = tipolugar;
        }

        public void LlenarLista()
        {
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
                        double calid = Convert.ToDouble(controlLugares.ObtenerPromedioResenas(place.Id));
                        if (calid > 0)
                        {
                            place.Calificacion = calid;
                        }
                        else
                        {
                            place.Calificacion = null;
                        }
                        place.Estrellas = ObtenerEstrellas(calid);


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

        public void LlenarListaFiltros()
        {
            string? nombrelugar = !string.IsNullOrEmpty(txbBusqueda.Text) ? txbBusqueda.Text : null;
            int? tipo = cmbTipo.SelectedIndex != -1 ? (int?)(cmbTipo.SelectedIndex + 1) : null;
            int? region = cmbRegion.SelectedIndex != -1 ? (int?)(cmbRegion.SelectedIndex + 1) : null;
            ServiciosLugar controlLugares = new ServiciosLugar();
            List<Lugar> lugares;

            lugares = controlLugares.getLugaresFiltrado(tipo, region, nombrelugar);

            // Validar si se encontraron lugares
            if (lugares == null || lugares.Count == 0)
            {
                MessageBox.Show("No se encontraron lugares con los filtros especificados.");
                return;
            }

            // Procesar las fotografías de los lugares
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
                        double calid = Convert.ToDouble(controlLugares.ObtenerPromedioResenas(place.Id));
                        if (calid > 0)
                        {
                            place.Calificacion = calid;
                        }
                        else
                        {
                            place.Calificacion = null;
                        }
                        place.Estrellas = ObtenerEstrellas(calid);


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

            // Asignar la lista de lugares al ListBox
            MiListBox.ItemsSource = null;
            MiListBox.ItemsSource = lugares;
        }


        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            LlenarListaFiltros();
        }

        private void btbVaciar_Click(object sender, RoutedEventArgs e)
        {
            txbBusqueda.Text = string.Empty;
            cmbRegion.SelectedIndex = -1;
            cmbTipo.SelectedIndex = -1;
            LlenarLista();
        }

        private List<string> ObtenerEstrellas(double calificacion)
        {
            List<string> estrellas = new List<string>();

            if (calificacion == 0)
            {
                
                return estrellas;
            }

            int estrellasCompletas = (int)calificacion;
            bool mediaEstrella = calificacion - estrellasCompletas >= 0.1;
            int estrellasVacias = 5 - estrellasCompletas - (mediaEstrella ? 1 : 0);

            for (int i = 0; i < estrellasCompletas; i++)
            {
                estrellas.Add("\uf005"); 
            }
            if (mediaEstrella)
            {
                estrellas.Add("\uf5c0"); 
            }
            for (int i = 0; i < estrellasVacias; i++)
            {
                
            }

            return estrellas;
        }





    }
}
