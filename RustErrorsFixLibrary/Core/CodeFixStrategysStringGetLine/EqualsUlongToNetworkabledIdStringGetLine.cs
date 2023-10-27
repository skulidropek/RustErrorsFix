using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class EqualsUlongToNetworkabledIdStringGetLine : CodeFixStrategyStringGetLine
    {
        public EqualsUlongToNetworkabledIdStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(\(\s*[.\w\d]+\s*(!=|=|<|>|>=|<=)\s*.+\.net\.ID)(\s*\))", "$1.Value$3"));
            code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(\(\s*.+\.net\.ID)(\s*(!=|=|<|>|>=|<=)\s*.+\s*\))", "$1.Value$3"));

            return code;
        }
    }
}
