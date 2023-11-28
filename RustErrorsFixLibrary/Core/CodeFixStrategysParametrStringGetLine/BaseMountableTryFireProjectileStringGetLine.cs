using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class BaseMountableTryFireProjectileStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public BaseMountableTryFireProjectileStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            return code.Replace(errorLineString, Regex.Replace(errorLineString, @"BaseMountable\.", @"new BaseMountable()."));
        }
    }
}
