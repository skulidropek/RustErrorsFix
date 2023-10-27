using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class ValueIntAddStringGetLine : CodeFixStrategyStringGetLine
    {
        public ValueIntAddStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"=(\s.+)\?\?(.+);", "=$1.Value ?? $2;"));
            return code;
        }
    }
}
