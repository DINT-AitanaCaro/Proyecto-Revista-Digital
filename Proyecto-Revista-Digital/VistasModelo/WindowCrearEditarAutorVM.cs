using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class WindowCrearEditarAutorVM : ObservableObject
    {
        private Autor _autor;

        public Autor AutorActual
        {
            get { return _autor; }
            set { SetProperty(ref _autor, value); }
        }

        private string _modo;

        public string Modo
        {
            get { return _modo; }
            set { SetProperty(ref _modo, value); }
        }

        private ObservableCollection<string> _redesSociales;
        private ServicioDialogo servicioDialogo = new ServicioDialogo();
        private ServicioAzure servicioAzure = new ServicioAzure();
        private ServicioAutor servicioAutor = new ServicioAutor();

        public ObservableCollection<string> RedesSociales
        {
            get { return _redesSociales; }
            set { SetProperty(ref _redesSociales, value); }
        }

        public RelayCommand CommandSeleccionImagen { get; }
        public WindowCrearEditarAutorVM()
        {
            AutorActual = WeakReferenceMessenger.Default.Send<EnviarAutorMessage>();
            RedesSociales = new ObservableCollection<string>() { "Instagram", "Twitter", "Facebook" };
            Modo = string.IsNullOrEmpty(AutorActual.Nombre) ? "Crear Autor" : "Editar Autor";
            CommandSeleccionImagen = new RelayCommand(SeleccionImagen);
        }

        public void GuardarAutor()
        {
            servicioAutor.AddAutor(AutorActual);
        }

        public void SeleccionImagen()
        {
            string file = servicioDialogo.DialogoAbrirFichero();
            AutorActual.Imagen = file != null ? servicioAzure.AlmacenarImagenEnLaNube(file) : string.Empty;
        }
    }
}
