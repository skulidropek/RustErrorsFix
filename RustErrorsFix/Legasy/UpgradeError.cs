using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class UpgradeError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = plugin
                .Replace(".GetGrade(", ".blockDefinition.GetGrade(")
                .Replace(".costToBuild", ".CostToBuild()");

            plugin = Regex.Replace(plugin, @"(\.CanAffordUpgrade.+,)", "$1 0,");
            plugin = Regex.Replace(plugin, @"(\.CanAffordUpgrade\([^,]+,)[0,\s]+", "$1 0,");


            if (Regex.IsMatch(plugin, @"(GetGrade.+)(\),.+)"))
            {
                plugin = Regex.Replace(plugin, @"(\.GetGrade.+)(\),.+)", "$1, 0 $2");
                plugin = Regex.Replace(plugin, @"(\.GetGrade.+)(\),.+)", "$1, 0 $2");
            }

            plugin = plugin.Replace(".blockDefinition.blockDefinition.GetGrade", ".blockDefinition.GetGrade");

            return plugin;
        }
    }
}
