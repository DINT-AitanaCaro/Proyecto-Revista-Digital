using Proyecto_Revista_Digital.Vistas;
using Proyecto_Revista_Digital.VistasModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.Servicios
{
    /// <summary>
    ///     Servicio para navegar y cargar las distintas ventanas de la aplicación.
    /// </summary>
    class ServicioNavegacion
    {
        /// <summary>
        ///     Constructor vacío para inicializar la clase.
        /// </summary>
        public ServicioNavegacion() { }

        /// <summary>
        ///     Método que devuelve una nueva instancia del User Control de gestión de autores
        /// </summary>
        /// <returns>UserControl de gestión de autores.</returns>
        public UserControl CargarGestionAutores()
        {
            return new GestionAutores();
        }

        /// <summary>
        ///     Método que crea una instancia de tipo WindowCrearEditarAutor.
        /// </summary>
        /// <returns>bool con la respuesta de la ventana dialogo.</returns>
        public bool? CargarNuevoEditarAutor()
        {
            WindowCrearEditarAutor windowCrearEditarAutor = new WindowCrearEditarAutor();
            return windowCrearEditarAutor.ShowDialog();
        }

        /// <summary>
        ///     Método para crear una nueva instancia del User Contro gestionar artículos.
        /// </summary>
        /// <returns>UserControl de gestión de articulos.</returns>
        public UserControl CargarGestionArticulos()
        {
            return new GestionArticulos();
        }

        /// <summary>
        ///     Método para crear una nueva instancia del UserControl para la creación de un nuevo artículo.
        /// </summary>
        /// <returns>UserControl de crear artículo.</returns>
        public UserControl CargarNuevoArticulo()
        {
            return new CrearArticulo();
        }

        /// <summary>
        ///     Método para crear una nueva instancia de UserControl para la gestión de listas de términos.
        /// </summary>
        /// <returns>UserControl de gestionar listas.</returns>
        public UserControl CargarGestionarListas()
        {
            return new UserControlGestionPalabrasProhibidas();
        }

        /// <summary>
        ///     Método para crear una nueva instancia tipo Window de crear una sección.
        /// </summary>
        /// <returns>bool con la respuesta de la ventana dialogo.</returns>
        public bool? CargarCrearSeccion()
        {
            CrearSeccion windowCrearSeccion = new CrearSeccion();
            return windowCrearSeccion.ShowDialog();
        }

        /// <summary>
        ///     Método para crear una nueva instancia tipo Window de crear/editar una lista de términos.
        /// </summary>
        /// <returns>bool con la respuesta de la ventana dialogo.</returns>
        public bool? CargarNuevoEditarListaTerminos()
        {
            WindowCrearEditarListaTerminos windowCrearEditarListaTerminos = new WindowCrearEditarListaTerminos();
            return windowCrearEditarListaTerminos.ShowDialog();
        }
    }
}
