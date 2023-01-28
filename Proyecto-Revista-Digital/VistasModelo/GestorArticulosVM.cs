using CommunityToolkit.Mvvm.ComponentModel;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestorArticulosVM : ObservableObject
    {
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

        public GestorArticulosVM()
		{
			Articulos = new ObservableCollection<Articulo>();
            ArticuloSeleccionado = new Articulo();
		}

	}
}
