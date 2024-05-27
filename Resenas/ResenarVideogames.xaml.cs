using Proyecto_Integrador.Globales;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Integrador.Resenas
{
    /// <summary>
    /// Lógica de interacción para ResenarVideogames.xaml
    /// </summary>
    public partial class ResenarVideogames : UserControl
    {
        public int RatingValue { get; private set; }

        int idDelGame = -1;
        public ResenarVideogames(int idgame, string nomgame)
        {
            InitializeComponent();
            VerificarUsuario(idgame, nomgame);
        }

        public void VerificarUsuario(int id, string nombre)
        {
            titulo.Content = nombre;
            idDelGame = id;
            string user = Global.UsuarioActual;

            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("No has iniciado sesión");
                MainWindow main = new MainWindow();
                main.IrAIniciar();
            }
            else
            {
                txbUser.Text = user;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RatingValue = 1;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RatingValue = 2;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RatingValue = 3;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RatingValue = 4;
            star1.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star2.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star3.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star4.Icon = FontAwesome.WPF.FontAwesomeIcon.Star;
            star5.Icon = FontAwesome.WPF.FontAwesomeIcon.StarOutline;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
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
                ServicioVideogames resena = new ServicioVideogames();
                ResenaVideoGame re = new ResenaVideoGame();
                re.Alias = txbUser.Text;
                re.Calificacion = RatingValue;
                re.Texto = txbResena.Text;
                re.IdGame = idDelGame;
                bool res = resena.InsertarResenaVideogame(re);
                if (res == true)
                {
                    MessageBox.Show("Resena Guardada");
                    MainWindow main = new MainWindow();
                    main.VerVideoGames();
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
