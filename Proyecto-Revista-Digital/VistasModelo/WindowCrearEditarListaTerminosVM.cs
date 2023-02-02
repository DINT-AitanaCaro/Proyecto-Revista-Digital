using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class WindowCrearEditarListaTerminosVM : ObservableObject
    {
        public RelayCommand AñadirTerminoCommand { get;  }
        public RelayCommand EliminarTerminoCommand { get;  }
        public RelayCommand EliminarTodosTerminoCommand { get;  }
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

        private string _modo;

        public string Modo
        {
            get { return _modo; }
            set { SetProperty(ref _modo, value); }
        }

        private ServicioAPIRestListasTerminos servicioListas;
        public WindowCrearEditarListaTerminosVM()
        {
            servicioListas = new ServicioAPIRestListasTerminos();
            ListaActual = WeakReferenceMessenger.Default.Send<EnviarListaMessage>();
            Modo = ListaActual.Id == 0 ? "Crear Lista" : "Editar Lista";
            AñadirTerminoCommand = new RelayCommand(CrearTermino);
            EliminarTerminoCommand = new RelayCommand(EliminarTermino);
            EliminarTodosTerminoCommand = new RelayCommand(EliminarTodosTermino);
        }
        
        public void CrearTermino()
        {
            servicioListas.AñadirTermino(ListaActual.Id, NuevoTermino);
        }
        public void EliminarTermino()
        {
            servicioListas.EliminarTermino(ListaActual.Id, NuevoTermino);
        }
        public void EliminarTodosTermino()
        {
            servicioListas.EliminarTodosTerminos(ListaActual.Id);
        }
        public void GuardarLista()
        {
            if(Modo == "Crear Lista")
            {
                servicioListas.CrearLista(ListaActual);
            } 
            else
            {
                //update
            }
        }
    }
}
