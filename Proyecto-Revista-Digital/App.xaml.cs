using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Revista_Digital
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Proyecto_Revista_Digital.Properties.Settings.Default.ClaveSyncfusion);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Process.Start("cleanTrash.bat");
        }
    }
}
