using Microsoft.Toolkit.Uwp.Notifications;
using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Servicios;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Integrador.VistasExtra
{
    /// <summary>
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : UserControl
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.RegistrarUsuario();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ServicioUsuarios iniciador = new ServicioUsuarios();

            if (txtUsuario.Text == string.Empty || txtContraseña.Password == string.Empty)
            {
                MessageBox.Show("Rellena los datos solicitados");
            }
            else
            {
                bool res = iniciador.Iniciar(txtUsuario.Text, txtContraseña.Password);

                if (res == true)
                {

                    MessageBox.Show("Sesion Iniciada");
                    Global.TipoResenar = 0;
                    Global.IdAResenar = 0;
                    if (Global.IdAResenar == null || Global.IdAResenar == 0 || Global.TipoResenar == null || Global.TipoResenar == 0 )
                    {
                        Global.UsuarioActual = txtUsuario.Text;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.VerInicio();
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow();
                        int id;
                        switch (Global.TipoResenar)
                        {
                            case 0:
                                Global.UsuarioActual = txtUsuario.Text;
                                mainWindow.VerInicio();
                                break;
                            case 1:
                                id = Global.IdAResenar;
                                Global.UsuarioActual = txtUsuario.Text;
                                mainWindow.AbrirVerInfoLugar(id);
                                break;
                            case 2:
                                id = Global.IdAResenar;
                                Global.UsuarioActual = txtUsuario.Text;
                                mainWindow.AbrirVerInfoJuego(id);
                                break;
                            case 3:
                                id = Global.IdAResenar;
                                Global.UsuarioActual = txtUsuario.Text;
                                mainWindow.AbrirVerEntretenimiento(id);
                                break;
                        }
                    }

                }
                else if (res == false)
                {
                    MessageBox.Show("Usuario y/o Contrasena incorrectos");
                }
                else
                {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("Error al Iniciar Sesion");
                }
            }
        }
    }
}
