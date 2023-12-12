using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_Nunit
{
    public class UserData
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userid")]
        public string? UserId { get; set; }
        [JsonProperty("title")]
        public string? Title{ get; set; }
        [JsonProperty("completed")]
        public bool IsCompleted { get; set; }

    }
  
}
