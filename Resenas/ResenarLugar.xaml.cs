using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Resenas
{
    public partial class ResenarLugar : UserControl
    {
        public int RatingValue { get; private set; }
        
        int idDelLugar = -1;

        public ResenarLugar(int idlugar, string nomlugar)
        {
            InitializeComponent();
            RatingValue = 1;
            VerificarUsuario(idlugar, nomlugar);
        }

        public void VerificarUsuario(int id, string nombre)
        {
            titulo.Content = nombre;
            idDelLugar = id;
            string user = Global.UsuarioActual;

            if (string.IsNullOrEmpty(user))
            {
                Global.TipoResenar = 1;
                Global.IdAResenar = idDelLugar;
                MessageBox.Show("No has iniciado sesión");
                MainWindow main = new MainWindow();
                main.IrAIniciar();
            }
            else
            {
                txbUser.Text = user;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RatingValue = 1;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            RatingValue = 2;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            RatingValue = 3;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            RatingValue = 4;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            RatingValue = 5;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbUser.Text) || RatingValue < 1)
            {
                MessageBox.Show("Rellena todos los campo \n Recuerda agregar una calificacion");
            }
            else
            {
                ServiciosLugar resena = new ServiciosLugar();
                ResenaLugares re = new ResenaLugares();
                re.Alias = txbUser.Text;
                re.Calificacion = RatingValue;
                re.Texto = txbResena.Text;
                re.IdLugar = idDelLugar;
                bool res = resena.InsertarResenaLugar(re);
                if (res == true)
                {
                    MessageBox.Show("Resena Guardada");
                    MainWindow main = new MainWindow();
                    main.VerLugares();
                }
                else if (res == false)
                {
                    MessageBox.Show("Error al guardar reseña");
                }
                else
                {
                    MessageBox.Show("Error interno ");
                }
            }
        }
    }
}
