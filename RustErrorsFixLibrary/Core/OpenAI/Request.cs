using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RustErrorsFixLibrary.Core.OpenAI
{
    internal partial class OpenAIAPI
    {
        public class Request
        {
            [JsonProperty("model")]
            public string ModelId { get; set; } = "";
            [JsonProperty("messages")]
            public List<Message> Messages { get; set; } = new List<Message>();
        }
    }
}
