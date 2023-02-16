using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Revista_Digital.Modelos;
using Proyecto_Revista_Digital.Servicios;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class GestorArticulosVM : ObservableObject
    {
        private ServicioModeracionContenido servicioModeracion;
        private ServicioAzure servicioAzure;
        private ServicioArticulo servicioArticulo;
		private ObservableCollection<Articulo> articulos;

		public ObservableCollection<Articulo> Articulos
        {
			get { return articulos; }
			set { SetProperty(ref articulos, value); }
		}

        private Articulo articuloSeleccionado;

        public Articulo ArticuloSeleccionado
        {
            get { return articuloSeleccionado; }
            set { SetProperty(ref articuloSeleccionado, value); }
        }

        public RelayCommand CargarArticulosCommand { get; }
        public RelayCommand PublicarCommand { get; }
        public RelayCommand EliminarCommand { get; }

        public GestorArticulosVM()
		{
			Articulos = new ObservableCollection<Articulo>();
            ArticuloSeleccionado = new Articulo();

            CargarArticulosCommand = new RelayCommand(CargarArticulos);
            PublicarCommand = new RelayCommand(PublicarArticulo);
            EliminarCommand = new RelayCommand(EliminarArticulo);

            servicioModeracion = new ServicioModeracionContenido();
            servicioAzure = new ServicioAzure();
            servicioArticulo = new ServicioArticulo();

            CargarArticulos();
		}

        public void CargarArticulos()
        {
            Articulos = new ObservableCollection<Articulo>();
            foreach (Articulo a in servicioArticulo.GetArticulos())
            {
                if (!a.Publicado)
                {
                    Articulos.Add(a);
                }
            }
        }

        public void PublicarArticulo()
        {
            if (ArticuloSeleccionado != null)
            {
                if (ComprobarArticulo())
                {
                    ArticuloSeleccionado.UrlPdf = servicioAzure.AlmacenarPDFEnLaNube(GenerarPDF());
                    servicioArticulo.UpdateUrlPdf(ArticuloSeleccionado.Id, ArticuloSeleccionado.UrlPdf);
                    servicioArticulo.PublicarArticulo(ArticuloSeleccionado.Id);
                    CargarArticulos();
                }
            }

        }

        public bool ComprobarArticulo()
        {
            int num = 0;
            ObservableCollection<string> palabrasMalsonantes = servicioModeracion.AnalizarTexto(ArticuloSeleccionado.Titulo);
            num = palabrasMalsonantes.Count();

            palabrasMalsonantes = servicioModeracion.AnalizarTexto(ArticuloSeleccionado.Contenido);
            num = palabrasMalsonantes.Count();

            if (num > 0)
            {
                return false;
            }
            return true;
        }

        public void EliminarArticulo()
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este artículo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                servicioArticulo.DeleteArticulo(ArticuloSeleccionado.Id);
                CargarArticulos();
            }
        }

        public string GenerarPDF()
        {
            String ruta = "./Basura";

            if (!File.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);
            }

            ruta += "/imagen" + ArticuloSeleccionado.Titulo + ".jpg";

            

            Document
                .Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(1, Unit.Inch);

                        page.Header()
                            .AlignCenter()
                            .Text(ArticuloSeleccionado.Titulo)
                            .FontSize(48)
                            .SemiBold();

                        page.Content()
                            .Column(column =>
                            {
                                column.Spacing(0.5f, Unit.Inch);

                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(new Uri(ArticuloSeleccionado.Imagen), ruta);
                                }

                                column.Item()
                                    .Image(ruta);

                                column.Item().AlignRight().Row(y =>
                                {
                                    string redSocial = "";
                                    if (ArticuloSeleccionado.AutorArticulo.Social.Equals("Twitter")) {
                                        redSocial = "./assets/twitter.ico";
                                    } else if (ArticuloSeleccionado.AutorArticulo.Social.Equals("Instagram")) {
                                        redSocial = "./assets/instagram.ico";
                                    } else {
                                        redSocial = "./assets/facebook.ico";
                                    }
                                    y.ConstantItem(20)
                                        .Image(redSocial);

                                    y.RelativeItem()
                                       .Text("@" + ArticuloSeleccionado.AutorArticulo.Nickname);
                                });

                                column.Item()
                                    .Text(ArticuloSeleccionado.Contenido)
                                    .FontSize(18);
                            });
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(x => x.FontSize(18));

                                text.CurrentPageNumber();
                                text.Span(" / ");
                                text.TotalPages();
                            });
                    });
                }).GeneratePdf("./Basura/" + ArticuloSeleccionado.Titulo + ".pdf");

            return "./Basura/" + ArticuloSeleccionado.Titulo +".pdf";
        }
	}
}