using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class ReplaceUintStringGetLine : CodeFixStrategyStringGetLine
    {
        public ReplaceUintStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            if(Regex.IsMatch(errorLineString, @"(.+\s=)(\s.+)"))
                code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(.+\s=)(\s.+)", "$1(uint)$2"));
            else
                code = code.Replace(errorLineString, Regex.Replace(errorLineString, @".RemoveExact\(\(ulong\),", ".RemoveExact((uint)"));
            
            return code;
        }
    }
}
