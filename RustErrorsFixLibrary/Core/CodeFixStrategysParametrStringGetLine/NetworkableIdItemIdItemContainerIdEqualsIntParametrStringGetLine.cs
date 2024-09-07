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
    internal class NetworkableIdItemIdItemContainerIdEqualsIntParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public NetworkableIdItemIdItemContainerIdEqualsIntParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            SourceText sourceText = CodeFixManager.SyntaxTree.GetText();

            string lineText = sourceText.GetSubText(CompilationErrorModel.Location.SourceSpan).ToString();

            if (lineText.Contains("svActiveItemID"))
            {
                return code.Replace(lineText, lineText.Replace("svActiveItemID", "svActiveItemID.Value"));
            }

            return code.Replace(lineText, lineText + ".Value");//Regex.Replace(errorLineString, field + @"\s*==\s*([\d\w])+(\s|\)|,)", $"{field} == new {type}($1)$2"));
        }
    }
}
