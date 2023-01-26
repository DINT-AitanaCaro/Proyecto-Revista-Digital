using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Revista_Digital.Servicios;

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

        public GestorAutoresVM()
        {
            //servicio = new ServiciosAutor();
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
