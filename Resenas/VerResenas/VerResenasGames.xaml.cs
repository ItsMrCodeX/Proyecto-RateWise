﻿using Proyecto_Integrador.Modelos;
using Proyecto_Integrador.Modelos.ModelosResenas;
using Proyecto_Integrador.Servicios;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Proyecto_Integrador.Resenas.VerResenas
{
    /// <summary>
    /// Lógica de interacción para VerResenasGames.xaml
    /// </summary>
    public partial class VerResenasGames : UserControl
    {
        public List<ResenaVideoGame> Games { get; set; }

        private ServicioVideogames controlJuegos;
        public VerResenasGames(int gameId)
        {
            InitializeComponent();
            controlJuegos = new ServicioVideogames();
            List<ResenaVideoGame> resenas = controlJuegos.MostrarResenas(gameId);
            ServicioUsuarios resenado = new ServicioUsuarios();

            foreach (ResenaVideoGame resena in resenas)
            {
                Usuarios user = resenado.InfoUsuario(resena.Alias);
                if (resena.Texto == "" || resena.Texto == " ")
                {
                    resena.Texto = "Sin Opinion";
                }
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    try
                    {
                        string base64String = user.Avatar;
                        byte[] binaryData = Convert.FromBase64String(base64String);

                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.EndInit();
                        bi.Freeze();

                        resena.Imagen = bi; // Asigna la imagen al modelo de la reseña
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar imagen para {user.Nombre}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"La fotografía está vacía o es nula para {user.Nombre}.");
                }
            }

            MiListBox.ItemsSource = resenas;
        }
    }
}
