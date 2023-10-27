using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class ItemContainerBoolParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public ItemContainerBoolParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(GiveItem\(.+,)(.+),([^)]+)\)", "$1$3,$2)"));
            return code;
        }
    }
}
