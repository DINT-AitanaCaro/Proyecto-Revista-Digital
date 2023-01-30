using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    class GestorArticulosVM : ObservableObject
    {
        private ServicioArticulo servicioArticulo;
		private ObservableCollection<Articulo> articulos;

		public ObservableCollection<Articulo> Articulos
        {
			get { return articulos; }
			set { SetProperty(ref articulos, value); }
		}

        private Articulo articuloSeleccionado;

        public Articulo ArticuloSeleccionado
        {
            get { return articuloSeleccionado; }
            set { SetProperty(ref articuloSeleccionado, value); }
        }

        public RelayCommand CargarArticulosCommand { get; }
        public RelayCommand PublicarCommand { get; }
        public RelayCommand EliminarCommand { get; }

        public GestorArticulosVM()
		{
			Articulos = new ObservableCollection<Articulo>();
            ArticuloSeleccionado = new Articulo();

            CargarArticulosCommand = new RelayCommand(CargarArticulos);
            PublicarCommand = new RelayCommand(PublicarArticulo);
            EliminarCommand = new RelayCommand(EliminarArticulo);

            servicioArticulo = new ServicioArticulo();
            CargarArticulos();
		}

        public void CargarArticulos()
        {
            Articulos = new ObservableCollection<Articulo>();
            foreach (Articulo a in servicioArticulo.GetArticulos())
            {
                if (!a.Publicado)
                {
                    Articulos.Add(a);
                }
            }
        }

        public void PublicarArticulo()
        {
            servicioArticulo.PublicarArticulo(ArticuloSeleccionado.Id);
            CargarArticulos();
        }

        public void EliminarArticulo()
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este artículo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                servicioArticulo.DeleteArticulo(ArticuloSeleccionado.Id);
                CargarArticulos();
            }
        }
	}
}