using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class ItemContainerError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = Regex.Replace(plugin, ".+.onlyAllowedItem.+", "");

            return plugin;
        }
    }
}
