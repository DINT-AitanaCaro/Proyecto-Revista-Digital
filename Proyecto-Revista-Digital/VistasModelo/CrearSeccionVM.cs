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
    class CrearSeccionVM : ObservableObject
    {
        private Seccion nuevaSeccion;

        public Seccion NuevaSeccion
        {
            get { return nuevaSeccion; }
            set { SetProperty(ref nuevaSeccion, value); }
        }

        //public RelayCommand AñadirSeccionCommand { get; }
        private ServicioSeccion servicioSeccion;

        public CrearSeccionVM()
        {
            NuevaSeccion = new Seccion();
            NuevaSeccion = WeakReferenceMessenger.Default.Send<EnviarSeccionMessage>();
            servicioSeccion = new ServicioSeccion();
            //AñadirSeccionCommand = new RelayCommand(AddSeccion);
            
        }

        public void GuardarSeccion()
        {
            servicioSeccion.AddSeccion(NuevaSeccion);
        }
    }
}
