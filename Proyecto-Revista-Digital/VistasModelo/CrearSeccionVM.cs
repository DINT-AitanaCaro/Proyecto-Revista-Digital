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
    class CrearSeccionVM : ObservableRecipient
    {
        private Seccion nuevaSeccion;

        public Seccion NuevaSeccion
        {
            get { return nuevaSeccion; }
            set { SetProperty(ref nuevaSeccion, value); }
        }

        public RelayCommand AñadirSeccionCommand { get; }
        private ServicioSeccion servicioSeccion;
        private ServicioDialogo servicioDialogo;

        public CrearSeccionVM()
        {
            NuevaSeccion = new Seccion();
            servicioSeccion = new ServicioSeccion();
            servicioDialogo = new ServicioDialogo();
            AñadirSeccionCommand = new RelayCommand(AñadirSeccion);
            
        }

        public void AñadirSeccion()
        {
            if (NuevaSeccion.NombreSeccion != null)
            {
                if (!RepiteSeccion())
                {
                    servicioSeccion.AddSeccion(NuevaSeccion);
                    WeakReferenceMessenger.Default.Send(new EnviarSeccionMessage(NuevaSeccion));
                }
                else
                {
                    servicioDialogo.MostrarMensaje("No se pueden agregar dos secciones con el mismo nombre", "ERROR - Seccion", MessageBoxButton.OK,MessageBoxImage.Error);
                }

            }
            else
            {
                servicioDialogo.MostrarMensaje("El campo del nombre de la seccion no puede estar vacio", "ADVERTENCIA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private bool RepiteSeccion()
        {
            ObservableCollection<Seccion> secciones = servicioSeccion.GetSecciones();
            foreach (Seccion item in secciones)
            {
                if (NuevaSeccion.NombreSeccion.Equals(item.NombreSeccion))
                    return true;
            }
            return false;
        }
    }
}
