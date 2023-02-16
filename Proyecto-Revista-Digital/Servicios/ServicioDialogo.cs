using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Revista_Digital.Servicios
{
    /// <summary>
    ///     Servicio para mostrar diálogos.
    /// </summary>
    class ServicioDialogo
    {
        /// <summary>
        ///     Método para abrir el explorador de archivos del sistema y seleccionar imágenes.
        /// </summary>
        /// <returns>Devuelve el resultado del diálogo.</returns>
        public string DialogoAbrirFichero()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        /// <summary>
        ///     Método para mostrar un mensaje al usuario.
        /// </summary>
        /// <param name="mensaje">string que contiene el mensaje que se desea mostrar al usuario.</param>
        /// <param name="titulo">string que contiene ún título para el mensaje.</param>
        /// <param name="button">tipo de botón/botones que tendrá el mensaje.</param>
        /// <param name="image">tipo de icono que mostrará el mensaje.</param>
        public void MostrarMensaje(string mensaje, string titulo, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBox.Show(mensaje, titulo, button, image);
        }
    }
}
