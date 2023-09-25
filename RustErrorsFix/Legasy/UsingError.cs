using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class UsingError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = plugin.Replace("using Apex;", "");
            return plugin;
        }
    }
}
