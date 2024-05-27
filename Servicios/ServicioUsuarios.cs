using MySql.Data.MySqlClient;
using Proyecto_Integrador.Modelos;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Servicios
{
    public class ServicioUsuarios
    {
        public string cadenaCnx;
        private MySqlConnection conexion;

        public ServicioUsuarios()
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

        public bool Insertar(Usuarios usuario)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO usuarios (Alias,Nombre,Amaterno,Apaterno,Avatar,Secreto,Correo) VALUES (@alias, @nombre, @amaterno, @apaterno, @avatar, @secreto, @correo)", conexion);
                    cmd.Parameters.AddWithValue("@alias", usuario.Alias);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@amaterno", usuario.Amaterno);
                    cmd.Parameters.AddWithValue("@apaterno", usuario.Apaterno);
                    cmd.Parameters.AddWithValue("@avatar", usuario.Avatar);
                    cmd.Parameters.AddWithValue("@secreto", usuario.Secreto);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
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

        public bool Actualizar(Usuarios usuario)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE usuarios SET Alias=@alias, Nombre=@nombre, Amaterno=@amaterno, Apaterno=@apaterno, Avatar=@avatar, Secreto=@secreto, Correo=@correo WHERE Alias=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", usuario.Alias);
                    cmd.Parameters.AddWithValue("@alias", usuario.Alias);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@amaterno", usuario.Amaterno);
                    cmd.Parameters.AddWithValue("@apaterno", usuario.Apaterno);
                    cmd.Parameters.AddWithValue("@avatar", usuario.Avatar);
                    cmd.Parameters.AddWithValue("@secreto", usuario.Secreto);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
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

        public bool ActualizarMenosAlias(Usuarios usuario)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE usuarios SET Nombre=@nombre, Amaterno=@amaterno, Apaterno=@apaterno, Avatar=@avatar, Secreto=@secreto, Correo=@correo WHERE Alias=@id", conexion);
                    cmd.Parameters.AddWithValue("@id", usuario.Alias);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@amaterno", usuario.Amaterno);
                    cmd.Parameters.AddWithValue("@apaterno", usuario.Apaterno);
                    cmd.Parameters.AddWithValue("@avatar", usuario.Avatar);
                    cmd.Parameters.AddWithValue("@secreto", usuario.Secreto);
                    cmd.Parameters.AddWithValue("@correo", usuario.Correo);
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
                    MySqlCommand cmd = new MySqlCommand("Delete from usuarios where Alias=@id", conexion);
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

        public bool Iniciar(string alias, string contra)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Alias=@alias AND Secreto=@contra LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@alias", alias);
                    cmd.Parameters.AddWithValue("@contra", contra);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.CerrarConexion();

                    return count > 0;
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

        public bool UsuarioDisponible(string alias)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE Alias=@alias LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@alias", alias);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    this.CerrarConexion();

                    return count == 0;
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

        public BitmapImage ImagenUsuario(string alias)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios WHERE Alias=@alias LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@alias", alias);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        try
                        {
                            string base64String = Convert.ToString(dataReader["Avatar"]);
                            string aliasaqui = Convert.ToString(dataReader["Alias"]);
                            byte[] binaryData = Convert.FromBase64String(base64String);
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(binaryData);
                            bi.CacheOption = BitmapCacheOption.OnLoad;
                            bi.EndInit();
                            bi.Freeze();

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

            return null;
        }


        public Usuarios InfoUsuario(string alias)
        {
            try
            {
                if (this.AbrirConexion())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuarios WHERE Alias=@alias LIMIT 1", conexion);
                    cmd.Parameters.AddWithValue("@alias", alias);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.Read())
                    {
                        try
                        {
                            // Crear una nueva instancia de Usuario
                            Usuarios usuario = new Usuarios();

                            usuario.Alias = Convert.ToString(dataReader["Alias"]);
                            usuario.Nombre = Convert.ToString(dataReader["Nombre"]);
                            usuario.Apaterno = Convert.ToString(dataReader["Apaterno"]);
                            usuario.Amaterno = Convert.ToString(dataReader["Amaterno"]);
                            usuario.Avatar = (string)(dataReader["Avatar"]);
                            usuario.Correo = Convert.ToString(dataReader["Correo"]);
                            usuario.Secreto = Convert.ToString((dataReader["Secreto"]));


                            // Procesar el avatar
                            string base64String = usuario.Avatar;
                            if (!string.IsNullOrEmpty(base64String))
                            {
                                byte[] binaryData = Convert.FromBase64String(base64String);
                                BitmapImage bi = new BitmapImage();
                                bi.BeginInit();
                                bi.StreamSource = new MemoryStream(binaryData);
                                bi.CacheOption = BitmapCacheOption.OnLoad;
                                bi.EndInit();
                                bi.Freeze();
                                usuario.Imagen = bi;

                            }

                            dataReader.Close();
                            this.CerrarConexion();

                            return usuario;
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
                        MessageBox.Show("No se encontró información para el alias proporcionado.");
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

            return null;
        }




    }
}

