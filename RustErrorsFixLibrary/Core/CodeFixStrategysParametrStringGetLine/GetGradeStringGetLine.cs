using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class GetGradeStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public GetGradeStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            return code.Replace(errorLineString, Regex.Replace(errorLineString, @"\s*,\s*0,\s*0\)", ", 0)"));//Regex.Replace(errorLineString, field + @"\s*==\s*([\d\w])+(\s|\)|,)", $"{field} == new {type}($1)$2"));
        }
    }
}