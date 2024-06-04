using MySql.Data.MySqlClient;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using System.Configuration;
using System.Windows;

namespace Proyecto_Integrador.Servicios
{
    public class ServicioVideogames
    {
        public string cadenaCnx;
        private MySqlConnection conexion;

        public ServicioVideogames()
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

        public bool Insertar(Videojuegos videojuego)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO videogames (Nombre, Descripcion, Fechadeestreno, Poster, idplataforma, idgenerovid, idplataformadescarga) VALUES (@nombre, @descripcion, @fechaEstreno, @poster, @idPlataforma, @idGeneroVid, @idPlataformaDescarga)", conexion);
                    cmd.Parameters.AddWithValue("@nombre", videojuego.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", videojuego.Descripcion);
                    cmd.Parameters.AddWithValue("@fechaEstreno", videojuego.FechaEstreno);
                    cmd.Parameters.AddWithValue("@poster", videojuego.Poster);
                    cmd.Parameters.AddWithValue("@idPlataforma", videojuego.IdPlataforma);
                    cmd.Parameters.AddWithValue("@idGeneroVid", videojuego.IdGeneroVid);
                    cmd.Parameters.AddWithValue("@idPlataformaDescarga", videojuego.IdPlataformaDescarga);
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

        public bool Actualizar(Videojuegos videojuego)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE videogames SET Nombre=@nombre, Descripcion=@descripcion, Fechadeestreno=@fechaEstreno, Poster=@poster, idplataforma=@idPlataforma, idgenerovid=@idGeneroVid, idplataformadescarga=@idPlataformaDescarga WHERE idgames=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", videojuego.Id);
                    cmd.Parameters.AddWithValue("@nombre", videojuego.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", videojuego.Descripcion);
                    cmd.Parameters.AddWithValue("@fechaEstreno", videojuego.FechaEstreno);
                    cmd.Parameters.AddWithValue("@poster", videojuego.Poster);
                    cmd.Parameters.AddWithValue("@idPlataforma", videojuego.IdPlataforma);
                    cmd.Parameters.AddWithValue("@idGeneroVid", videojuego.IdGeneroVid);
                    cmd.Parameters.AddWithValue("@idPlataformaDescarga", videojuego.IdPlataformaDescarga);
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
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM videogames WHERE idgames=@id", conexion);
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
                    MySqlCommand cmd = new MySqlCommand("Delete from resenasvideogames where idgames=@id", conexion);
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

        public List<Videojuegos> getVideojuegos()
        {
            string query = "SELECT * FROM videogames";
            List<Videojuegos> videojuegos = new List<Videojuegos>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Videojuegos videojuego = new Videojuegos();
                        videojuego.Id = Convert.ToInt32(dataReader["idgames"]);
                        videojuego.Nombre = Convert.ToString(dataReader["Nombre"]);
                        videojuego.Descripcion = Convert.ToString(dataReader["Descripcion"]);
                        videojuego.FechaEstreno = Convert.ToDateTime(dataReader["Fechadeestreno"]);
                        videojuego.Poster = Convert.ToString(dataReader["Poster"]);
                        videojuego.IdPlataforma = Convert.ToInt32(dataReader["idplataforma"]);
                        videojuego.IdGeneroVid = Convert.ToInt32(dataReader["idgenerovid"]);
                        videojuego.IdPlataformaDescarga = Convert.ToInt32(dataReader["idplataformadescarga"]);
                        videojuegos.Add(videojuego);
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
                return videojuegos;
            }
            else
            {
                return null;
            }
        }

        public List<Videojuegos> getVideojuegosPorId(int id)
        {
            string query = "SELECT * FROM videogames WHERE idgames = @id";
            List<Videojuegos> videojuegos = new List<Videojuegos>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Videojuegos videojuego = new Videojuegos
                        {
                            Id = Convert.ToInt32(dataReader["idgames"]),
                            Nombre = Convert.ToString(dataReader["Nombre"]),
                            Descripcion = Convert.ToString(dataReader["Descripcion"]),
                            FechaEstreno = Convert.ToDateTime(dataReader["Fechadeestreno"]),
                            Poster = Convert.ToString(dataReader["Poster"]),
                            IdPlataforma = Convert.ToInt32(dataReader["idplataforma"]),
                            IdGeneroVid = Convert.ToInt32(dataReader["idgenerovid"]),
                            IdPlataformaDescarga = Convert.ToInt32(dataReader["idplataformadescarga"])
                        };
                        videojuegos.Add(videojuego);
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
                return videojuegos;
            }
            else
            {
                return null;
            }
        }

        public List<Videojuegos> getVideojuegosFiltrado(int? plataforma, int? genero, int? plataformaDescarga, string? nombre)
        {
            List<Videojuegos> videojuegos = new List<Videojuegos>();
            List<string> filters = new List<string>();
            string query = "SELECT * FROM videogames WHERE 1=1"; // 1=1 es para facilitar la concatenación de filtros

            if (plataforma.HasValue)
            {
                filters.Add("idplataforma = @plataforma");
            }
            if (genero.HasValue)
            {
                filters.Add("idgenerovid = @genero");
            }
            if (plataformaDescarga.HasValue)
            {
                filters.Add("idplataformadescarga = @plataformaDescarga");
            }
            if (!string.IsNullOrEmpty(nombre))
            {
                filters.Add("Nombre LIKE @nombre");
            }

            if (filters.Count > 0)
            {
                query += " AND " + string.Join(" AND ", filters);
            }

            if (this.AbrirConexion())
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                {
                    if (plataforma.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@plataforma", plataforma.Value);
                    }
                    if (genero.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@genero", genero.Value);
                    }
                    if (plataformaDescarga.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@plataformaDescarga", plataformaDescarga.Value);
                    }
                    if (!string.IsNullOrEmpty(nombre))
                    {
                        cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                    }

                    try
                    {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            Videojuegos videojuego = new Videojuegos
                            {
                                Id = Convert.ToInt32(dataReader["idgames"]),
                                Nombre = Convert.ToString(dataReader["Nombre"]),
                                Descripcion = Convert.ToString(dataReader["Descripcion"]),
                                FechaEstreno = Convert.ToDateTime(dataReader["Fechadeestreno"]),
                                Poster = Convert.ToString(dataReader["Poster"]),
                                IdPlataforma = Convert.ToInt32(dataReader["idplataforma"]),
                                IdGeneroVid = Convert.ToInt32(dataReader["idgenerovid"]),
                                IdPlataformaDescarga = Convert.ToInt32(dataReader["idplataformadescarga"])
                            };
                            videojuegos.Add(videojuego);
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
                }
                return videojuegos;
            }
            else
            {
                return null;
            }
        }


        public ResenaVideoGame UltimaResena(int idGame)
        {
            ResenaVideoGame resena = null; // Inicializar como null para verificar si se encontró una reseña
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT alias, calificacion FROM resenasvideogames WHERE idgames= @id_game ORDER BY idresenasvideogames DESC LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@id_game", idGame);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        resena = new ResenaVideoGame();
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
                    MySqlCommand cmd = new MySqlCommand("SELECT ROUND(AVG(calificacion), 1) AS PromedioResenas FROM resenasvideogames WHERE idgames = @id_game", conexion);
                    cmd.Parameters.AddWithValue("@id_game", idGame);

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
            string query = $"SELECT nombre FROM generovideojuego where idGeneroVid = {tipoid}";

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
            string query = $"SELECT nombre FROM plataformas where idPlataforma = {platid}";

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

        public string getDescarga(int descId)
        {
            string query = $"SELECT nombre FROM plataformadescarga where idPlataformaDescarga = {descId}";

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

        public bool InsertarResenaVideogame(ResenaVideoGame resenas)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO resenasvideogames (calificacion,textoresena,alias,idgames) VALUES (@calificacion,@texto,@alias,@idgames)", conexion);
                    cmd.Parameters.AddWithValue("@calificacion", resenas.Calificacion);
                    cmd.Parameters.AddWithValue("@texto", resenas.Texto);
                    cmd.Parameters.AddWithValue("@alias", resenas.Alias);
                    cmd.Parameters.AddWithValue("@idgames", resenas.IdGame);
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
            string query = "SELECT nombre FROM generovideojuego";
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
            string query = "SELECT nombre FROM plataformas";
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

        public List<string> MostrarDescarga()
        {
            string query = "SELECT nombre FROM plataformadescarga";
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

        public List<ResenaVideoGame> MostrarResenas(int idGame)
        {
            List<ResenaVideoGame> resenas = new List<ResenaVideoGame>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM resenasvideogames where idgames = @idlugar", conexion);
                cmd.Parameters.AddWithValue("@idlugar", idGame);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResenaVideoGame resena = new ResenaVideoGame();
                        resena.IdResenaVideoGames = Convert.ToInt32(dataReader["IdResenasvideogames"]);
                        resena.Calificacion = Convert.ToDouble(dataReader["Calificacion"]);
                        resena.Texto = Convert.ToString(dataReader["textoresena"]);
                        resena.Alias = Convert.ToString(dataReader["alias"]);
                        resena.IdGame = Convert.ToInt16(dataReader["idgames"]);
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
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM resenasvideogames WHERE Alias=@alias and idgames=@id LIMIT 1", conexion);
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
        public List<ResenaVideoGame> MostrarResenasPorUsuario(string nomUsuario)
        {
            List<ResenaVideoGame> resenas = new List<ResenaVideoGame>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM resenasvideogames where alias = @nomuser", conexion);
                cmd.Parameters.AddWithValue("@nomuser", nomUsuario);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResenaVideoGame resena = new ResenaVideoGame();
                        resena.IdResenaVideoGames = Convert.ToInt32(dataReader["idresenasvideogames"]);
                        resena.Calificacion = Convert.ToDouble(dataReader["Calificacion"]);
                        resena.Texto = Convert.ToString(dataReader["textoresena"]);
                        resena.Alias = Convert.ToString(dataReader["alias"]);
                        resena.IdGame = Convert.ToInt16(dataReader["idgames"]);
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

    }
}
