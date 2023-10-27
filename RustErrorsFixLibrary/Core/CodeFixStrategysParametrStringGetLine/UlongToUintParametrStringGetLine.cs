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
    internal class UlongToUintParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public UlongToUintParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            int index = errorLine.Item2-1;

            SourceText sourceText = CodeFixManager.SyntaxTree.GetText();

            string lineText = sourceText.GetSubText(CompilationErrorModel.Location.SourceSpan).ToString();

            string newField;

            if (lineText.Contains("ulong") || lineText.Contains("UInt64"))
                newField = lineText.Replace("ulong", "uint").Replace("UInt64", "uint");
            else
                newField = $"(uint)" + lineText;

            code = code.Replace(errorLineString, errorLineString.Replace(lineText, newField));

            return code;
        }
    }
}
