using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3.TestScript
{
    internal class UserData
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("completed")]
        public string? Completed { get; set; }
    }
}
