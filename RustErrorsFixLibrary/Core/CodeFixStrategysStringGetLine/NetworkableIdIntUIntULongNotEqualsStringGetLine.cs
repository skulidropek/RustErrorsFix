using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class NetworkableIdIntUIntULongNotEqualsStringGetLine : CodeFixStrategyStringGetLine
    {
        public NetworkableIdIntUIntULongNotEqualsStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            return code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.(subEntity|net\.ID|uid)", ".$1.Value"));
        }
    }
}
