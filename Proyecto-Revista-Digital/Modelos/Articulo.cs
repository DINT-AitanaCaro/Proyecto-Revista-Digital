using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Modelos
{
    class Articulo : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private int idAutor;
        public int IdAutor
        {
            get { return idAutor; }
            set { SetProperty(ref idAutor, value); }
        }

        private int idSeccion;
        public int IdSeccion
        {
            get { return idSeccion; }
            set { SetProperty(ref idSeccion, value); }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }
            set { SetProperty(ref titulo, value); }
        }

        private string imagen;
        public string Imagen
        {
            get { return imagen; }
            set { SetProperty(ref imagen, value); }
        }

        private string contenido;
        public string Contenido
        {
            get { return contenido; }
            set { SetProperty(ref contenido, value); }
        }

        private bool publicado;
        public bool Publicado
        {
            get { return publicado; }
            set { SetProperty(ref publicado, value); }
        }

        public Articulo(int idAutor, int idSeccion, string titulo, string imagen, string contenido, bool publicado)
        {
            IdAutor = idAutor;
            IdSeccion = idSeccion;
            Titulo = titulo;
            Imagen = imagen;
            Contenido = contenido;
            Publicado = publicado;
        }

        public Articulo(int id, int idAutor, int idSeccion, string titulo, string imagen, string contenido, bool publicado) : this( idAutor,  idSeccion,  titulo,  imagen,  contenido,  publicado)
        {
            Id = id;
        }
    }
}
