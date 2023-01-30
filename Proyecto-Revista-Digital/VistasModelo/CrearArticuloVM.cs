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
using System.Windows;

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

        private ObservableCollection<string> nombreSecciones;

        public ObservableCollection<string> NombreSecciones
        {
            get { return nombreSecciones; }
            set { SetProperty(ref nombreSecciones, value); }
        }

        public RelayCommand NuevaSeccionCommand { get; }
        public RelayCommand NuevoArticuloCommand { get; }
        public RelayCommand NuevaImagenArticuloCommand { get; }
        private ServicioArticulo servicoArticulo;
        private readonly ServicioAutor servicioAutor;
        private readonly ServicioSeccion servicioSeccion;
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAzure servicioAzure;
        private readonly ServicioNavegacion servicioNavegacion;

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
            
            ArticuloNuevo = new Articulo();
            //ListaSecciones = new ObservableCollection<Seccion>();
            //ListaAutores = new ObservableCollection<Autor>();
            servicioAutor = new ServicioAutor();
            servicioAzure = new ServicioAzure();
            servicioDialogo = new ServicioDialogo();
            servicioSeccion = new ServicioSeccion();
            servicoArticulo = new ServicioArticulo();
            servicioNavegacion = new ServicioNavegacion();
            NuevaImagenArticuloCommand = new RelayCommand(SeleccionImagen);
            NuevaSeccionCommand = new RelayCommand(AñadirNuevaSeccion);
            NuevoArticuloCommand = new RelayCommand(AñadirArticulo);

            CargarAutores();
            CargarSecciones();
            SeccionArticulo = new Seccion();
            AutorArticulo = new Autor();
            

            /*WeakReferenceMessenger.Default.Register<CrearArticuloVM, EnviarSeccionMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.NuevaSeccion);
                }
            });*/
        }

        private void CargarAutores()
        {
            ListaAutores = new ObservableCollection<Autor>();
            ListaAutores = servicioAutor.GetAutores();
            
        }

        private void CargarSecciones()
        {
            ListaSecciones = new ObservableCollection<Seccion>();
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
            bool? resultado = servicioNavegacion.CargarCrearSeccion();
            CargarSecciones();
            //RefrescarLista((bool)resultado);
        }

        public void AñadirArticulo()
        {
            //Autor autor = BuscarAutor();
            //Seccion seccion = BuscarSeccion();
            //BuscarAutor();
            //BuscarSeccion();

            if (AutorArticulo != null && SeccionArticulo != null)
            {
                ArticuloNuevo.AutorArticulo = AutorArticulo;
                ArticuloNuevo.IdSeccion = SeccionArticulo.IdSeccion;
                ArticuloNuevo.Publicado = false;
                ArticuloNuevo.Contenido = "";
                
                if (!ControlarArticulosTitulo())
                {
                    servicoArticulo.AddArticulo(ArticuloNuevo);
                    MessageBox.Show("Agregado");
                }
                
            }
        }

        /*private void BuscarAutor()
        {
            foreach (var item in ListaAutores.Where(item => item.Nombre.Equals(AutorArticulo.Nombre)))
            {
                AutorArticulo = item;
            }

            //AutorArticulo = null;
        }

        private void BuscarSeccion()
        {
            foreach (var item in ListaSecciones.Where(item => item.NombreSeccion.Equals(SeccionArticulo.NombreSeccion)))
            {
                SeccionArticulo = item;
            }

            //SeccionArticulo = null;
        }*/

        private bool ControlarArticulosTitulo()
        {
            bool iguales = false;
            ObservableCollection<Articulo> articulos = new ObservableCollection<Articulo>();
            articulos = servicoArticulo.GetArticulos();
            foreach (var _ in articulos.Where(item => item.Titulo.Equals(ArticuloNuevo.Titulo)).Select(item => new { }))
            {
                iguales = true;
            }

            return iguales;
        }

        /*public void RefrescarLista(bool refrescar)
        {
            WeakReferenceMessenger.Default.Send(new RefrescarVentanaMessage(refrescar));
        }*/
    }
}
