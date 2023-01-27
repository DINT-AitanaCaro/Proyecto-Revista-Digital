﻿using CommunityToolkit.Mvvm.ComponentModel;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestarArticulosVM : ObservableObject
    {
        private Articulo articuloNuevo;

        public Articulo ArticuloNuevo
        {
            get { return articuloNuevo; }
            set { SetProperty(ref articuloNuevo, value); }
        }


        private ObservableCollection<Seccion> listaSecciones;

        public ObservableCollection<Seccion> ListaSecciones
        {
            get { return listaSecciones; }
            set { SetProperty(ref listaSecciones, value); }
        }

        private ObservableCollection<Autor> listaAutores;

        public ObservableCollection<Autor> ListaAutores
        {
            get { return listaAutores; }
            set { SetProperty(ref listaAutores, value); }
        }

        private Seccion seccionArticulo;

        public Seccion SeccionArticulo
        {
            get { return seccionArticulo; }
            set { SetProperty(ref seccionArticulo, value); }
        }

        private Autor autorArticulo;

        public Autor AutorArticulo
        {
            get { return autorArticulo; }
            set { SetProperty(ref autorArticulo, value); }
        }

        public GestarArticulosVM()
        {
            ArticuloNuevo = new Articulo();
            ListaSecciones = new ObservableCollection<Seccion>();
            ListaAutores = new ObservableCollection<Autor>();
            SeccionArticulo = new Seccion();
            AutorArticulo = new Autor();
        }
    }
}
