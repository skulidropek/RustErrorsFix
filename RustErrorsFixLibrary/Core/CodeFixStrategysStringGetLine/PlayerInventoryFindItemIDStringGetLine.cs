using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class PlayerInventoryFindItemIDStringGetLine : CodeFixStrategyStringGetLine
    {
        public PlayerInventoryFindItemIDStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            code = code.Replace(errorLineString, errorLineString.Replace("FindItemID", "FindItemByItemID"));
            return code;
        }
    }
}
