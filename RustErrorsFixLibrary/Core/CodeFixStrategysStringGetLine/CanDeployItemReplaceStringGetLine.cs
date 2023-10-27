using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class CanDeployItemReplaceStringGetLine : CodeFixStrategyStringGetLine
    {
        public CanDeployItemReplaceStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = Regex.Replace(code, @"(CanDeployItem\(BasePlayer\s[\d\w]+,Deployer\s[\d\w]+,)uint(\s[\d\w]+\))", "$1NetworkableId$2)");
            return code;
        }
    }
}
