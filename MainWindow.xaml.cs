using ModernWpf;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Resenas;
using Proyecto_Integrador.Resenas.VerResenas;
using Proyecto_Integrador.Vistas;
using Proyecto_Integrador.VistasExtra;
using Proyecto_Integrador.VistasExtra.EditoresAdministradores;
using System.Windows;

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
            Global.UsuarioActual = "ItsMrCodeX";
            lblUsuarioActual.Content = Global.UsuarioActual;
            Cnt_main.Content = new Inicial();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cnt_main.Content = new LugaresControl();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Cnt_main.Content = new ControlEntretenimiento();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VerVideoGames();
        }


        // Paginas de informacion
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
                var verInfoGame = new VerInfoGame(juego);
                mainWindow.Cnt_main.Content = verInfoGame;
            }
        }
        public void AbrirVerEntretenimiento(int entreten)
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                var VerEntretenimiento = new VerInfoEntretenimiento(entreten);
                mainWindow.Cnt_main.Content = VerEntretenimiento;
            }
        }



        // Ver Reseñas
        public void VerResenasLugar(int lugar)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                var verResenasLugar = new VerResenasLugares(lugar);
                mainWindow.Cnt_main.Content = verResenasLugar;
            }
        }
        public void VerResenasVideojuego(int game)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                var verResenasGame = new VerResenasGames(game);
                mainWindow.Cnt_main.Content = verResenasGame;
            }
        }
        public void VerResenasEntretenimiento(int movie)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                var verResenasGame = new VerResenasEntretenimiento(movie);
                mainWindow.Cnt_main.Content = verResenasGame;
            }
        }



        // Par ver paginas princiaples de cada tipo
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
        public void VerEntretenimiento()
        {

            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ControlEntretenimiento();
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
        public void VerInfoUsuario()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new Inicial();
                mainWindow.Cnt_main.Content = new InfoUsuario();
                ActualizaUsuario();

            }
        }
        public void VerResenasdeUsuario()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                mainWindow.Cnt_main.Content = new VerMisResenas();
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



        // Paginas de registro
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
        public void RegistrarVideojuego()
        {

            // Obtén una referencia a MainWindow
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Comprueba si mainWindow no es null
            if (mainWindow != null)
            {
                mainWindow.ActualizaUsuario();
                mainWindow.Cnt_main.Content = new RegistrarVideogames();
            }
        }
        public void RegistrarLugare()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new RegistrarLugar();

            }
        }
        public void RegistrarEntretenimiento()
        {

            // Obtén una referencia a MainWindow
            var mainWindow = Application.Current.MainWindow as MainWindow;

            // Comprueba si mainWindow no es null
            if (mainWindow != null)
            {
                mainWindow.ActualizaUsuario();
                mainWindow.Cnt_main.Content = new RegistrarEntretenimiento();
            }
        }


        // Paginas para reseñar
        public void ResenarLugar(int id, string nombre)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ResenarLugar(id, nombre);

            }
        }
        public void ResenarGame(int id, string nombre)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ResenarVideogames(id, nombre);

            }
        }
        public void ResenarEntretenimiento(int id, string nombre)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ResenarEntretenimiento(id, nombre);

            }
        }



        // Paginas para actualizar
        public void ActualizarLugar(int idLugar)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ActualizarLugar(idLugar);

            }
        }
        public void ActualizarJuego(int idJuego)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ActualizarVideojuego(idJuego);

            }
        }
        public void ActualizarEntretenimiento(int idEntreten)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {

                mainWindow.Cnt_main.Content = new ActualizarEntretenimiento(idEntreten);

            }
        }
        public void ActualizaUsuario()
        {
            lblUsuarioActual.Content = Global.UsuarioActual;
        }

    }
}