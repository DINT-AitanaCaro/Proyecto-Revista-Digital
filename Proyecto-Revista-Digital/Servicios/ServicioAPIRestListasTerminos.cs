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
        private const int WAIT_TIME = 2500;
        public ObservableCollection<ListaTerminos> GetListas()
        {
            RestClient client = new RestClient("https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return JsonConvert.DeserializeObject<ObservableCollection<ListaTerminos>>(response.Content);
        }

        public ObservableCollection<string> GetTerminos(int idLista)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}/terms?language=spa");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            ObservableCollection<string> terminos = new ObservableCollection<string>();
            if (response.Content != null)
            {
                JObject content = JsonConvert.DeserializeObject<JObject>(response.Content);
                JArray terms = content["Data"].Value<JObject>()["Terms"].Value<JArray>();

                foreach (JObject i in terms)
                {
                    string t = i["Term"].Value<string>();
                    terminos.Add(t);
                }
            }
            return terminos;
        }

        public bool AñadirTermino(int listId, string term)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}/terms/{term}?language=spa");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            Thread.Sleep(WAIT_TIME);
            var response = client.Execute(request);
            RefrechSearchIndex(listId);
            return response.IsSuccessful;
        }

        public void RefrechSearchIndex(int listId)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}/RefreshIndex?language=spa");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
        }
    }
}
