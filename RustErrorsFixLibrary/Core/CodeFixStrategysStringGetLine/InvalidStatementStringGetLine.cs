﻿using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class InvalidStatementStringGetLine : CodeFixStrategyStringGetLine
    {
        public InvalidStatementStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"\{([^{}]+\?.+:[^{}]+)\}", "{($1)}"));
            return code;
        }
    }
}
