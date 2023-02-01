using CommunityToolkit.Mvvm.ComponentModel;
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
        private ListaTerminos _lista;

        public ListaTerminos ListaActual
        {
            get { return _lista; }
            set { SetProperty(ref _lista, value); }
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
            Modo = ListaActual.Id == 0 ? "Crear Autor" : "Editar Autor";
        }

        public void GuardarLista()
        {
            //servicioListas
        }
    }
}
