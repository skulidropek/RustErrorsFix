using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    internal abstract class CodeFixStrategyStringGetLine : CodeFixStrategy
    {
        private readonly CodeFixManager _codeFixManager;
        public CodeFixStrategyStringGetLine(CodeFixManager codeFixManager)
        {
            _codeFixManager = codeFixManager;
        }

        public abstract string Fix(string code, (int, int) errorLine, string errorLineString);

        public override SyntaxNode Fix(SyntaxNode model, (int, int) errorLine, CompilationErrorModel error)
        {
            string code = model.ToString();

            SourceText sourceText = _codeFixManager.SyntaxTree.GetText();
            string lineText = sourceText.Lines.GetLineFromPosition(error.Location.SourceSpan.Start).ToString();

            //while (lineText.Contains("  "))
            //    lineText = lineText.Replace("  ", " ");

            if (lineText.Contains(code))
                lineText = code;

            int spaceCount = 0;
            for(int i = 0; i < lineText.Length - 1; i++)
            {
                if (lineText[i] != ' ')
                    break;

                spaceCount++;
            }

            errorLine.Item2 -= spaceCount;

            lineText = Regex.Replace(lineText, @"\}.*\s([\d\w]+\s[\d\w]+\(.+\))", "$1");

            lineText = Regex.Replace(lineText, "^\\s*", "");

            code = Fix(code, errorLine, lineText);

            return ToSyntaxNode(code);
        }

        public override SyntaxNode Fix(SyntaxNode model)
        {
            throw new NotImplementedException();
        }
    }
}
