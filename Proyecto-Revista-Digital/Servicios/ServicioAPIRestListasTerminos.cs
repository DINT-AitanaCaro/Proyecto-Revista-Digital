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
    /// <summary>
    ///     Servicio para la gestión de listas de terminos de Azure Content Moderator
    /// </summary>
    class ServicioAPIRestListasTerminos
    {
        /// <summary>
        ///     Tiempo de espera entre peticiones a la API Rest.
        /// </summary>
        private const int WAIT_TIME = 2000;

        /// <summary>
        ///     Método para obtener todas las listas de términos de la nube.
        /// </summary>
        /// <returns>ObservableCollection de listas de términos.</returns>
        public ObservableCollection<ListaTerminos> GetListas()
        {
            RestClient client = new RestClient("https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            IRestResponse response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return JsonConvert.DeserializeObject<ObservableCollection<ListaTerminos>>(response.Content);
        }

        /// <summary>
        ///     Método para crear una lista de términos nueva.
        /// </summary>
        /// <param name="listaTerminos">Lista de términos a crear.</param>
        /// <returns>Respuesta de la petición.</returns>
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

        /// <summary>
        ///     Mçetodo para eliminar una lista de términos.
        /// </summary>
        /// <param name="listId">I de la lista a elminar.</param>
        /// <returns>Respuesta de la petición.</returns>
        public IRestResponse EliminarLista(int listId)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{listId}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            return response;
        }

        /// <summary>
        ///     Método para editar una lista.
        /// </summary>
        /// <param name="listId">Id de la lista a modificar.</param>
        /// <param name="name">Nuevo nombre para la lista.</param>
        /// <param name="descripcion">Nueva descripción para la lista.</param>
        /// <returns>Respuesta de la petición.</returns>
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

        /// <summary>
        ///     Método para obtener todos los términos de una lista.
        /// </summary>
        /// <param name="idLista">Id de la lista de la que se desea obtener todos los términos.</param>
        /// <returns>ObservableCollection con todos los términos de la lista.</returns>
        public ObservableCollection<string> GetTerminos(int idLista)
        {
            RestClient client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms?language=spa");
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            IRestResponse response = client.Execute(request);
            
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
            Thread.Sleep(WAIT_TIME);
            return terminos;
        }

        /// <summary>
        ///     Método para añadir un término a una lista.
        /// </summary>
        /// <param name="idLista">Id de la lista a la que se desea añadir el término.</param>
        /// <param name="term">Término a añadir.</param>
        /// <returns>Respuesta de la petición.</returns>
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

        /// <summary>
        ///     Método para eliminar un término de una lista de términos.
        /// </summary>
        /// <param name="idLista">Id de la lista de la que se desea eliminar el término.</param>
        /// <param name="termino">Término a eliminar.</param>
        /// <returns>Respuesta de la petición.</returns>
        public IRestResponse EliminarTermino(int idLista, string termino)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms/{termino}?language=spa");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.ClaveAzureListas);
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            RefreshSearchIndex(idLista);
            return response;
        }

        /// <summary>
        ///     Método para eliminar todos los términos de una lista de términos.
        /// </summary>
        /// <param name="idLista">Id de la lista de la que se desea eliminar todos los términos.</param>
        /// <returns>Respuesta de la petición.</returns>
        public IRestResponse EliminarTodosTerminos(int idLista)
        {
            var client = new RestClient($"https://ModeradorArticulos.cognitiveservices.azure.com/contentmoderator/lists/v1.0/termlists/{idLista}/terms?language=spa");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Ocp-Apim-Subscription-Key", "3db2c831f29248f883bf33d925347349");
            var response = client.Execute(request);
            Thread.Sleep(WAIT_TIME);
            RefreshSearchIndex(idLista);
            return response;
        }

        /// <summary>
        ///     Método para refrescar el índice de una lista que ha sido modificada.
        /// </summary>
        /// <param name="idLista">Id de la lista.</param>
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
