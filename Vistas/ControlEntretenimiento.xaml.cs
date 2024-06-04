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
            LlenarCMBS();
            LLenarLista();


        }

        public void LLenarLista()
        {
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
                        double calid = Convert.ToDouble(controlEntretenimiento.ObtenerPromedioResenas(entreten.Id));
                        if (calid > 0)
                        {
                            entreten.Calificacion = calid;
                        }
                        else
                        {
                            entreten.Calificacion = null;
                        }
                        entreten.Estrellas = ObtenerEstrellas(calid);
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

        public void LlenarCMBS()
        {
            ServicioEntretenimiento movie = new ServicioEntretenimiento();
            List<string> genero = movie.MostrarGenero();
            List<string> plataforma = movie.MostrarPlataforma();
            List<string> tipo = movie.MostrarTipo();
            
            cmbTipo.ItemsSource = tipo;
            cmbGenero.ItemsSource = genero;
            cmbPlataforma.ItemsSource = plataforma;
            
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

        public void LlenarListaFiltrosEntretenimiento()
        {
            string? nombreEntretenimiento = !string.IsNullOrEmpty(txbBusqueda.Text) ? txbBusqueda.Text : null;
            int? tipo = cmbTipo.SelectedIndex != -1 ? (int?)(cmbTipo.SelectedIndex + 1) : null;
            int? genero = cmbGenero.SelectedIndex != -1 ? (int?)(cmbGenero.SelectedIndex + 1) : null;
            int? plataforma = cmbPlataforma.SelectedIndex != -1 ? (int?)(cmbPlataforma.SelectedIndex + 1) : null;
            ServicioEntretenimiento controlEntretenimiento = new ServicioEntretenimiento();
            List<Entretenimiento> entretenimientos;

            entretenimientos = controlEntretenimiento.getEntretenimientoFiltrado(tipo, genero, plataforma, nombreEntretenimiento);

            if (entretenimientos == null || entretenimientos.Count == 0)
            {
                MessageBox.Show("No se encontraron entretenimientos con los filtros especificados.");
                return;
            }

            foreach (Entretenimiento entretenimiento in entretenimientos)
            {
                if (!string.IsNullOrEmpty(entretenimiento.Poster))
                {
                    try
                    {
                        string base64String = entretenimiento.Poster;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        entretenimiento.Imagen = bi;
                        double calid = Convert.ToDouble(controlEntretenimiento.ObtenerPromedioResenas(entretenimiento.Id));
                        if (calid > 0)
                        {
                            entretenimiento.Calificacion = calid;
                        }
                        else
                        {
                            entretenimiento.Calificacion = null;
                        }
                        entretenimiento.Estrellas = ObtenerEstrellas(calid);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar poster para {entretenimiento.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"El poster está vacío o es nulo para {entretenimiento.Nombre}.");
                }
            }

            MiListBox.ItemsSource = entretenimientos;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.RegistrarEntretenimiento();
        }

        private void btbVaciar_Click(object sender, RoutedEventArgs e)
        {
            txbBusqueda.Text = string.Empty;
            cmbGenero.SelectedIndex = -1;
            cmbTipo.SelectedIndex = -1;
            cmbPlataforma.SelectedIndex = -1;
            LLenarLista();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            LlenarListaFiltrosEntretenimiento();
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
