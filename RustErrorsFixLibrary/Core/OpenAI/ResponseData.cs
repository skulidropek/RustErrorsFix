using System.Collections.Generic;

namespace RustErrorsFixLibrary.Core.OpenAI
{
    internal partial class OpenAIAPI
    {
        public class ResponseData
        {
            public string id { get; set; }
            public string @object { get; set; }
            public int created { get; set; }
            public string model { get; set; }
            public List<Choice> choices { get; set; }
            public Usage usage { get; set; }
        }
    }
}
