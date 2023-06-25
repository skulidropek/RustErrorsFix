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

            if(plugin.Contains(@"netWrite.Start"))
                plugin = Regex.Replace(plugin, @"(.+)netWrite.Start\(\)", "$1 netWrite.Start(Net.sv)");
            plugin = Regex.Replace(plugin, @"(netWrite\.EntityID\(.+\.net.ID).Value", "$1");

            plugin = plugin.Replace("netWrite", "Facepunch.Pool.Get<NetWrite>()");

            return plugin;
        }
    }
}
