﻿using BirthDayApp.themes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirthDayApp.Items
{
    public class Configuration
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("theme")]
        public EThemes Theme { get; set; }
    }
}
