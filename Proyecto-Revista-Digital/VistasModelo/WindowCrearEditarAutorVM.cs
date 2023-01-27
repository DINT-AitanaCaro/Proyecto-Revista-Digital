using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
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

        public WindowCrearEditarAutorVM()
        {
            WeakReferenceMessenger.Default.Register<EnviarAutorMessage>(this, (r, m) =>
            {
                AutorActual = m.Value;
            });
        }
    }
}
