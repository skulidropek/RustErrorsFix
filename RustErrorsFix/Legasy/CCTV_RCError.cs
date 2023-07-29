using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class CCTV_RCError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = Regex.Replace(plugin, @"(.controlBookmarks\.Add\(.+\.GetIdentifier\(\)), .+\.net\.ID\.Value\);", "$1);");

            return plugin;
        }
    }
}
