using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Revista_Digital.Servicios;
using System.Collections.ObjectModel;
using Proyecto_Revista_Digital.Modelos;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestorAutoresVM : ObservableObject
    {
        //private ServicioAutor servicio;

        private string nombreAutor;

        public string NombreAutor
        {
            get { return nombreAutor; }
            set { SetProperty(ref nombreAutor, value); }
        }

        private string imagenAutor;

        public string ImagenAutor
        {
            get { return imagenAutor; }
            set { SetProperty(ref imagenAutor, value); }
        }

        private string nicknameAutor;

        public string NicknameAutor
        {
            get { return nicknameAutor; }
            set { SetProperty(ref nicknameAutor, value); }
        }

        private ObservableCollection<Autor> autores;
        public ObservableCollection<Autor> Autores
        {
            get { return autores; }
            set { SetProperty(ref autores, value); }
        }

        public GestorAutoresVM()
        {
            //servicio = new ServiciosAutor();
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

        }

        public void EliminarAutor()
        {

        }

    }
}
