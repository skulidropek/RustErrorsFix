using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal class UpgradeError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = plugin
                .Replace(".GetGrade(", ".blockDefinition.GetGrade(")
                .Replace(".costToBuild", ".CostToBuild()");

            plugin = Regex.Replace(plugin, @"(\.CanAffordUpgrade.+,)", "$1 0,");

            if (Regex.IsMatch(plugin, @"(GetGrade.+)(\),.+)"))
                plugin = Regex.Replace(plugin, @"(\.GetGrade.+)(\),.+)", "$1, 0 $2");

            return plugin;
        }
    }
}
