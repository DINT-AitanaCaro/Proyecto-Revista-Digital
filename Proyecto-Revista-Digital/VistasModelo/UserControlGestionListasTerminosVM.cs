using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.Servicios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class UserControlGestionListasTerminosVM : ObservableObject
    {
        public RelayCommand MarcarComoAplicadaCommnad { get; }
        public RelayCommand AñadirListaCommand { get; }
        public RelayCommand EliminarListaCommand { get; }
        public RelayCommand EditarListaCommand { get; }
        private ServicioDialogo servicioDialogo;
        private ObservableCollection<ListaTerminos> listasTerminos;

        public ObservableCollection<ListaTerminos> ListasTerminos
        {
            get { return listasTerminos; }
            set { SetProperty(ref listasTerminos, value); }
        }

        private ListaTerminos listaSeleccionada;
        public ListaTerminos ListaSeleccionada
        {
            get { return listaSeleccionada; }
            set { SetProperty(ref listaSeleccionada, value); }
        }

        private ServicioNavegacion servicioNavegacion;
        private ServicioAPIRestListasTerminos servicioListas;
        public UserControlGestionListasTerminosVM()
        {
            ListasTerminos = new ObservableCollection<ListaTerminos>();
            //servicios
            servicioNavegacion = new ServicioNavegacion();
            servicioListas = new ServicioAPIRestListasTerminos();
            servicioDialogo = new ServicioDialogo();
            //Cargar listas
            CargarListas();
            //comandos
            MarcarComoAplicadaCommnad = new RelayCommand(MarcarListaComoAplicada);
            AñadirListaCommand = new RelayCommand(AñadirLista);
            EliminarListaCommand = new RelayCommand(AñadirLista);
            EditarListaCommand = new RelayCommand(EditarLista);

            //envío mensaje
            WeakReferenceMessenger.Default.Register<UserControlGestionListasTerminosVM, EnviarListaMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.ListaSeleccionada);
                }
            });

            WeakReferenceMessenger.Default.Register<ListaCreadaEditadaMessage>(this, (r, m) =>
            {
                ListaTerminos editada = m.Value;
                if (ListaSeleccionada.Id == 0)
                {
                    ListasTerminos.Add(editada);
                } else
                {
                    ListaTerminos lista = ListasTerminos.First( l => l.Id == editada.Id);
                    lista.Name = editada.Name;
                    lista.Description = editada.Description;
                    lista.Terminos = editada.Terminos;
                }
            });
        }

        public void MarcarListaComoAplicada()
        {
            ListasTerminos.ToList().ForEach(list => { if (list.Aplicada) { list.Aplicada = false; } });
            ListaSeleccionada.Aplicada = true;
            Properties.Settings.Default.IdListaAplicada = ListaSeleccionada.Id;
        }

        public void AñadirLista()
        {
            ListaSeleccionada = new ListaTerminos();
            bool? resultado = servicioNavegacion.CargarNuevoEditarListaTerminos();
            //Refrescar((bool)resultado);
        }

        public void EditarLista()
        {
            bool? resultado = servicioNavegacion.CargarNuevoEditarListaTerminos();
            //Refrescar((bool)resultado);
        }
        public void EliminarLista()
        {
            IRestResponse response = servicioListas.EliminarLista(ListaSeleccionada.Id);
            if (servicioListas.EliminarLista(ListaSeleccionada.Id).StatusCode == System.Net.HttpStatusCode.OK)
            {
                ListasTerminos.Remove(ListaSeleccionada);
                servicioDialogo.MostrarMensaje(response.ErrorException.Message, "Error al eliminar la lista", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CargarListas()
        {
            //ListasTerminos = new ObservableCollection<ListaTerminos>();
            ListasTerminos = servicioListas.GetListas();
            foreach (ListaTerminos lista in ListasTerminos)
            {
                lista.Terminos = servicioListas.GetTerminos(lista.Id);
            }
        }
    }
}
