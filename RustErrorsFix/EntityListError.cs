using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal class EntityListError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = Regex.Replace(plugin, @"foreach\s+?\(KeyValuePair<(UInt32|UInt64|uint|ulong), BaseNetworkable>", "foreach (var ");

            return plugin;
        }
    }
}
