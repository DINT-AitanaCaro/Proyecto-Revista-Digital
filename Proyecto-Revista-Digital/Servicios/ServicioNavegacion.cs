using Proyecto_Revista_Digital.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioNavegacion
    {
        public ServicioNavegacion() { }

        public UserControl CargarGestionAutores()
        {
            return new GestionAutores();
        }

        public bool? CargarNuevoEditarAutor()
        {
            WindowCrearEditarAutor windowCrearEditarAutor = new WindowCrearEditarAutor();
            return windowCrearEditarAutor.ShowDialog();
        }
    }
}
