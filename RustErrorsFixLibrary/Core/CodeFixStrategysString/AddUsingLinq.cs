using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysString
{
    internal class AddUsingLinq : CodeFixStrategyString
    {
        public override string Fix(string text)
        {
            return "using System.Linq;\n" + text;
        }
    }
}
