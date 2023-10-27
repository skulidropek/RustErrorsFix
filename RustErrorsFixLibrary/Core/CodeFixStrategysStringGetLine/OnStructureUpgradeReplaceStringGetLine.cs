using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class OnStructureUpgradeReplaceStringGetLine : CodeFixStrategyStringGetLine
    {
        public OnStructureUpgradeReplaceStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, @"(OnStructureUpgrade\()\s*BaseCombatEntity\s[\d\w]+,(\s*BasePlayer\s[\d\w]+,\s*BuildingGrade.Enum\s[\d\w]+)\)", "$1$2, ulong skinId)");
            return code;
        }
    }
}
