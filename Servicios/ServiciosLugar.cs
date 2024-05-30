using MySql.Data.MySqlClient;
using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Servicios
{
    public class ServiciosLugar
    {
        public string cadenaCnx;
        private MySqlConnection conexion;

        public ServiciosLugar()
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
                        MessageBox.Show("Nose puso conectar con el servidor. Contacte a su Administrador");
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

        public bool Insertar(Lugar lugar)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO lugares (Nombre,Descripcion, Direccion,Contacto, Latitud, Longitud, Fotografias, idTipoLugar, idRegiones) VALUES (@nombre, @descripcion, @direccion,@contacto, @latitud, @longitud, @fotografias, @idTipoLugar, @idRegiones)", conexion);
                    cmd.Parameters.AddWithValue("@nombre", lugar.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", lugar.Descripcion);
                    cmd.Parameters.AddWithValue("@direccion", lugar.Direccion);
                    cmd.Parameters.AddWithValue("@contacto", lugar.Contacto);
                    cmd.Parameters.AddWithValue("@latitud", lugar.Latitud);
                    cmd.Parameters.AddWithValue("@longitud", lugar.Longitud);
                    cmd.Parameters.AddWithValue("@fotografias", lugar.Fotografia);
                    cmd.Parameters.AddWithValue("@idTipoLugar", lugar.IdTipoLugar);
                    cmd.Parameters.AddWithValue("@idRegiones", lugar.IdRegion);
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

        public bool Actualizar(Lugar lugar)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE lugares SET Nombre=@nombre, Direccion=@direccion,Contacto=@contacto, Latitud=@latitud, Longitud=@longitud, Fotografias=@fotografias, idTipoLugar=@idTipoLugar, idRegiones=@idRegiones WHERE idlugares=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", lugar.Id);
                    cmd.Parameters.AddWithValue("@nombre", lugar.Nombre);
                    cmd.Parameters.AddWithValue("@direccion", lugar.Direccion);
                    cmd.Parameters.AddWithValue("@contacto", lugar.Contacto);
                    cmd.Parameters.AddWithValue("@latitud", lugar.Latitud);
                    cmd.Parameters.AddWithValue("@longitud", lugar.Longitud);
                    cmd.Parameters.AddWithValue("@fotografias", lugar.Fotografia);
                    cmd.Parameters.AddWithValue("@idTipoLugar", lugar.IdTipoLugar);
                    cmd.Parameters.AddWithValue("@idRegiones", lugar.IdRegion);
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
                    MySqlCommand cmd = new MySqlCommand("Delete from lugares where idlugares=@id", conexion);
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
                    MySqlCommand cmd = new MySqlCommand("Delete from resenaslugares where idlugares=@id", conexion);
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

        public List<Lugar> getLugares()
        {
            string query = "SELECT * FROM lugares";
            List<Lugar> lugares = new List<Lugar>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Lugar lugar = new Lugar();
                        lugar.Id = Convert.ToInt32(dataReader["idlugares"]);
                        lugar.Nombre = Convert.ToString(dataReader["Nombre"]);
                        lugar.Descripcion = Convert.ToString(dataReader["Descripcion"]);
                        lugar.Direccion = Convert.ToString(dataReader["Direccion"]);
                        lugar.Contacto = Convert.ToInt64(dataReader["Contacto"]);
                        lugar.Latitud = Convert.ToDouble(dataReader["Latitud"]);
                        lugar.Longitud = Convert.ToDouble(dataReader["Longitud"]);
                        lugar.Fotografia = (string)(dataReader["Fotografias"]);
                        lugar.IdTipoLugar = Convert.ToInt32(dataReader["idTipoLugar"]);
                        lugar.IdRegion = Convert.ToInt32(dataReader["idRegiones"]);
                        lugares.Add(lugar);
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
                return lugares;
            }
            else
            {
                return null;
            }
        }

        public List<Lugar> getLugarePorId(int id)
        {
            string query = $"SELECT * FROM lugares where idLugares = {id}";
            List<Lugar> lugares = new List<Lugar>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Lugar lugar = new Lugar();
                        lugar.Id = Convert.ToInt32(dataReader["idlugares"]);
                        lugar.Nombre = Convert.ToString(dataReader["Nombre"]);
                        lugar.Descripcion = Convert.ToString(dataReader["Descripcion"]);
                        lugar.Direccion = Convert.ToString(dataReader["Direccion"]);
                        lugar.Contacto = Convert.ToInt64(dataReader["Contacto"]);
                        lugar.Latitud = Convert.ToDouble(dataReader["Latitud"]);
                        lugar.Longitud = Convert.ToInt64(dataReader["Longitud"]);
                        lugar.Fotografia = Convert.ToString(dataReader["Fotografias"]);
                        lugar.IdTipoLugar = Convert.ToInt32(dataReader["idTipoLugar"]);
                        lugar.IdRegion = Convert.ToInt32(dataReader["idRegiones"]);
                        lugares.Add(lugar);
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
                return lugares;
            }
            else
            {
                return null;
            }
        }

        public string getTipoLugar(int tipoLugarid)
        {
            string query = $"SELECT nombre FROM tipolugares where idtipolugar = {tipoLugarid}";

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

        public string getRegion(int regionid)
        {
            string query = $"SELECT nombre FROM regiones where idregiones = {regionid}";

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

        public bool InsertarResenaLugar(ResenaLugares resenas)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO resenaslugares (calificacion,textoresena,alias,idLugares) VALUES (@calificacion,@texto,@alias,@idlugar)", conexion);
                    cmd.Parameters.AddWithValue("@calificacion", resenas.Calificacion);
                    cmd.Parameters.AddWithValue("@texto", resenas.Texto);
                    cmd.Parameters.AddWithValue("@alias", resenas.Alias);
                    cmd.Parameters.AddWithValue("@idlugar", resenas.IdLugar);
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

        public double ObtenerPromedioResenas(int idLugar)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT ROUND(AVG(calificacion), 1) AS PromedioResenas FROM resenaslugares WHERE idlugares = @id_lugar", conexion);
                    cmd.Parameters.AddWithValue("@id_lugar", idLugar);

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

        public ResenaLugares UltimaResena(int idLugar)
        {
            ResenaLugares resena = null; // Inicializar como null para verificar si se encontró una reseña
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT alias, calificacion FROM resenaslugares WHERE idlugares = @id_lugar ORDER BY idresenaslugares DESC LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@id_lugar", idLugar);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        resena = new ResenaLugares();
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

        public List<string> MostrarRegiones()
        {
            string query = "SELECT nombre FROM regiones";
            List<string> nombresRegiones = new List<string>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string nombreRegion = Convert.ToString(dataReader["nombre"]);
                        nombresRegiones.Add(nombreRegion);
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

            return nombresRegiones;
        }

        public List<string> MostrarTipoLugar()
        {
            string query = "SELECT nombre FROM tipolugares";
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

        public BitmapImage ImagenUsuario(int idlugar)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM lugares WHERE idlugares=@iglugar LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@iglugar", idlugar);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        try
                        {
                            string base64String = Convert.ToString(dataReader["Fotografias"]);
                            string aliasaqui = Convert.ToString(dataReader["Nombre"]);
                            byte[] binaryData = Convert.FromBase64String(base64String);
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(binaryData);
                            bi.CacheOption = BitmapCacheOption.OnLoad; // Asegura que la imagen se carga completamente
                            bi.EndInit();
                            bi.Freeze(); // Opcional: Congela la imagen para mejorar el rendimiento en la UI

                            dataReader.Close();
                            this.CerrarConexion();

                            return bi;
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show("Error al convertir la cadena base64 a byte array: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al convertir imagen: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró avatar para el alias proporcionado.");
                    }

                    dataReader.Close();
                    this.CerrarConexion();
                }
                else
                {
                    MessageBox.Show("No se pudo abrir la conexión a la base de datos.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de MySQL: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error desconocido: " + ex.Message);
            }

            return null; // Retorna null si hay algún error
        }

        public List<ResenaLugares> MostrarResenas(int IdLugar)
        {
            List<ResenaLugares> resenas = new List<ResenaLugares>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM resenaslugares where idLugares = @idlugar", conexion);
                cmd.Parameters.AddWithValue("@idlugar", IdLugar);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResenaLugares resena = new ResenaLugares();
                        resena.IdResenaLugares = Convert.ToInt32(dataReader["IdResenasLugares"]);
                        resena.Calificacion = Convert.ToDouble(dataReader["Calificacion"]);
                        resena.Texto = Convert.ToString(dataReader["textoresena"]);
                        resena.Alias = Convert.ToString(dataReader["alias"]);
                        resena.IdLugar = Convert.ToInt16(dataReader["IdLugares"]);
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
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM resenaslugares WHERE Alias=@alias and idlugares=@id LIMIT 1", conexion);
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

        public List<ResenaLugares> MostrarResenasPorUsuario(string nomUsuario)
        {
            List<ResenaLugares> resenas = new List<ResenaLugares>();

            if (this.AbrirConexion())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM resenaslugares where alias = @nomuser", conexion);
                cmd.Parameters.AddWithValue("@nomuser", nomUsuario);
                try
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        ResenaLugares resena = new ResenaLugares();
                        resena.IdResenaLugares = Convert.ToInt32(dataReader["IdResenasLugares"]);
                        resena.Calificacion = Convert.ToDouble(dataReader["Calificacion"]);
                        resena.Texto = Convert.ToString(dataReader["textoresena"]);
                        resena.Alias = Convert.ToString(dataReader["alias"]);
                        resena.IdLugar = Convert.ToInt16(dataReader["IdLugares"]);
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
