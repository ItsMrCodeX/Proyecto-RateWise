namespace Proyecto_Integrador.Modelos
{
    public class Entretenimiento
    {
        private int Id;
        private string Nombre;
        private string Descripcion;
        private int FechaEstreno;
        private string Poster;
        private int IdTipoEntreten;
        private int IdGeneroEntreten;
        private int IdPlataformaEntreten;

        public Entretenimiento()
        {
        }

        public Entretenimiento(int id, string nombre, string descripcion, int fechaEstreno, string poster, int idTipoEntreten, int idGeneroEntreten, int idPlataformaEntreten)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaEstreno = fechaEstreno;
            Poster = poster;
            IdTipoEntreten = idTipoEntreten;
            IdGeneroEntreten = idGeneroEntreten;
            IdPlataformaEntreten = idPlataformaEntreten;
        }
    }

}
