using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto_Revista_Digital.Modelos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    class ServicioAPIRestListasTerminos
    {
        private const int WAIT_TIME = 2000;
        public ObservableCollection<ListaTerminos> GetListas()
        {
            RestClient client = new RestClient("https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return JsonConvert.DeserializeObject<ObservableCollection<ListaTerminos>>(response.Content);
        }

        public IRestResponse CrearLista(ListaTerminos listaTerminos)
        {
            var client = new RestClient("https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists?language=spa");
            var request = new RestRequest(Method.POST);
            string data = JsonConvert.SerializeObject(listaTerminos);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return response;
        }

        public IRestResponse EliminarLista(int listId)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return response;
        }
        
        public IRestResponse EditarLista(int listId, string name, string descripcion)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}");
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            request.AddParameter("Name", name, ParameterType.RequestBody);
            request.AddParameter("Description", descripcion, ParameterType.RequestBody);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            RefreshSearchIndex(listId);
            return response;
        }

        public ObservableCollection<string> GetTerminos(int idLista)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms?language=spa");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            ObservableCollection<string> terminos = new ObservableCollection<string>();
            if (response.Content != null)
            {
                JObject content = JsonConvert.DeserializeObject<JObject>(response.Content);
                JArray terms = content["Data"].Value<JObject>()["Terms"].Value<JArray>();

                foreach (JObject t in terms)
                {
                    string term = t["Term"].Value<string>();
                    terminos.Add(term);
                }
            }
            return terminos;
        }

        public IRestResponse AñadirTermino(int idLista, string term)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms/{term}?language=spa");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            Thread.Sleep(WAIT_TIME);
            var response = client.Execute(request);
            RefreshSearchIndex(idLista);
            return response;
        }

        public IRestResponse EliminarTermino(int idLista, string termino)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms/{termino}?language=spa");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return response;
        }

        public IRestResponse EliminarTodosTerminos(int idLista)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms?language=spa");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return response;
        }

        public void RefreshSearchIndex(int idLista)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/RefreshIndex?language=spa");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
        }
    }
}
