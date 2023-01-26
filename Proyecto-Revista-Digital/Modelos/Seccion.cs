using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Modelos
{
    class Seccion:ObservableObject
    {
        private int idSeccion;

        public int IdSeccion
        {
            get { return idSeccion; }
            set { SetProperty(ref idSeccion, value); }
        }

        private string nombreSeccion;

        public string NombreSeccion
        {
            get { return nombreSeccion; }
            set { SetProperty(ref nombreSeccion, value); }
        }

        public Seccion()
        {
        }

        public Seccion(int idSeccion, string nombreSeccion)
        {
            this.idSeccion = idSeccion;
            this.nombreSeccion = nombreSeccion;
        }

        public Seccion(string nombreSeccion)
        {
            this.nombreSeccion = nombreSeccion;
        }
    }
}
