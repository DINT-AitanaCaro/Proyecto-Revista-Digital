using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        public RelayCommand GestionarAutoresCommand { get; }
        public RelayCommand GestionarArticulosCommand { get; }

        public MainWindowVM()
        {
            GestionarAutoresCommand = new RelayCommand(GestionarAutores);
            GestionarArticulosCommand = new RelayCommand(GestionarArticulos);
        }

        public void GestionarAutores()
        {

        }

        public void GestionarArticulos()
        {

        }
    }
}
