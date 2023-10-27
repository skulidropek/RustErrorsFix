using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class ulongNetworkableIdParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public ulongNetworkableIdParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            SourceText sourceText = CodeFixManager.SyntaxTree.GetText();

            string lineText = sourceText.GetSubText(CompilationErrorModel.Location.SourceSpan).ToString();

            var field = Regex.Split(lineText, "==").First();

            return code.Replace(lineText, lineText.Replace(field, field + ".Value"));
        }
    }
}
