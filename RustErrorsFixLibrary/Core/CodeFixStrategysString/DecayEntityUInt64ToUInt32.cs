using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysString
{
    internal class DecayEntityUInt64ToUInt32 : CodeFixStrategyString
    {
        public override string Fix(string code)
        {
            code = Regex.Replace(code, @"\.AttachToBuilding\(((?!\(uint\)).+)\)", ".AttachToBuilding((uint)$1)");
            return code;
        }
    }
}
