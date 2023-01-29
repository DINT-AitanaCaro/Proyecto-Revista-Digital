using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class CrearArticuloVM : ObservableRecipient
    {
        private Articulo articuloNuevo;

        public Articulo ArticuloNuevo
        {
            get { return articuloNuevo; }
            set { SetProperty(ref articuloNuevo, value); }
        }


        private ObservableCollection<Seccion> listaSecciones;

        public ObservableCollection<Seccion> ListaSecciones
        {
            get { return listaSecciones; }
            set { SetProperty(ref listaSecciones, value); }
        }

        private ObservableCollection<Autor> listaAutores;

        public ObservableCollection<Autor> ListaAutores
        {
            get { return listaAutores; }
            set { SetProperty(ref listaAutores, value); }
        }

        private ObservableCollection<string> nombreAutores;

        public ObservableCollection<string> NombreAutores
        {
            get { return nombreAutores; }
            set { SetProperty(ref nombreAutores, value); }
        }


        public RelayCommand NuevaSeccionCommand { get; }
        public RelayCommand NuevoArticuloCommand { get; }
        public RelayCommand NuevaImagenArticuloCommand { get; }
        private ServicioArticulo servicoArticulo;
        private ServicioAutor servicioAutor;
        private ServicioSeccion servicioSeccion;
        private ServicioDialogo servicioDialogo;
        private ServicioAzure servicioAzure;
        private ServicioNavegacion servicioNavegacion;

        private Seccion seccionArticulo;

        public Seccion SeccionArticulo
        {
            get { return seccionArticulo; }
            set { SetProperty(ref seccionArticulo, value); }
        }

        private Autor autorArticulo;

        public Autor AutorArticulo
        {
            get { return autorArticulo; }
            set { SetProperty(ref autorArticulo, value); }
        }

        private Seccion nuevaSeccion;

        public Seccion NuevaSeccion
        {
            get { return nuevaSeccion; }
            set { SetProperty(ref nuevaSeccion, value); }
        }


        public CrearArticuloVM()
        {
            NombreAutores = new ObservableCollection<string>();
            ArticuloNuevo = new Articulo();
            ListaSecciones = new ObservableCollection<Seccion>();
            ListaAutores = new ObservableCollection<Autor>();
            servicioAutor = new ServicioAutor();
            servicioAzure = new ServicioAzure();
            servicioDialogo = new ServicioDialogo();
            servicioSeccion = new ServicioSeccion();
            servicoArticulo = new ServicioArticulo();
            servicioNavegacion = new ServicioNavegacion();
            NuevaImagenArticuloCommand = new RelayCommand(SeleccionImagen);
            CargarAutores();
            CargarSecciones();
            SeccionArticulo = new Seccion();
            AutorArticulo = new Autor();
            

            WeakReferenceMessenger.Default.Register<CrearArticuloVM, EnviarSeccionMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.NuevaSeccion);
                }
            });
        }

        private void CargarAutores()
        {
            ListaAutores = servicioAutor.GetAutores();

            foreach (Autor item in ListaAutores)
            {
                NombreAutores.Add(item.Nombre);
            }
        }

        private void CargarSecciones()
        {
            ListaSecciones = servicioSeccion.GetSecciones();
        }

        public void SeleccionImagen()
        {
            string file = servicioDialogo.DialogoAbrirFichero();
            ArticuloNuevo.Imagen = file != null ? servicioAzure.AlmacenarImagenEnLaNube(file) : string.Empty;
        }

        public void AñadirNuevaSeccion()
        {
            NuevaSeccion = new Seccion();

        }

        public void RefrescarLista(bool refrescar)
        {
            WeakReferenceMessenger.Default.Send(new RefrescarVentanaMessage(refrescar));
        }
    }
}
