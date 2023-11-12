using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class UlongToNetworkabledParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public UlongToNetworkabledParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            SourceText sourceText = CodeFixManager.SyntaxTree.GetText();

            string lineText = sourceText.GetSubText(CompilationErrorModel.Location.SourceSpan).ToString();

            if (Regex.IsMatch(errorLineString, @"Convert.ToUInt64\(.+\)"))
                code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(Convert.ToUInt64\(.+\))", $"new {groupCollection[4].Value}($1)"));
            //else
            //    code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.uid\s=\s(.+);", ".uid = new NetworkableId($1)"));

            return code;
        }
    }
}
