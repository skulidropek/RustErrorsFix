using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class OnActiveItemChangeReplaceStringGetLine : CodeFixStrategyStringGetLine
    {
        public OnActiveItemChangeReplaceStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, @"(OnActiveItemChange\(\s*BasePlayer\s[\d\w]+,\s*Item\s[\d\w]+,\s*)uint(\s[\d\w]+\))", "$1ItemId$2");
            return code;
        }
    }
}
