using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class Configuration
    {
        [JsonProperty("token")]
        string Token;
        string AppId = "7629034";
    }
}
