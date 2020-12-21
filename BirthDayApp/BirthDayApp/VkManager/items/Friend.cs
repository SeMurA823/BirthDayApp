using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.VkManager.items
{
    public class Friend
    {
        [JsonProperty("first_name")] 
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("bdate")] 
        public string BDate { get; set; }

        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }

        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
    }
}
