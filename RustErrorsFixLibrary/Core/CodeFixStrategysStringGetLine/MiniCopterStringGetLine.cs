using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class MiniCopterStringGetLine : CodeFixStrategyStringGetLine
    {
        public MiniCopterStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            if (errorLineString.Contains("VehicleEngineController<MiniCopter>"))
            {
                return code.Replace(errorLineString, errorLineString.Replace("MiniCopter", "PlayerHelicopter")); ;
            }


            if (errorLineString.Contains("MiniCopter.population"))
            {
                return code.Replace(errorLineString, errorLineString.Replace("MiniCopter.population", "Minicopter.population"));
            }

            return Regex.Replace(code, "(?<!\\.)MiniCopter", "Minicopter");
        }
    }
}
