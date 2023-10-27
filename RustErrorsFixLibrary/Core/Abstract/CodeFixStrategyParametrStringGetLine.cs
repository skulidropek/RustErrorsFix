using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    internal abstract class CodeFixStrategyParametrStringGetLine : CodeFixStrategy
    {
        protected readonly CodeFixManager CodeFixManager;

        protected CompilationErrorModel CompilationErrorModel;

        public CodeFixStrategyParametrStringGetLine(CodeFixManager codeFixManager)
        {
            CodeFixManager = codeFixManager;
        }

        public abstract string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection);

        public override SyntaxNode Fix(SyntaxNode model, (int, int) errorLine, CompilationErrorModel error)
        {
            CompilationErrorModel = error;
            string code = model.ToString();

            SourceText sourceText = CodeFixManager.SyntaxTree.GetText();
            string lineText = sourceText.Lines.GetLineFromPosition(error.Location.SourceSpan.Start).ToString();

            if (lineText.Contains(code))
                lineText = code;

            CompilationErrorModel = error;

            int spaceCount = 0;
            for (int i = 0; i < lineText.Length - 1; i++)
            {
                if (lineText[i] != ' ')
                    break;

                spaceCount++;
            }

            errorLine.Item2 -= spaceCount;

            lineText = Regex.Replace(lineText, @"\}.*\s([\d\w]+\s[\d\w]+\(.+\))", "$1");

            lineText = Regex.Replace(lineText, "^\\s*", "");

            code = Fix(code, errorLine, lineText, error.Parametrs);

            return ToSyntaxNode(code);
        }

        public override SyntaxNode Fix(SyntaxNode model)
        {
            throw new NotImplementedException();
        }
    }
}
