using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class MinicopterWaterSampleStringGetLine : CodeFixStrategyStringGetLine
    {
        public MinicopterWaterSampleStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            return Regex.Replace(code, @"\.waterSample\.", ".engineController.waterloggedPoint.");
        }
    }
}