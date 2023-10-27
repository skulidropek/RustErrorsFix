using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine
{
    internal class UlongToNetworkabledParametrStringGetLine : CodeFixStrategyParametrStringGetLine
    {
        public UlongToNetworkabledParametrStringGetLine(CodeFixManager codeFixManager) : base(codeFixManager)
        {
        }

        public override string Fix(string code, (int, int) errorLine, string errorLineString, GroupCollection groupCollection)
        {
            int index = errorLine.Item2;

            if (Regex.IsMatch(errorLineString, @"\.(ID|uid)\.Value"))
            {
                return code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.(ID|uid)\.Value", ".$1"));
            }

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

                int brakets = 0;

                string field = "";

                for(int i = 0; i < item2.Length; i++)
               // foreach(char c in errorLineString.Remove(0, index))
                {
                    if (item2[i] == '(') 
                        brakets++;
                    else if (item2[i] == ')')
                    {
                        brakets--;
                        if (brakets == -1)
                        {
                            break;
                        }

                        if (brakets == 0)
                        {
                            if (item2[i + 1] == ',' || item2[i + 1] == ' ')
                                break;
                        }
                    }

                    field += item2[i];
                }

                Log.Debug.Log(groupCollection[groupCollection.Count - 1].Value);
                code = code.Replace(errorLineString, errorLineString.Replace(field, $"new {groupCollection[1].Value}(" + field + ")"));
            }
            catch { }

            if (Regex.IsMatch(errorLineString, @"Convert.ToUInt64\(.+\)"))
                code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"(Convert.ToUInt64\(.+\))", $"new {groupCollection[4].Value}($1)"));
            //else
            //    code = code.Replace(errorLineString, Regex.Replace(errorLineString, @"\.uid\s=\s(.+);", ".uid = new NetworkableId($1)"));

            return code;
        }
    }
}
