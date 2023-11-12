using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class BaseProjectileMagazineTryReloadStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public BaseProjectileMagazineTryReloadStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            return code.Replace(errorLineString, errorLineString.Replace(".primaryMagazine.TryReload", ".ServerTryReload"));//Regex.Replace(errorLineString, field + @"\s*==\s*([\d\w])+(\s|\)|,)", $"{field} == new {type}($1)$2"));
        }
    }
}
