using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    internal class ScientistToScientistNPC : CodeFixStrategyStringGetLine
    {
        public ScientistToScientistNPC(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"Scientist(?!NPC)", "ScientistNPC"));
            return code;
        }
    }
}
