using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    class WindowCrearEditarListaTerminosVM : ObservableObject
    {
        public RelayCommand AñadirTerminoCommand { get; }
        public RelayCommand EliminarTerminoCommand { get; }
        public RelayCommand EliminarTodosTerminoCommand { get; }
        private ListaTerminos _lista;
        public ListaTerminos ListaActual
        {
            get { return _lista; }
            set { SetProperty(ref _lista, value); }
        }
        private string nuevoTermino;
        public string NuevoTermino
        {
            get { return nuevoTermino; }
            set { SetProperty(ref nuevoTermino, value); }
        }

        private string terminoSeleccionado;
        public string TerminoSeleccionado
        {
            get { return terminoSeleccionado; }
            set { SetProperty(ref terminoSeleccionado, value); }
        }

        private ObservableCollection<string> terminos;
        public ObservableCollection<string> Terminos
        {
            get { return terminos; }
            set { SetProperty(ref terminos, value); }
        }

        private string _modo;

        public string Modo
        {
            get { return _modo; }
            set { SetProperty(ref _modo, value); }
        }


        private bool existe;

        public bool Existe
        {
            get { return existe; }
            set { SetProperty(ref existe, value); }
        }

        private ServicioAPIRestListasTerminos servicioListas;
        private ServicioDialogo servicioDialogo;
        public WindowCrearEditarListaTerminosVM()
        {
            servicioDialogo = new ServicioDialogo();
            servicioListas = new ServicioAPIRestListasTerminos();
            ListaActual = new ListaTerminos(WeakReferenceMessenger.Default.Send<EnviarListaMessage>());

            Modo = (Existe = ListaActual.Id != 0) ? "Editar Lista" : "Crear Lista";
            AñadirTerminoCommand = new RelayCommand(CrearTermino);
            EliminarTerminoCommand = new RelayCommand(EliminarTermino);
            EliminarTodosTerminoCommand = new RelayCommand(EliminarTodosTermino);
        }

        public void CrearTermino()
        {
            //IRestResponse response = servicioListas.AñadirTermino(ListaActual.Id, NuevoTermino);
           // if (response.StatusCode == System.Net.HttpStatusCode.Created)
           // {
                ListaActual.Terminos.Add(NuevoTermino);
          //  }
        }
        public void EliminarTermino()
        {
         //   IRestResponse response = servicioListas.EliminarTermino(ListaActual.Id, NuevoTermino);
          //  if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
          //  {
                ListaActual.Terminos.Remove(TerminoSeleccionado);
          //  }
        }
        public void EliminarTodosTermino()
        {
           // IRestResponse response = servicioListas.EliminarTodosTerminos(ListaActual.Id);
          //  if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
          //  {
                ListaActual.Terminos = new ObservableCollection<string>();
           // }
        }

        public void GuardarLista()
        {
            if (Existe)
            {
                IRestResponse response = servicioListas.EditarLista(ListaActual.Id, ListaActual.Name, ListaActual.Description);
                if (response.StatusCode != System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensaje(response.ErrorException.Message, "Error en edición de la lista", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                IRestResponse response = servicioListas.CrearLista(ListaActual);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _ = response.Content;
                    JObject content = JsonConvert.DeserializeObject<JObject>(response.Content);
                    int id = content["Id"].Value<int>();
                    ListaActual.Id = id;
                    Existe = true;
                }
                else
                {
                    servicioDialogo.MostrarMensaje(response.ErrorException.Message, "Error en creación de la lista", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            WeakReferenceMessenger.Default.Send(new ListaCreadaEditadaMessage(ListaActual));
        }
    }
}
