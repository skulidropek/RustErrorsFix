using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class UlongToItemContainerIdGetLine : CodeFixStrategyStringGetLine
    {
        public UlongToItemContainerIdGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString)
        {
            if (Regex.IsMatch(errorLineString, @"\.(ID|uid)\.Value"))
            {
                return code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.(ID|uid)\.Value", ".$1"));
            }

            var index = errorLine.Item2;
            try
            {
                while (true)
                {
                    if (errorLineString[index] == '(' ||
                        errorLineString[index] == ' ')
                        break;

                    index--;
                }

                var item2 = errorLineString.Substring(index + 1);

                if (errorLineString[index] == '(')
                {
                    for (int i = item2.Length - 1; i > 0; i--)
                    {
                        var item = item2[i];
                        item2 = item2.Remove(i);

                        if (item == ')')
                            break;
                    }
                }

                code = code.Replace(errorLineString, errorLineString.Replace(item2, "new ItemContainerId(" + item2.Replace(");", "") + ")"));
            }
            catch { }

            if (Regex.IsMatch(errorLineString, @"Convert.ToUInt64\(.+\)"))
                code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(Convert.ToUInt64\(.+\))", "new ItemContainerId($1)"));
            //else
            //    code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.uid\s=\s(.+);", ".uid = new ItemContainerId($1)"));
            
            return code;
        }
    }
}
