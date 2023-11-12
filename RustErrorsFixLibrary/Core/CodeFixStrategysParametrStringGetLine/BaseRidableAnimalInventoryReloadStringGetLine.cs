using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine
{
    internal class BaseRidableAnimalInventoryReloadStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public BaseRidableAnimalInventoryReloadStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            return code.Replace(errorLineString, errorLineString.Replace(".inventory", ".storageInventory"));//Regex.Replace(errorLineString, field + @"\s*==\s*([\d\w])+(\s|\)|,)", $"{field} == new {type}($1)$2"));
        }
    }
}
