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
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Vistas;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestorAutoresVM : ObservableObject
    {
        private ServicioNavegacion servicioNavegacion;
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
            set { SetProperty(ref autorSeleccionado, value); }
        }

        private Autor autorNuevo;

        public Autor AutorNuevo
        {
            get { return autorNuevo; }
            set { SetProperty(ref autorNuevo, value); }
        }

        public RelayCommand EditarAutorCommand { get; }
        public RelayCommand NuevoAutorCommand { get; }
        public RelayCommand EliminarAutorCommand { get; }

        public GestorAutoresVM()
        {
            this.servicioNavegacion = new ServicioNavegacion();
            this.servicioAutor = new ServicioAutor();

            EditarAutorCommand = new RelayCommand(EditarAutor);
            NuevoAutorCommand = new RelayCommand(AñadirAutor);
            EliminarAutorCommand = new RelayCommand(EliminarAutor);
            AutorSeleccionado = new Autor();

            AutorNuevo = new Autor();
            Autores = new ObservableCollection<Autor>();

            CargarAutores();

            WeakReferenceMessenger.Default.Register<GestorAutoresVM, EnviarAutorMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.AutorSeleccionado);
                }
            });
        }

        public void CargarAutores()
        {
            Autores = servicioAutor.GetAutores();
        }

        public void AñadirAutor()
        {
            bool? resultado = this.servicioNavegacion.CargarNuevoEditarAutor();
            if ((bool)resultado)
            {
                new GestionAutores();
            }
        }

        public void EditarAutor()
        {
            bool? resultado = this.servicioNavegacion.CargarNuevoEditarAutor();
            new GestionAutores();
        }

        public void EliminarAutor()
        {

            servicioAutor.DeleteAutor(AutorSeleccionado.Id);
            new GestionAutores();
        }
    }
}
