using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class BasePlayerServerInputStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public BasePlayerServerInputStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            return code.Replace(errorLineString, Regex.Replace(errorLineString, @"(.+)\.serverInput\s=\s(.+);",
@"$1.serverInput.current = $2.current;
  $1.serverInput.previous = $2.previous;
 "));
        }
    }
}
