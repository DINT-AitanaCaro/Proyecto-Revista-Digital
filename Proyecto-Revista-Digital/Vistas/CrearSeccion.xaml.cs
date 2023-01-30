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
using System.Windows.Shapes;

namespace Proyecto_Revista_Digital.Vistas
{
    /// <summary>
    /// Lógica de interacción para CrearSeccion.xaml
    /// </summary>
    public partial class CrearSeccion : Window
    {
        private CrearSeccionVM vm;
        public CrearSeccion()
        {
            InitializeComponent();
            vm = new CrearSeccionVM();
            this.DataContext = vm;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            //vm.GuardarSeccion();
            DialogResult = true;
        }
    }
}
