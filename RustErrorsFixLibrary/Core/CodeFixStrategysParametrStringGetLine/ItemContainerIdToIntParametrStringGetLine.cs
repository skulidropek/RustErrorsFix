using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class ItemContainerIdToIntParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public ItemContainerIdToIntParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"if\s*\((\((int|Int32)\))?(.+)==\s*([\d\w])\)", @"if($3.Value==$4)"));

            return code;
        }
    }
}
