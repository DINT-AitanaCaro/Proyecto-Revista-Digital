using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Modelos
{
    class Autor : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }



        private string nombre;
        public string Nombre
        {
            get { return nombre; }
            set { SetProperty(ref nombre, value); }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set { SetProperty(ref nickname, value); }
        }

        private string social;

        public string Social
        {
            get { return social; }
            set { SetProperty(ref social, value); }
        }
        public Autor() { }

        public Autor(string nombre, string imagen, string nickname, string social)
        {
            Nombre = nombre;
            Imagen = imagen;
            Nickname = nickname;
            Social = social;
        }

        public Autor(int id, string nombre, string imagen, string nickname, string social) : this(nombre, imagen, nickname, social)
        {
            Id = id;
        }
    }
}
