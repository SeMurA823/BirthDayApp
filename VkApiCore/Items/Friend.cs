using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkApiCore.Items
{
    public class Friend
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("first_name")]
        public string FirstName;
        [JsonProperty("last_name")]
        public string LastName;
        [JsonProperty("bdate")]
        public string BDate;
        [JsonProperty("photo_200")]
        public string PhotoURL200;
        [JsonProperty("sex")]
        public int Sex;
        [JsonProperty("screen_name")]
        public string ScreenName;
    }
}
