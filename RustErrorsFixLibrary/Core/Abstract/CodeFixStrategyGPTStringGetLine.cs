using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RustErrorsFixLibrary.Core.OpenAI;
using RustErrorsFixLibrary.Core.Model;

namespace RustErrorsFixLibrary.Core.Abstract
{
    internal abstract class CodeFixStrategyGPTStringGetLine : CodeFixStrategy
    {
        private readonly CodeFixManager _codeFixManager;
        private const string _apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
        protected readonly OpenAIAPI OpenAIAPI;

        public CodeFixStrategyGPTStringGetLine(CodeFixManager codeFixManager)
        {
            OpenAIAPI = new OpenAIAPI(_apiKey);
            _codeFixManager = codeFixManager;
        }

        public abstract string Fix(string code, (int, int) errorLine, string errorLineString, string error);

        public override SyntaxNode Fix(SyntaxNode model, (int, int) errorLine, CompilationErrorModel location)
        {
            string code = model.ToString();

            SourceText sourceText = _codeFixManager.SyntaxTree.GetText();
            string lineText = sourceText.Lines.GetLineFromPosition(location.Location.SourceSpan.Start).ToString();

            if (lineText.Contains(code))
                lineText = code;

            code = Fix(code, errorLine, lineText, location.Text);

            return ToSyntaxNode(code);
        }

        public override SyntaxNode Fix(SyntaxNode model)
        {
            throw new NotImplementedException();
        }
    }
}
