
using Newtonsoft.Json;

namespace RustErrorsFixLibrary.Core.OpenAI
{
    internal partial class OpenAIAPI
    {
        public class Message
        {
            [JsonProperty("role")]
            public string Role { get; set; } = "";
            [JsonProperty("content")]

            public string Content { get; set; } = "";
        }
    }
}
