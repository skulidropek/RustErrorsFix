using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class ItemContainerBaseEntityStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public ItemContainerBaseEntityStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.TakeFrom\(([^.]+)\.inventory", ".TakeFrom($1, $1.inventory"));
            return code;
        }
    }
}
