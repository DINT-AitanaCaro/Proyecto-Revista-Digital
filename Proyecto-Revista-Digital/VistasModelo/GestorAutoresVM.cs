﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Revista_Digital.Servicios;
using System.Collections.ObjectModel;
using Proyecto_Revista_Digital.Modelos;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestorAutoresVM : ObservableObject
    {
        private ServicioAutor servicioAutor;

        private ObservableCollection<Autor> autores;
        public ObservableCollection<Autor> Autores
        {
            get { return autores; }
            set { SetProperty(ref autores, value); }
        }

        private Autor autorSeleccionado;

        public Autor AutorSeleccionado
        {
            get { return autorSeleccionado; }
            set { autorSeleccionado = value; }
        }


        public RelayCommand EditarAutorCommand { get; }
        public RelayCommand NuevoAutorCommand { get; }
        public RelayCommand EliminarAutorCommand { get; }

        public GestorAutoresVM()
        {
            this.servicioAutor = new ServicioAutor();
            EditarAutorCommand = new RelayCommand(EditarAutor);
            NuevoAutorCommand = new RelayCommand(AñadirAutor);
            EliminarAutorCommand = new RelayCommand(EliminarAutor);
            Autores = new ObservableCollection<Autor>();
            GenerarAutores();
        }

        public void GenerarAutores()
        {
            Autores.Add(new Autor(1001, "Jose Alfredo", "./fotoJose.jpg", "Jusep", "Instagram"));
            Autores.Add(new Autor(2002, "Ian Tauzy", "./fotoIan.jpg", "Naiian", "Twitter"));
            Autores.Add(new Autor(3003, "Aitana Caro", "./fotoAitana.jpg", "Padna", "Facebook"));
        }

        public void AñadirAutor()
        {

        }

        public void EditarAutor()
        {

            
            MessageBoxResult result = MessageBox.Show("¿Deseas guardar los cambios?", "Advertencia", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    MessageBox.Show("Se supone que ahora los guardas", "Info");
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        public void EliminarAutor()
        {
            servicioAutor.DeleteAutor(AutorSeleccionado.Id);
        }
    }
}
