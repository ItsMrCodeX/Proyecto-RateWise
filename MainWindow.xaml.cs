using ModernWpf;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Resenas;
using Proyecto_Integrador.Resenas.VerResenas;
using Proyecto_Integrador.Vistas;
using Proyecto_Integrador.VistasExtra;
using System.Windows;
using System.Windows.Media;

namespace Proyecto_Integrador
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lblUsuarioActual.Content = Global.UsuarioActual;
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cnt_main.Content = new LugaresControl();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Cnt_main.Content = new VerInfoLugar(1);
        }
        public void AbrirVerInfoLugar(int lugar)
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                var verInfoLugar = new VerInfoLugar(lugar);
                mainWindow.Cnt_main.Content = verInfoLugar;
            }
        }

        public void AbrirVerInfoJuego(int juego)
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                MessageBox.Show("Cargando: " + juego);
                var verInfoGame = new VerInfoGame(juego);
                mainWindow.Cnt_main.Content = verInfoGame;
            }
        }

        public void VerResenasLugar(int lugar)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                var verResenasLugar = new VerResenasLugares(lugar);
                mainWindow.Cnt_main.Content = verResenasLugar;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Global.UsuarioActual))
            {
                Cnt_main.Content = new InicioSesion();
            }
            else
            {
                Cnt_main.Content = new InfoUsuario();
            }
        }

        public void VerLugares()
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new LugaresControl();
                mainWindow.ActualizaUsuario();

            }
        }

        public void VerVideoGames()
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new VideoGamesControl();
                mainWindow.ActualizaUsuario();

            }
        }

        public void VerInicio()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new Inicial();
                mainWindow.ActualizaUsuario();

            }
        }
        public void IrAIniciar()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new InicioSesion();

            }
        }

        public void ActualizaUsuario()
        {
            lblUsuarioActual.Content = Global.UsuarioActual;
        }

        public void RegistrarUsuario()
        {

            // Obtén una referencia a MainWindow
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Comprueba si mainWindow no es null
            if (mainWindow != null)
            {
                mainWindow.ActualizaUsuario();
                mainWindow.Cnt_main.Content = new RegistrarUsuarios();
            }
        }

        public void ResenarLugar(int id, string nombre)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ResenarLugar(id, nombre);

            }
        }

        public void AgregarLugar()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new RegistrarLugar();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VerVideoGames();
        }
    }
}