using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class PlayerInventoryFindItemUIDStringGetLine : CodeFixStrategyStringGetLine
    {
        public PlayerInventoryFindItemUIDStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, errorLineString.Replace("FindItemUID", "FindItemByUID"));
            return code;

        }
    }
}
