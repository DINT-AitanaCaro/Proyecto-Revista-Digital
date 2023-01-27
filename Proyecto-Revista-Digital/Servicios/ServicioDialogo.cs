using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioDialogo
    {
        public string DialogoAbrirFichero()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}
