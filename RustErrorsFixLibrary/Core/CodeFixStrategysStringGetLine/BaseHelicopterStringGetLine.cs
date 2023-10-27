using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class BaseHelicopterStringGetLine : CodeFixStrategyStringGetLine
    {
        public BaseHelicopterStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, "BaseHelicopter", "PatrolHelicopter");

            //Match match = Regex.Match(errorLineString, "([\\d\\w]+)\\.maxCratesToSpawn");

            //var field = match.Groups[1].ToString();

            //code = Regex.Replace(code, $"BaseHelicopter\\s{field}", $"PatrolHelicopter {field}");
            return code;
        }
    }
}
