namespace RustErrorsFixLibrary.Core.OpenAI
{
    internal partial class OpenAIAPI
    {
        public class Usage
        {
            public object prompt_tokens { get; set; }
            public object completion_tokens { get; set; }
            public object total_tokens { get; set; }
        }
    }
}
