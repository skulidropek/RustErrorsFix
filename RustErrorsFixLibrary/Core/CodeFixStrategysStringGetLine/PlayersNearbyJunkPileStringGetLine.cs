using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class PlayersNearbyJunkPileStringGetLine : CodeFixStrategyStringGetLine
    {
        public PlayersNearbyJunkPileStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            if (errorLineString.Contains("PlayersNearby"))
                code = code.Replace(errorLineString, "");
            else
                code = Regex.Replace(code, ".+PlayersNearby.+", "");
            return code;

        }
    }
}
