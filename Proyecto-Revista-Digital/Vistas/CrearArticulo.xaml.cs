using Proyecto_Revista_Digital.VistasModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Revista_Digital.Vistas
{
    /// <summary>
    /// Lógica de interacción para CrearArticulo.xaml
    /// </summary>
    public partial class CrearArticulo : UserControl
    {
        private CrearArticuloVM vm;
        public CrearArticulo()
        {
            InitializeComponent();
            vm = new CrearArticuloVM();
            this.DataContext = vm;
        }
    }
}
