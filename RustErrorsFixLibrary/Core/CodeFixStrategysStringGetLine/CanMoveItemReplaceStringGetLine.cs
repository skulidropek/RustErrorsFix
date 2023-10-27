using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class CanMoveItemReplaceStringGetLine : CodeFixStrategyStringGetLine
    {
        public CanMoveItemReplaceStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, @"(CanMoveItem\(\s*Item\s[\d\w]+,\s*PlayerInventory\s[\d\w]+,\s*)uint(\s[\d\w]+,\s*int\s[\d\w]+,\s*int\s[\d\w]+\))", "ItemContainerId$2)");
            return code;
        }
    }
}
