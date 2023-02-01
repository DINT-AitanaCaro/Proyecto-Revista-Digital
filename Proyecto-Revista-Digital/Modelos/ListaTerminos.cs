using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Modelos
{
    class ListaTerminos : ObservableObject
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private ObservableCollection<string> _terminos;
        public ObservableCollection<string> Terminos
        {
            get { return _terminos; }
            set { SetProperty(ref _terminos, value); }
        }

        private bool aplicada;

        public bool Aplicada
        {
            get { return aplicada; }
            set { SetProperty(ref aplicada, value); }
        }

        public ListaTerminos(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
