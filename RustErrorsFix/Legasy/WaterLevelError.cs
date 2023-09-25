using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class WaterLevelError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            var matches = Regex.Matches(plugin, @"(.+WaterLevel\.Test\(.+)\)");

            foreach (Match match in matches)
            {
                var group1 = match.Groups[1].ToString();

                if (group1.Contains("true") || group1.Contains("false"))
                    continue;

                group1 = SplitBrackets(group1);

                if (group1.Contains("if"))
                {
                    if (group1.Contains("())"))
                        group1 = group1.Replace("))", ")");
                    else
                        group1 = group1.Replace("))", "");
                }

                plugin = plugin.Replace(group1, group1 + ", true, true");
            }

            matches = Regex.Matches(plugin, @"(.+WaterLevel\.GetWaterDepth\([^>^<]+)\)");

            foreach (Match match in matches)
            {
                var group1 = match.Groups[1].ToString();

                group1 = SplitBrackets(group1);

                if (group1.Contains("if"))
                {
                    if (group1.Contains("())"))
                        group1 = group1.Replace("))", ")");
                    else
                        group1 = group1.Replace("))", "");
                }

                var newGroup1 = group1 + ", true, true";

                newGroup1 = newGroup1.Replace("null", "true");

                while (newGroup1.Contains(", true, true, true"))
                    newGroup1 = newGroup1.Replace(", true, true, true", ", true, true");

                plugin = plugin.Replace(group1, newGroup1);
            }

            matches = Regex.Matches(plugin, @"(.+WaterLevel\.Factor\([^>^<]+)\)");

            foreach (Match match in matches)
            {
                var group1 = match.Groups[1].ToString();

                group1 = SplitBrackets(group1);

                if (group1.Contains("if"))
                {
                    if (group1.Contains("())"))
                        group1 = group1.Replace("))", ")");
                    else
                        group1 = group1.Replace("))", "");
                }

                var newGroup1 = group1 + ", true, true";

                while (newGroup1.Contains(", true, true, true"))
                    newGroup1 = newGroup1.Replace(", true, true, true", ", true, true");

                plugin = plugin.Replace(group1, newGroup1);
            }


            return plugin;
        }

        public string SplitBrackets(string code)
        {
            int index = 0;

            string newCode = "";
            foreach (var chr in code)
            {
                newCode += chr;

                if (chr == '(')
                    index++;
                else if (chr == ')')
                {
                    index--;

                    if (index == 0)
                    {
                        return newCode;
                    }
                }
            }

            return code;
        }
    }
}
