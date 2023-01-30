using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Revista_Digital.Mensajes;
using Proyecto_Revista_Digital.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Revista_Digital.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private ServicioSQLite servicioSQLite;
        private ServicioNavegacion serviciosVentanas;

        private UserControl contenidoVentana;

        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana,value); }
        }

        public RelayCommand GestionarAutoresCommand { get; }
        public RelayCommand GestionarArticulosCommand { get; }
        public RelayCommand PublicarPaginaCommand { get; }
        public RelayCommand NuevoArticuloCommand { get; }

        private bool refrescar;

        public bool Refrescar
        {
            get { return refrescar; }
            set { SetProperty(ref refrescar, value); }
        }


        public MainWindowVM()
        {
            servicioSQLite = new ServicioSQLite();
            servicioSQLite.CrearBD();

            GestionarAutoresCommand = new RelayCommand(GestionarAutores);
            GestionarArticulosCommand = new RelayCommand(GestionarArticulos);
            PublicarPaginaCommand = new RelayCommand(PublicarPagina);
            NuevoArticuloCommand = new RelayCommand(NuevoArticulo);

            this.serviciosVentanas = new ServicioNavegacion();
        }

        public void GestionarAutores()
        {
            ContenidoVentana = this.serviciosVentanas.CargarGestionAutores();
        }

        public void GestionarArticulos()
        {
            ContenidoVentana = this.serviciosVentanas.CargarGestionArticulos();
        }

        public void PublicarPagina()
        {
            String indexHTML = "";
            String sectionHTML = "<section>";

            for (int i = 0; i < 5; i++)
            {
                sectionHTML += "<h1>Encabezado Prueba " + i+1 + "</h1>";
            }
            sectionHTML += "</section>";

            indexHTML = "<!DOCTYPE html>" +
                            "< html lang = \"en\" >"+
                                "< head >"+
                                    "< meta charset = \"UTF-8\" >" +
                                    "< meta http - equiv = \"X-UA-Compatible\" content = \"IE=edge\" >" +
                                    "< meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\" >" +
                                    "< title >DAM's Mondogo</title>" +
                                    "< link rel = \"stylesheet\" href = \"./RevistaOnline/styles.css\" >" +
                                    "< script src = \"https://kit.fontawesome.com/afd6aa68df.js\" crossorigin = \"anonymous\"></ script >" +
                                "</ head >" +
                                "< body >" +
                                    "< header >" +
                                        "< img src = \"./img/logoRevista.png\" >" +
                                        "< div class=\"derecha\">" +
                                        "<nav>" +
                                            "<ul>" +
                                                "<li>" +
                                                    "<a href = \"#\" > Secciones </ a >" +
                                                "</ li >" +
                                                "< li >" +
                                                    "< a href=\"#\">Contáctanos</a>" +
                                                "</li>" +
                                                "<li>" +
                                                    "<a href = \"#\" > Suscríbete </ a >" +
                                                "</ li >" +
                                                "< li >" +
                                                    "< a href=\"#\">Sobre nosotros</a>" +
                                                "</li>" +
                                            "</ul>" +
                                        "</nav>" +
                                        "</div>" +
                                        "<div class=\"box\">" +
                                            "<form name = \"search\" >" +
                                                "< input type=\"text\" class=\"input\" name=\"buscador\">" +
                                            "</form>" +
                                                "<i class=\"fas fa-search\"></i>" +
                                        "</div>" +
                                    "</header>" +
                                    sectionHTML +
                                    "<footer>" +
                                        "< div class=\"pie\">" +
                                        "<h3>Quiénes Somos</h3>" +
                                            "<div>" +
                                                "<span>Quiénes Somos</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Nuestras Oficinas</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Condiciones de Compra</span>" +
                                            "</div>" +
                                            "<div>" +
                                            "<span>Aviso legal</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Privacidad</span>" +
                                            "</div>" +
                                        "</div>" +
                                        "<div class=\"pie\">" +
                                            "<h3>Contactar</h3>" +
                                            "<div>" +
                                                "<span>Centro de Soporte</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Contacto</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Opina y Gana</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Trabaja con Nosotros</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Política de cookies</span>" +
                                            "</div>" +
                                        "</div>" +
                                        "<div class=\"pie\">" +
                                        "<h3>Comunidad</h3>" +
                                            "<div>" +
                                                "<span>Instagram</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Twitter</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Facebook</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Youtube</span>" +
                                            "</div>" +
                                            "<div>" +
                                                "<span>Telegram</span>" +
                                            "</div>" +
                                        "</div>" +
                                        "<iframe src = \"https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d2212.1479636783683!2d-0.4909920576212618!3d38.36171822424404!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x2e616f3c6039c9e9!2sI.E.S%20Doctor%20Balmis!5e0!3m2!1ses!2ses!4v1674495639629!5m2!1ses!2ses\" width=\"600\" height=\"450\" style=\"border:0;\" allowfullscreen=\"\" loading=\"lazy\" referrerpolicy=\"no-referrer-when-downgrade\"></iframe>" +
                                        "<h1>© 2023 DAM's Space - Todos los derechos reservados</h1>" +
                                    "</footer>" +
                                "</body>" +
                                "</html>";

            string path = "index.html";

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(indexHTML); 
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            Process.Start("index.html");
        }

        public void NuevoArticulo()
        {
            ContenidoVentana = serviciosVentanas.CargarNuevoArticulo();
        }
    }
}
