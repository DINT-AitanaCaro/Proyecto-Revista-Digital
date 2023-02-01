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
        public ObservableCollection<ListaTerminos> GetListas()
        {
            var client = new RestClient("https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            var response = client.Execute(request);
            Thread.Sleep(2000);
            return JsonConvert.DeserializeObject<ObservableCollection<ListaTerminos>>(response.Content);
        }

        public ObservableCollection<string> GetTerminos(int idLista)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            request.AddHeader("Accept-", "3db2c831f29248f883bf33d925347349");
            var response = client.Execute(request);
            Thread.Sleep(2000);
            JObject content = JsonConvert.DeserializeObject<JObject>(response.Content);
            JArray terms = content["Data"].Value<JObject>()["Terms"].Value<JArray>();
            ObservableCollection<string> terminos = new ObservableCollection<string>();
            foreach (JObject i in terms)
            {
                string t = i["Term"].Value<string>();
                terminos.Add(t);
            }
            return terminos;
        }
    }
}
