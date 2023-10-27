using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class OnItemCraftCancelledReplaceStringGetLine : CodeFixStrategyStringGetLine
    {
        public OnItemCraftCancelledReplaceStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, @"(OnItemCraftCancelled\(\s*ItemCraftTask\s[\d\w]+)\)", "$1, ItemCrafter itemCrafterOwner)");
            return code;
        }
    }
}
