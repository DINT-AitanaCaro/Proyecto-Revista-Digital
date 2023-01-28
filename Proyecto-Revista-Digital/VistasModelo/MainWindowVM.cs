using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ServicioSQLite servicioSQLite;
        private ServicioNavegacion serviciosVentanas;

        private UserControl contenidoVentana;

        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana,value); }
        }

        public RelayCommand GestionarAutoresCommand { get; }
        public RelayCommand GestionarArticulosCommand { get; }
        public RelayCommand PublicarPaginaCommand { get; }
        public RelayCommand NuevoArticuloCommand { get; }

        private bool refrescar;

        public bool Refrescar
        {
            get { return refrescar; }
            set { SetProperty(ref refrescar, value); }
        }


        public MainWindowVM()
        {
            servicioSQLite = new ServicioSQLite();
            servicioSQLite.CrearBD();

            GestionarAutoresCommand = new RelayCommand(GestionarAutores);
            GestionarArticulosCommand = new RelayCommand(GestionarArticulos);
            PublicarPaginaCommand = new RelayCommand(PublicarPagina);
            NuevoArticuloCommand = new RelayCommand(NuevoArticulo);

            this.serviciosVentanas = new ServicioNavegacion();

            WeakReferenceMessenger.Default.Register<RefrescarVentanaMessage>(this, (r, m) => 
            {
                Refrescar = m.Value;

                if (Refrescar)
                {
                    ContenidoVentana = this.serviciosVentanas.CargarGestionAutores();
                }
            }); 
        }

        public void GestionarAutores()
        {
            ContenidoVentana = this.serviciosVentanas.CargarGestionAutores();
        }

        public void GestionarArticulos()
        {
            ContenidoVentana = this.serviciosVentanas.CargarGestionArticulos();
        }

        public void PublicarPagina()
        {
            Process.Start("\"..\\..\\RevistaOnline\\index.html\"");
        }

        public void NuevoArticulo()
        {
            ContenidoVentana = serviciosVentanas.CargarNuevoArticulo();
        }
    }
}
