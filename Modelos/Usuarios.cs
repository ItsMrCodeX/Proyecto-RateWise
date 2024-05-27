using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Modelos
{
    public class Usuarios
    {
        private string alias;
        private string nombre;
        private string apaterno;
        private string amaterno;
        private string avatar;
        private string secreto;
        private string correo;
        private BitmapImage imagen;



        public Usuarios() { }
        public Usuarios(string alias, string nombre, string apaterno, string amaterno, string avatar, string secreto, string correo)
        {
            Alias = alias;
            Nombre = nombre;
            Apaterno = apaterno;
            Amaterno = amaterno;
            Avatar = avatar;
            Secreto = secreto;
            Correo = correo;
        }

        public string Alias { get => alias; set => alias = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apaterno { get => apaterno; set => apaterno = value; }
        public string Amaterno { get => amaterno; set => amaterno = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public string Secreto { get => secreto; set => secreto = value; }
        public string Correo { get => correo; set => correo = value; }
        public BitmapImage Imagen { get => imagen; set => imagen = value; }
    }
}
