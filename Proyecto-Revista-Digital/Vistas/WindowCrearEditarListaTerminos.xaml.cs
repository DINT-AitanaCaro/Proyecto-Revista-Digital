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

namespace Proyecto_Revista_Digital.VistasModelo
{
    /// <summary>
    /// Lógica de interacción para WindowCrearEditarListaTerminos.xaml
    /// </summary>
    public partial class WindowCrearEditarListaTerminos : Window
    {
        WindowCrearEditarListaTerminosVM vm = new WindowCrearEditarListaTerminosVM();
        public WindowCrearEditarListaTerminos()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            vm.GuardarLista();
            DialogResult = true;
        }
    }
}
