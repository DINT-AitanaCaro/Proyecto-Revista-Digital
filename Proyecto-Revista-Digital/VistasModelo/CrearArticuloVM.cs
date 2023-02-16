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

        private ObservableCollection<string> palabrasMalsonantes;

        public ObservableCollection<string> PalabrasMalsonantes
        {
            get { return palabrasMalsonantes; }
            set { SetProperty(ref palabrasMalsonantes, value); }
        }

        public RelayCommand NuevaSeccionCommand { get; }
        public RelayCommand NuevoArticuloCommand { get; }
        public RelayCommand NuevaImagenArticuloCommand { get; }
        public RelayCommand ComprobarTituloCommand { get; }
        private readonly ServicioArticulo servicoArticulo;
        private readonly ServicioAutor servicioAutor;
        private readonly ServicioSeccion servicioSeccion;
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAzure servicioAzure;
        private readonly ServicioNavegacion servicioNavegacion;
        private readonly ServicioModeracionContenido servicioModeracionContenido;

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
            PalabrasMalsonantes = new ObservableCollection<string>();
            ArticuloNuevo = new Articulo();
            servicioAutor = new ServicioAutor();
            servicioAzure = new ServicioAzure();
            servicioDialogo = new ServicioDialogo();
            servicioSeccion = new ServicioSeccion();
            servicoArticulo = new ServicioArticulo();
            servicioNavegacion = new ServicioNavegacion();
            servicioModeracionContenido = new ServicioModeracionContenido();
            NuevaImagenArticuloCommand = new RelayCommand(SeleccionImagen);
            NuevaSeccionCommand = new RelayCommand(AñadirNuevaSeccion);
            NuevoArticuloCommand = new RelayCommand(AñadirArticulo);
            ComprobarTituloCommand = new RelayCommand(ComprobarTitulo);
            CargarAutores();
            CargarSecciones();
            SeccionArticulo = new Seccion();
            AutorArticulo = new Autor();


            WeakReferenceMessenger.Default.Register<CrearArticuloVM, EnviarSeccionMessage>(this, (r, m) =>
            {
                CargarSecciones();
            });
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
            //CargarSecciones();

        }

        public void AñadirArticulo()
        {
            if (AutorArticulo != null && SeccionArticulo != null)
            {
                ArticuloNuevo.AutorArticulo = AutorArticulo;
                ArticuloNuevo.IdSeccion = SeccionArticulo.IdSeccion;
                ArticuloNuevo.Publicado = false;
                ArticuloNuevo.UrlPdf = "";

                if (!ComprobarTitulosRepetidos())
                {
                    if (ComprobarTexto(ArticuloNuevo.Titulo))
                    {
                        MostrarPalabrasMalsonantes();
                    }
                    else if (ComprobarTexto(ArticuloNuevo.Contenido))
                    {
                        MostrarPalabrasMalsonantes();
                    }
                    else
                    {
                        servicoArticulo.AddArticulo(ArticuloNuevo);
                        servicioDialogo.MostrarMensaje("Articulo agregdo con exito", "Agregado", MessageBoxButton.OK, MessageBoxImage.Information);
                        ArticuloNuevo = new Articulo();
                    }
                }
                else
                {
                    servicioDialogo.MostrarMensaje("El titulo esta repetido con otro articulo que ya existe", "Titulo repetido", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }



        private bool ComprobarTitulosRepetidos()
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

        private void ComprobarTitulo()
        {
            if (!ComprobarTitulosRepetidos())
            {
                if (ComprobarTexto(ArticuloNuevo.Titulo))
                {
                    MostrarPalabrasMalsonantes();
                }
                else
                {
                    servicioDialogo.MostrarMensaje("El titulo no esta repetido y no hay ninguna palabra malsonante", "Todo correcto", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                servicioDialogo.MostrarMensaje("El titulo esta repetido con otro articulo que ya existe", "Titulo repetido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ComprobarTexto(string texto)
        {
            if (texto!= null)
            {
                PalabrasMalsonantes = servicioModeracionContenido.AnalizarTexto(texto.Replace(",", " ").Replace(".", " ").Replace("|", " ").Replace("-", " ").Replace("_", " ").Replace(";", " ").Replace(":", " ").Replace("'", " "));
            }
            
            return PalabrasMalsonantes.Count > 0;
        }

        private void MostrarPalabrasMalsonantes()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Palabras malsonantes: \n");
            foreach (string item in PalabrasMalsonantes)
            {
                sb.Append("- ").Append(item).Append("\n");
            }
            servicioDialogo.MostrarMensaje(sb.ToString(), "CIUDADO", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
