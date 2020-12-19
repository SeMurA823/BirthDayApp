using BirthDayApp.VkManager.items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BirthDayApp.VkManager
{
    public class VkManager
    {
        public string access_token { private get; set; }
        private const string START_URL = "https://api.vk.com/method/";

        public List<Friend> GetFriends()
        {
            if (access_token == null) return null;
            string urlRequest = START_URL + 
                "friends.get?order=hints&fields=photo_200,id,first_name,last_name,bdate&access_token=" +
                access_token + 
                "&v=5.126";
            WebRequest web = WebRequest.Create(urlRequest);
            string responseStr;
            try
            {
                responseStr = new StreamReader(web.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch (WebException e)
            {
                return new List<Friend>();
            }
            
            Response response = JsonConvert.DeserializeObject<Response>(responseStr);
            return response?.ResponseItems?.Items;
        }
        private class Response
        {
            [JsonProperty("response")]
            public ResponseItems ResponseItems; 
        }
        private class ResponseItems
        {
            [JsonProperty("count")]
            public int Count;

            [JsonProperty("items")]
            public List<Friend> Items;
        }
    }
}
