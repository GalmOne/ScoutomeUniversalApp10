using Newtonsoft.Json;
using Scoutome.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Scoutome.DAO
{
    public class DataAccessObject
    {
        private HttpClient client;
        private const string urlApi = "http://scoutome.azurewebsites.net/api/";


        public DataAccessObject()
        {
            client = new HttpClient();
        }
       
        public async Task<ObservableCollection<Anime>> GetChildrenList(string type)
        {
            try
            {                
                HttpResponseMessage response = await client.GetAsync(urlApi + type);
                string json = await response.Content.ReadAsStringAsync();
                var animes = Newtonsoft.Json.JsonConvert.DeserializeObject<Anime[]>(json);

                if (response.IsSuccessStatusCode)
                {
                    return CastListToObservableAnime(animes);
                }
                else
                {
                    return null;
                }
            }
            catch(HttpRequestException e)
            {
                return null;
            }                     
        }

        private static ObservableCollection<Anime> CastListToObservableAnime(Anime[] animes)
        {
            List<Anime> li = new List<Anime>();
            li = animes.ToList<Anime>();
            ObservableCollection<Anime> myAnimeListView = new ObservableCollection<Anime>();

            for (int i = 0; i < li.Count(); i++)
            {
                myAnimeListView.Add(li[i]);
            }
            return myAnimeListView;
        }

        public async Task<bool> AddReunion(Reunion reunion, ObservableCollection<Anime> selectedAnime)
        {
            try
            {
                string json = JsonConvert.SerializeObject(reunion);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlApi + "reunions", content);
                if (response.IsSuccessStatusCode)
                {
                    return await AddPresences(reunion, selectedAnime);
                }
                else
                {
                    return false;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
                     
        }

        private async Task<bool> AddPresences(Reunion reunion, ObservableCollection<Anime> selectedAnime)
        {
            //    Pour ajouter les présences  
            for (int i = 0; i < selectedAnime.Count(); i++)
            {
                Presences pre = new Presences();
                pre.codeReunion = reunion.codeReunion;
                pre.useless = 1;
                pre.codeAnime = selectedAnime[i].codeAnime;

                string jsonPresence = JsonConvert.SerializeObject(pre);
                HttpContent contentPresence = new StringContent(jsonPresence, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlApi+"presences", contentPresence);
                if (response.IsSuccessStatusCode)
                {
                    
                }
            }
            return true;
        }

        public async Task<ObservableCollection<Reunion>> CallWebApiReunionList(string type)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(urlApi + type);
                string json = await response.Content.ReadAsStringAsync();
                var reunions = Newtonsoft.Json.JsonConvert.DeserializeObject<Reunion[]>(json);
                if (response.IsSuccessStatusCode)
                {
                    return CastListToObservableReunion(reunions);
                }
                else
                    return null;
            }
            catch(HttpRequestException)
            {
                return null;
            }            
        }

        private static ObservableCollection<Reunion> CastListToObservableReunion(Reunion[] reunions)
        {
            List<Reunion> li = new List<Reunion>();
            li = reunions.ToList<Reunion>();
            ObservableCollection<Reunion> myReunionList = new ObservableCollection<Reunion>();


            for (int i = 0; i < li.Count(); i++)
            {
                myReunionList.Add(li[i]);
            }
            return myReunionList;
        }       
    }
}
