using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix
{
    internal class NetValueError : IErrorFixer
    {
        Dictionary<string, string> _methodNames = new Dictionary<string, string>()
        {
            { "FindContainer", "ItemContainerId" },
            { "serverEntities.Find", "NetworkableId" },
            { "UpdateActiveItem", "ItemId" },
            //{ ".FindItemUID(ulong.Parse", "ItemId" },
            { "FindItemUID", "ItemId" },
        };


        //List<Method> methods = new List<Method>()
        //{
        //    new Method("FindContainer", "ItemContainerId"),
        //    new Method("Find", "NetworkableId"),
        //    new Method("UpdateActiveItem", "ItemId"),
        //    new Method("FindItemUID","FindItemUID", "ItemContainerId"),

        //};

        public string Fix(string plugin)
        {
            plugin = plugin
                .Replace("uint", "ulong")
                .Replace(".net.ID", ".net.ID.Value")
                .Replace(".net.ID.Value.Value", ".net.ID.Value")
                .Replace("UInt32", "UInt64")
                .Replace("FileStorage.server.RemoveExact(UInt64.", "FileStorage.server.RemoveExact(UInt32.")
                .Replace("FileStorage.server.RemoveExact(ulong.", "FileStorage.server.RemoveExact(uint.")
                .Replace("CommunityEntity.ServerInstance.net.ID.Value", "CommunityEntity.ServerInstance.net.ID")
                ;

            foreach (var methodName in _methodNames)
            {
                plugin = Regex.Replace(plugin, $@".{methodName.Key}\(([^\)^\(]+)\)", $".{methodName.Key}(new {methodName.Value}($1))");
            }

            while (Regex.IsMatch(plugin, @"FileStorage\.server\.Store\(.+\.net\.ID\.Value.*?\)"))
            {
                var group0 = Regex.Match(plugin, @"FileStorage\.server\.Store\(.+\.net\.ID\.Value.*?\)").Groups[0].ToString(); //, "FileStorage.server.Store($1)"
                plugin = plugin.Replace(group0, group0.Replace(".net.ID.Value", ".net.ID"));
            }

            while (Regex.IsMatch(plugin, @"FileStorage\.server\.RemoveExact\(.+\.net\.ID\.Value.*?\)"))
            {
                var group0 = Regex.Match(plugin, @"FileStorage\.server\.RemoveExact\(.+\.net\.ID\.Value.*?\)").Groups[0].ToString(); //, "FileStorage.server.Store($1)"
                plugin = plugin.Replace(group0, group0.Replace(".net.ID.Value", ".net.ID"));
            }


            plugin = Regex.Replace(plugin, @"(UInt64|ulong|uint|UInt32)(\s.+\s*?=\s*?FileStorage.server.Store)", "var $2");

            plugin = Regex.Replace(plugin, @".uid\s*?==\s*?([\w\d]+)\s*?\)", ".uid == new ItemId($1))");

            plugin = Regex.Replace(plugin, @"\((ulong|UInt64)\)(\s*?DateTime.UtcNow.Ticks;\s*?\n*?\s*?.+\.Shuffle\()", "(uint)$2");

            plugin = Regex.Replace(plugin, ".+\\.(panelName|bp\\.panelNane) = \"generic_resizable\";", "");
            return plugin;
        }

        class Method
        {
            public string MethodNameRegex;
            public string MethodName;
            public string ValueName;

            public Method(string methodName, string valueName)
            {
                MethodName = methodName;
                ValueName = valueName;
            }
            public Method(string methodNameRegex, string methodName, string valueName)
            {
                MethodNameRegex = methodNameRegex;
                MethodName = methodName;
                ValueName = valueName;
            }
        }
    }
}
