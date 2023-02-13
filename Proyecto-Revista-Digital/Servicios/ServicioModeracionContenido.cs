using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioModeracionContenido : ObservableObject
    {
        public ObservableCollection<string> AnalizarTexto(string texto)
        {
            string clave = Properties.Settings.Default.ClaveAzureListas;
            string endpoint = Properties.Settings.Default.EndPoint;
            var client = new RestClient(endpoint);
            var request = new RestRequest("/contentmoderator/moderate/v1.0/ProcessText/Screen", Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", clave);
            request.AddHeader("Content-Type", "text/plain");
            request.AddParameter("listId", Properties.Settings.Default.IdListaAplicada, ParameterType.QueryString);
            request.AddParameter("text/plain", texto, ParameterType.RequestBody);
            var response = client.Execute(request);
            Root resultado = JsonConvert.DeserializeObject<Root>(response.Content);
            List<Termino> listaTerminos = resultado.Terms;
            ObservableCollection<string> palabrasMalas = new ObservableCollection<string>();

            foreach (var item in listaTerminos)
            {
                palabrasMalas.Add(item.Term);
            }

            return palabrasMalas;
        }



        public class Root
        {
            public string OriginalText { get; set; }
            public string NormalizedText { get; set; }
            public object Misrepresentation { get; set; }
            public string Language { get; set; }
            public List<Termino> Terms { get; set; }
            public Status Status { get; set; }
            public string TrackingId { get; set; }
        }

        public class Status
        {
            public int Code { get; set; }
            public string Description { get; set; }
            public object Exception { get; set; }
        }

        public class Termino
        {
            public int Index { get; set; }
            public int OriginalIndex { get; set; }
            public int ListId { get; set; }
            public string Term { get; set; }
        }
    }
}
