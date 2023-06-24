using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal class NetWrite : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = plugin
                .Replace("Network.Net.sv.write", "Net.sv.write")
                .Replace("Net.sv.write", "netWrite")
                ;

            plugin = Regex.Replace(plugin, @"(.+)netWrite.Start\(\)", "NetWrite netWrite = Facepunch.Pool.Get<NetWrite>();\n $1 netWrite.Start(Net.sv)");

            return plugin;
        }
    }
}
