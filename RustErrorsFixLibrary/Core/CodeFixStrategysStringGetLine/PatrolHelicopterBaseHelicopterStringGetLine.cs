﻿using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class PatrolHelicopterBaseHelicopterStringGetLine : CodeFixStrategyStringGetLine
    {
        public PatrolHelicopterBaseHelicopterStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, "BaseHelicopter", "PatrolHelicopter");

            return code;
            //var match = Regex.Match(errorLineString, @"([\d\w_]+)\s*=");

            //var field = match.Groups[1].ToString();

            //return Regex.Replace(code, $"BaseHelicopter\\s({field})", "PatrolHelicopter $1");
        }
    }
}
