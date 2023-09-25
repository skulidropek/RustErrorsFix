using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class NetWrite : IErrorFixer
    {
        public string Fix(string plugin)
        {
            if (!plugin.Contains("Net.sv.write")) return plugin;

            plugin = plugin
                .Replace("Network.Net.sv.write", "Net.sv.write")
                .Replace("Net.sv.write", "Netsvwrite")
                ;

            if (plugin.Contains(@"Netsvwrite.Start"))
                plugin = Regex.Replace(plugin, @"(.+)Netsvwrite.Start\(\)", "$1 Netsvwrite.Start(Net.sv)");

            plugin = Regex.Replace(plugin, @"(Netsvwrite\.EntityID\(.+\.net.ID).Value", "$1");

            plugin = plugin.Replace("Netsvwrite", "Facepunch.Pool.Get<Network.NetWrite>()");

            return plugin;
        }
    }
}
