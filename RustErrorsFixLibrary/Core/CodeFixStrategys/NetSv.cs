using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    internal class NetSv : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, @"(if\s*\(\s*)?Net.sv.write.Start\(\)\s*\)?", "var nw = Net.sv.StartWrite();");
            code = Regex.Replace(code, @"Net.sv.write", "nw");

            string output = "";

            bool varHave = false;

            foreach (var text in CustomSplit(code, "var"))
            {
                if(text.Contains("nw = Net.sv.StartWrite();") && !varHave)
                {
                    output += "\nvar ";
                    varHave = true;
                }

                output += text + "\n";
            }

            return ToSyntaxNode(output);
        }
        static string[] CustomSplit(string word, string delimiter)
        {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
            int start = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (i + delimiter.Length <= word.Length && word.Substring(i, delimiter.Length) == delimiter)
                {
                    result.Add(word.Substring(start, i - start));
                    start = i + delimiter.Length;
                }
            }
            result.Add(word.Substring(start));
            return result.ToArray();
        }
    }
}
