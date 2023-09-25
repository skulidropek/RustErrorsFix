using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class UInt64ToNetworkabledIdFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            List<(string, string)> tuples = new List<(string, string)>()
            {
                (@".serverEntities.Find", "NetworkableId"),
                (@"FindContainer", "ItemContainerId"),
                (@"UpdateActiveItem", "ItemId"),
                (@"FindItemByUID", "ItemId"),
                (@".inventory.FindItemUID", "ItemId"),
            };
            
            List<(string, string)> tuplesUid = new List<(string, string)>()
            {
                (@".inventory.uid", "ItemContainerId"),
            };


            foreach (Match match in Regex.Matches(code, @"FileStorage.server.RemoveEntityNum\(((?!new)[^,)]+)"))
            {
                var group1 = match.Groups[1].ToString();

                if (Regex.IsMatch(code, "NetworkableId" + @"\s+" + group1))
                {
                    continue;
                }

                var group0 = match.Groups[0].ToString();

                code = code.Replace(group0,
                    group0.Contains(".Value") ?
                        group0.Replace(".Value", "") :
                        $"FileStorage.server.RemoveEntityNum(new NetworkableId({group1})");
            }

            foreach (var tuble in tuples)
            {
                foreach(Match match in Regex.Matches(code, tuble.Item1 + @"\(((?!new).+)\)"))
                {
                    var group1 = match.Groups[1].ToString();

                    if(Regex.IsMatch(code, tuble.Item2 + @"\s+" + group1))
                    {
                        continue;
                    }

                    var group0 = match.Groups[0].ToString();

                    code = code.Replace(group0, 
                        group0.Contains(".Value") ?
                            group0.Replace(".Value", "") : 
                            $"{tuble.Item1}(new {tuble.Item2}({group1}))");
                } 
            }

            foreach (var tuble in tuplesUid)
            {
                foreach (Match match in Regex.Matches(code, tuble.Item1 + @".Value\s*==\s*([^\s)]+)"))
                {
                    var group1 = match.Groups[1].ToString();

                    if (!Regex.IsMatch(code, tuble.Item2 + @"\s+" + group1))
                    {
                        continue;
                    }

                    var group0 = match.Groups[0].ToString();

                    code = code.Replace(group0, group0.Replace(".Value", ""));
                }
            }

            if(Regex.IsMatch(code, @"(new Item[\s\n\w{}=\d,.]*)uid\s*=\s*([^,\s]+)"))
                code = Regex.Replace(code, @"(new Item[\s\n\w{}=\d,.]*)uid\s*=\s*([^,\s]+)", "$1uid = new ItemId($2)");

            return ToSyntaxNode(code);
        }
    }
}
