using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class UnexpectedTokenError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            while (Regex.IsMatch(plugin, "(\".*)\\{([^\\}^\\{^\\(^\\)]*\\?.*\\:.*)\\}(.*\")"))
                plugin = Regex.Replace(plugin, "(\".*)\\{([^\\}^\\{^\\(^\\)]*\\?.*\\:.*)\\}(.*\")", "$1 {($2)} $3");

            //var mathes = Regex.Matches(plugin, @"\{([^\{^\}]+)\}");

            //foreach(Match math in mathes)
            //{
            //    var group1ToString = math.Groups[1].ToString();

            //    if (Regex.IsMatch(group1ToString, @".+\s*\?s*.+:.+"))
            //    {
            //        plugin = plugin.Replace(group1ToString, "{(" + group1ToString + ")}");
            //    }
            //}

            return plugin;
        }
    }
}
