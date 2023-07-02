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
            { "FindItemByUID", "ItemId" },
        };


        private List<string> _fileStorageServer = new List<string>()
        {
            "Get",
            "Store",
            "RemoveExact",
            "Remove"
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
                .Replace(".uid", ".uid.Value")
                .Replace(".uid.Value.Value", ".uid.Value")
                .Replace("uid = Net.sv.TakeUID()", "uid = new ItemId(Net.sv.TakeUID())")
                ;

            var pluginOneLine = plugin.Replace("\n", " ");

            foreach (var methodName in _methodNames)
            {
               // while (Regex.IsMatch(plugin, $@".{methodName.Key}\(([^\)^\(]+)\)"))
                {
                    var matches = Regex.Matches(plugin, $@".{methodName.Key}\(([^\)^\(]+)\)");

                    foreach(Match match in matches)
                    {
                        var field = match.Groups[1].ToString();

                        if (!Regex.IsMatch(pluginOneLine, @$"{methodName.Value}\s{field}.*\.serverEntities\.Find\({field}\)"))
                        {
                            plugin = plugin.Replace(match.Groups[0].ToString(), Regex.Replace(match.Groups[0].ToString(), $@".{methodName.Key}\(([^\)^\(]+)\)", $".{methodName.Key}(new {methodName.Value}($1))"));
                        }
                    }
                }

                //plugin = Regex.Replace(plugin, $@".{methodName.Key}\(([^\)^\(]+)\)", $".{methodName.Key}(new {methodName.Value}($1))");
            }

            foreach(var name in _fileStorageServer)
            {
                //plugin = Regex.Replace(plugin, @"(FileStorage\.server\.Remove\(.+,)(.+\.Sign.NetId*?)\)", "$1 new NetworkableId($2));");

                //Regex.Replace(plugin, $@"FileStorage\.server\.{name}\((.+\.Sign.NetId*?)\)", $"FileStorage.server.{name}$1");

                while (Regex.IsMatch(plugin, $@"FileStorage\.server\.{name}\(.+\.net\.ID\.Value.*?\)"))
                {
                    var group0 = Regex.Match(plugin, $@"FileStorage\.server\.{name}\(.+\.net\.ID\.Value.*?\)").Groups[0].ToString(); //, "FileStorage.server.Store($1)"
                    plugin = plugin.Replace(group0, group0.Replace(".net.ID.Value", ".net.ID"));
                }

                plugin = Regex.Replace(plugin, $@"FileStorage\.server\.{name}\((.+\.Sign\.TextureId\(\)),", $"FileStorage.server.{name}((uint)$1,");
                plugin = Regex.Replace(plugin, $@"(FileStorage\.server\.{name}\(.+,)(.+\.Sign.NetId*?)\)", "$1 new NetworkableId($2))");
            }

            plugin = Regex.Replace(plugin, @"\.instanceData\.subEntity\s*?==\s*?([^\s.]+)", ".instanceData.subEntity == new NetworkableId($1)");
            plugin = Regex.Replace(plugin, @"\.instanceData\.subEntity\s*?=\s*?([^\s^;.]+)", ".instanceData.subEntity = new NetworkableId($1)");
            plugin = Regex.Replace(plugin, @"(\.instanceData\??.subEntity)(\s*\?\?\s*[^\s^,.]+)", "$1.Value $2");
            plugin = Regex.Replace(plugin, @"(\.instanceData\.subEntity)(\s*\!\=\s*[^\s^,.]+)", "$1.Value $2");


            plugin = Regex.Replace(plugin, @"(new Network\.Visibility\.Group\(null,\s*)(networkGroupId\);)", "$1 (uint)$2");

            plugin = Regex.Replace(plugin, @"(UInt64|ulong|uint|UInt32)(\s.+\s*?=\s*?FileStorage.server.Store)", "var $2");

            // plugin = Regex.Replace(plugin, @".uid\s*?==\s*?([\w\d]+)\s*?\)", ".uid.Value == $1)");

            plugin = Regex.Replace(plugin, @"(\.textureIDs\[.+\]\s*?=\s*?)", "$1 (uint)");
            plugin = Regex.Replace(plugin, @"(\.SetAudioId\()", "$1 (uint)");
            plugin = Regex.Replace(plugin, @"(\._overlayTextureCrc\s*?=\s*?)", "$1 (uint)");

            plugin = Regex.Replace(plugin, @"\((ulong|UInt64)\)(\s*?DateTime.UtcNow.Ticks;\s*?\n*?\s*?.+\.Shuffle\()", "(uint)$2");

            if(plugin.Contains(".panelName"))
                plugin = Regex.Replace(plugin, ".+\\.panelName = \"generic_resizable\";", "");

            plugin = Regex.Replace(plugin, @"(PrefabAttribute\.server\.Find<.+>\()", "$1 (uint)");
            plugin = Regex.Replace(plugin, @"(\.SetHeldItem\(.+)(\);)", "$1.Value$2");

            plugin = Regex.Replace(plugin, @"\.uid\.Value\s*=\s*([^=]?\s*[^;^\s^=.]+)", ".uid = new NetworkableId($1)");

            plugin = Regex.Replace(plugin, @"new NetworkableId\((.+\.instanceData\.subEntity)\)", "$1");

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
