using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DuboisAnke_Project.Model
{
    public class Quote
    {

       
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("author")]
            public string Author { get; set; }
        

        public class Root
        {
            public List<Quote> Results { get; set; }
        }
    }
}
