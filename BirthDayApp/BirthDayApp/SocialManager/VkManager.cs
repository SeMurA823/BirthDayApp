using BirthDayApp.SocialManager;
using BirthDayApp.SocialManager.items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Auth;

namespace SocialManager.VkManager
{
    public class VkManager
    {
        private static VkManager manager = new VkManager();
        public string AccessToken { get; set; }
        private const string START_URL = "https://api.vk.com/method/";
        private VkManager() { }
        public static VkManager GetManager()
        {
            return manager;
        }
        public void Auth(UIAuth ui, string appId, Action authDone)
        {
            var auth = new OAuth2Authenticator(
                clientId: appId,
                scope: "friends",
                authorizeUrl: new Uri("https://oauth.vk.com/authorize"),
                redirectUrl: new Uri("https://oauth.vk.com/blank.html")
                );
            auth.AllowCancel = true;
            auth.ShowErrors = false;
            auth.Completed += (obj, ee) =>
            {
                if (ee.IsAuthenticated)
                {
                    AccessToken = ee.Account.Properties["access_token"].ToString();
                    authDone();
                }
                else
                {
                    auth.OnCancelled();
                }
            };
            auth.Error += (obj, ee) => auth.OnCancelled();
            ui.StartAuth(auth);
        }
        public List<Friend> GetFriends()
        {
            if (AccessToken == null) return null;
            string urlRequest = START_URL + 
                "friends.get?order=hints&fields=photo_200,photo_50,id,first_name,last_name,bdate&access_token=" +
                AccessToken + 
                "&v=5.126";
            WebRequest web = WebRequest.Create(urlRequest);
            string responseStr;
            try
            {
                responseStr = new StreamReader(web.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch (WebException)
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
