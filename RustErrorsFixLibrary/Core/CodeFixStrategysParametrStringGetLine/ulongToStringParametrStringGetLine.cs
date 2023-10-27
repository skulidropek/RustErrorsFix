using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class ulongToStringParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public ulongToStringParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            SourceText sourceText = CompilationErrorModel.Location.SourceTree.GetText();

            string lineText = sourceText.GetSubText(CompilationErrorModel.Location.SourceSpan).ToString();

           return code.Replace(errorLineString, errorLineString.Replace(lineText, lineText + ".ToString()"));
        }
    }
}
