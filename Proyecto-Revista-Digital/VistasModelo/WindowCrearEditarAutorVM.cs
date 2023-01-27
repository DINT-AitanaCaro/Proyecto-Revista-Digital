using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class WindowCrearEditarAutorVM : ObservableObject
    {
        private Autor _autor;

        public Autor AutorActual
        {
            get { return _autor; }
            set { SetProperty(ref _autor, value); }
        }

        private string _modo;

        public string Modo
        {
            get { return _modo; }
            set { SetProperty(ref _modo, value); }
        }


        private ObservableCollection<string> _redesSociales;

        public ObservableCollection<string> RedesSociales
        {
            get { return _redesSociales; }
            set { SetProperty(ref _redesSociales, value); }
        }

        public WindowCrearEditarAutorVM()
        {
            AutorActual = WeakReferenceMessenger.Default.Send<EnviarAutorMessage>();
            RedesSociales = new ObservableCollection<string>() { "Instagram", "Twitter", "Facebook" };
            if (string.IsNullOrEmpty(AutorActual.Nombre)) Modo = "Crear Autor";
            else Modo = "Editar Autor";
        }
    }
}
