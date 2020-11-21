using risersoft.shared;
using risersoft.shared.cloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CognitiveServiceRsMx.Speech.Wordy
{
    public class OxfordApiClient : WebApiClientOutputBase
    {
        string _base = "https://od-api.oxforddictionaries.com/api/v2";
        string app_id= "da5819a4";
        string app_key= "29cd95bfb96aa5e7b46d50ba83f6c1d7";

        public OxfordApiClient()
        {
            {
                this.BuildHeaders = (HttpClient _client) =>
                {
                    _client.DefaultRequestHeaders.Add("app_id", app_id);
                    _client.DefaultRequestHeaders.Add("app_key", app_key);
                };
            }

        }

        public DictionaryResult GetLemma(string word)
        {
            var dic = new Dictionary<string, string>();
            dic["strictMatch"] = "false";
            this.PrepareQueryString(_base + "/lemmas/en/" + word, dic);
            var result = this.Get<DictionaryResult>();
            return result;
        }

        public DictionaryResult GetDictionaryEntry(string word)
        {
            this.PrepareQueryString(_base + "/entries/en/" + word, new Dictionary<string, string>());
            var result = this.Get<DictionaryResult>();
            return result;
        }

    }
}