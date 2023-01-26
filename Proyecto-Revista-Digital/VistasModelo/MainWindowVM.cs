using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ServicioNavegacion serviciosVentanas;

        private UserControl contenidoVentana;

        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana,value); }
        }

        public RelayCommand GestionarAutoresCommand { get; }
        public RelayCommand GestionarArticulosCommand { get; }

        public MainWindowVM()
        {
            GestionarAutoresCommand = new RelayCommand(GestionarAutores);
            GestionarArticulosCommand = new RelayCommand(GestionarArticulos);
            this.serviciosVentanas = new ServicioNavegacion();
        }

        public void GestionarAutores()
        {
            ContenidoVentana = this.serviciosVentanas.CargarGestionAutores();
        }

        public void GestionarArticulos()
        {

        }
    }
}
