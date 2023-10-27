using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class MinicopterFieldStringGetLine : CodeFixStrategyStringGetLine
    {
        public MinicopterFieldStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, errorLineString.Replace("(Minicopter,", "(MiniCopter,"));

            return code;
        }
    }
}
