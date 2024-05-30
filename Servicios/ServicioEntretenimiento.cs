using MySql.Data.MySqlClient;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using System.Configuration;
using System.Windows;

namespace Proyecto_Integrador.Servicios
{
    public class ServicioEntretenimiento
    {
        public string cadenaCnx;
        private MySqlConnection conexion;

        public ServicioEntretenimiento()
        {
            cadenaCnx = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            conexion = new MySqlConnection(cadenaCnx);
        }

        public bool AbrirConexion()
        {
            try
            {
                CerrarConexion();
                conexion.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("No se puso conectar con el servidor. Contacte a su Administrador");
                        break;
                    case 1045:
                        MessageBox.Show("Datos de usuario Invalido, intente de nuevo");
                        break;

                }
            }

            return false;
        }

        private bool CerrarConexion()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool Insertar(Entretenimiento entretenimiento)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO entretenimiento (Nombre, Descripcion, Fechadeestreno, Poster, idtipoentreten, idgeneroentreten, idplataformaentreten) VALUES (@nombre, @descripcion, @fechaEstreno, @poster, @idtipoentreten, @idgeneroentreten, @idplataformaentreten)", conexion);
                    cmd.Parameters.AddWithValue("@nombre", entretenimiento.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", entretenimiento.Descripcion);
                    cmd.Parameters.AddWithValue("@fechaEstreno", entretenimiento.FechaEstreno);
                    cmd.Parameters.AddWithValue("@poster", entretenimiento.Poster);
                    cmd.Parameters.AddWithValue("@idtipoentreten", entretenimiento.IdTipoEntreten);
                    cmd.Parameters.AddWithValue("@idgeneroentreten", entretenimiento.IdGeneroEntreten);
                    cmd.Parameters.AddWithValue("@idplataformaentreten", entretenimiento.IdPlataformaEntreten);
                    cmd.ExecuteNonQuery();
                    this.CerrarConexion();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Actualizar(Entretenimiento entretenimiento)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE entretenimiento SET Nombre=@nombre, Descripcion=@descripcion, Fechadeestreno=@fechaEstreno, Poster=@poster, idtipoentreten=@idtipoentreten, idgeneroentreten=@idgeneroentreten, idplataformaentreten=@idplataformaentreten WHERE identreten=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", entretenimiento.Id);
                    cmd.Parameters.AddWithValue("@nombre", entretenimiento.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", entretenimiento.Descripcion);
                    cmd.Parameters.AddWithValue("@fechaEstreno", entretenimiento.FechaEstreno);
                    cmd.Parameters.AddWithValue("@poster", entretenimiento.Poster);
                    cmd.Parameters.AddWithValue("@idtipoentreten", entretenimiento.IdTipoEntreten);
                    cmd.Parameters.AddWithValue("@idgeneroentreten", entretenimiento.IdGeneroEntreten);
                    cmd.Parameters.AddWithValue("@idplataformaentreten", entretenimiento.IdPlataformaEntreten);
                    cmd.ExecuteNonQuery();
                    this.CerrarConexion();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Borrar(int id)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM entretenimiento WHERE identreten=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    this.CerrarConexion();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool BorrarResenas(int id)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("Delete from resenasentretenimiento where identreten=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    this.CerrarConexion();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public List<Entretenimiento> getEntretenimiento()
        {
            string query = "SELECT * FROM entretenimiento";
            List<Entretenimiento> entretenimientos = new List<Entretenimiento>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Entretenimiento entretenimiento = new Entretenimiento();
                        entretenimiento.Id = Convert.ToInt32(dataReader["identreten"]);
                        entretenimiento.Nombre = Convert.ToString(dataReader["Nombre"]);
                        entretenimiento.Descripcion = Convert.ToString(dataReader["Descripcion"]);
                        entretenimiento.FechaEstreno = Convert.ToDateTime(dataReader["Fechadeestreno"]);
                        entretenimiento.Poster = Convert.ToString(dataReader["Poster"]);
                        entretenimiento.IdTipoEntreten = Convert.ToInt32(dataReader["idtipoentreten"]);
                        entretenimiento.IdGeneroEntreten = Convert.ToInt32(dataReader["idgeneroentreten"]);
                        entretenimiento.IdPlataformaEntreten = Convert.ToInt32(dataReader["idplataformaentreten"]);
                        entretenimientos.Add(entretenimiento);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                CerrarConexion();
                return entretenimientos;
            }
            else
            {
                return null;
            }
        }

        public List<Entretenimiento> getEntretenimientoPorId(int id)
        {
            string query = "SELECT * FROM entretenimiento WHERE identreten = @id";
            List<Entretenimiento> entretenimientos = new List<Entretenimiento>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Entretenimiento entretenimiento = new Entretenimiento
                        {
                            Id = Convert.ToInt32(dataReader["identreten"]),
                            Nombre = Convert.ToString(dataReader["Nombre"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            FechaEstreno = Convert.ToDateTime(dataReader["Fechadeestreno"]),
                            Poster = Convert.ToString(dataReader["Poster"]),
                            IdTipoEntreten = Convert.ToInt32(dataReader["idtipoentreten"]),
                            IdGeneroEntreten = Convert.ToInt32(dataReader["idgeneroentreten"]),
                            IdPlataformaEntreten = Convert.ToInt32(dataReader["idplataformaentreten"])
                        };
                        entretenimientos.Add(entretenimiento);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                finally
                {
                    CerrarConexion();
                }
                return entretenimientos;
            }
            else
            {
                return null;
            }
        }

        public ResenaEntretenimiento UltimaResena(int idEntreten)
        {
            ResenaEntretenimiento resena = null; // Inicializar como null para verificar si se encontró una reseña
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT alias, calificacion FROM resenasentretenimiento WHERE identreten= @identreten ORDER BY idresenasentreten DESC LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@identreten", idEntreten);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        resena = new ResenaEntretenimiento();
                        resena.Alias = dataReader["alias"].ToString();
                        resena.Calificacion = Convert.ToDouble(dataReader["calificacion"]);
                    }

                    dataReader.Close();
                    this.CerrarConexion();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return resena;
        }

        public double ObtenerPromedioResenas(int idGame)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT ROUND(AVG(calificacion), 1) AS PromedioResenas FROM resenasentretenimiento WHERE identreten = @identreten", conexion);
                    cmd.Parameters.AddWithValue("@identreten", idGame);

                    object result = cmd.ExecuteScalar();
                    this.CerrarConexion();

                    if (result != DBNull.Value)
                    {
                        return Convert.ToDouble(result);
                    }
                    else
                    {
                        return 0; // Retorna 0 si no hay reseñas
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public string getGenero(int tipoid)
        {
            string query = $"SELECT nombre FROM generoentreten where idGeneroEntreten = {tipoid}";

            string tipolugar;
            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        tipolugar = Convert.ToString(dataReader["Nombre"]);
                        return tipolugar;
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public string getPlataforma(int platid)
        {
            string query = $"SELECT nombre FROM plataformaentreten where idplataformaentreten = {platid}";

            string tipolugar;
            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        tipolugar = Convert.ToString(dataReader["Nombre"]);
                        return tipolugar;
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public string getTipo(int tipoId)
        {
            string query = $"SELECT nombre FROM tipoentreten where idtipoEntreten = {tipoId}";

            string tipolugar;
            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        tipolugar = Convert.ToString(dataReader["Nombre"]);
                        return tipolugar;
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public bool InsertarResenaEntretenimiento(ResenaEntretenimiento resenas)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO resenasentretenimiento (calificacion,textoresena,alias,identreten) VALUES (@calificacion,@texto,@alias,@identreten)", conexion);
                    cmd.Parameters.AddWithValue("@calificacion", resenas.Calificacion);
                    cmd.Parameters.AddWithValue("@texto", resenas.Texto);
                    cmd.Parameters.AddWithValue("@alias", resenas.Alias);
                    cmd.Parameters.AddWithValue("@identreten", resenas.IdEntreten);
                    cmd.ExecuteNonQuery();
                    this.CerrarConexion();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<string> MostrarGenero()
        {
            string query = "SELECT nombre FROM generoentreten";
            List<string> tipolugares = new List<string>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string nombreRegion = Convert.ToString(dataReader["nombre"]);
                        tipolugares.Add(nombreRegion);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }

            }
            else
            {
                return null;
            }

            return tipolugares;
        }

        public List<string> MostrarPlataforma()
        {
            string query = "SELECT nombre FROM plataformaentreten";
            List<string> tipolugares = new List<string>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string nombreRegion = Convert.ToString(dataReader["nombre"]);
                        tipolugares.Add(nombreRegion);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }

            }
            else
            {
                return null;
            }

            return tipolugares;
        }

        public List<string> MostrarTipo()
        {
            string query = "SELECT nombre FROM tipoentreten";
            List<string> tipolugares = new List<string>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string nombreRegion = Convert.ToString(dataReader["nombre"]);
                        tipolugares.Add(nombreRegion);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }

            }
            else
            {
                return null;
            }

            return tipolugares;
        }

        public List<ResenaEntretenimiento> MostrarResenas(int idEntreten)
        {
            List<ResenaEntretenimiento> resenas = new List<ResenaEntretenimiento>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM resenasentretenimiento where identreten = @identreten", conexion);
                cmd.Parameters.AddWithValue("@identreten", idEntreten);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResenaEntretenimiento resena = new ResenaEntretenimiento();
                        resena.Id = Convert.ToInt32(dataReader["idresenasentreten"]);
                        resena.Calificacion = Convert.ToDouble(dataReader["Calificacion"]);
                        resena.Texto = Convert.ToString(dataReader["textoresena"]);
                        resena.Alias = Convert.ToString(dataReader["alias"]);
                        resena.IdEntreten = Convert.ToInt16(dataReader["identreten"]);
                        resenas.Add(resena);
                    }
                    dataReader.Close();
                }
                catch (MySqlException ex1)
                {
                    MessageBox.Show(ex1.Message);
                    return null;
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.StackTrace);
                    MessageBox.Show(ex2.Message);
                    return null;
                }
                CerrarConexion();
                return resenas;
            }
            else
            {
                return null;
            }
        }

        public bool VerificarResenaPrevia(string alias, int id)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM resenasentretenimiento WHERE Alias=@alias and identreten=@id LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@alias", alias);
                    cmd.Parameters.AddWithValue("@id", id);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.CerrarConexion();

                    return count == 0;
                }
                else
                {
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
        }

    }
}
