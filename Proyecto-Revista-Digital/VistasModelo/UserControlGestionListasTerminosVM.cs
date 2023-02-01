using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Revista_Digital.Modelos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class UserControlGestionListasTerminosVM : ObservableObject
    {
        public RelayCommand MarcarComoAplicadaCommnad { get; }
        public RelayCommand AddListaCommand { get; }

        private ObservableCollection<ListaTerminos> terminos;

        public ObservableCollection<ListaTerminos> Terminos
        {
            get { return terminos; }
            set { SetProperty(ref terminos, value); }
        }

        private ListaTerminos listaSeleccionada;
        public ListaTerminos ListaSeleccionada
        {
            get { return listaSeleccionada; }
            set { SetProperty(ref listaSeleccionada, value); }
        }

        public UserControlGestionListasTerminosVM()
        {
            MarcarComoAplicadaCommnad = new RelayCommand(MarcarListaComoAplicada);
        }

        public void MarcarListaComoAplicada()
        {
            Terminos.ToList().ForEach(list => { if (list.Aplicada) { list.Aplicada = false; } });
            ListaSeleccionada.Aplicada = true;
        }
    }
}
