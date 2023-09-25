using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RustErrorsFixLibrary.Core.Abstract;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class WaterLevelNullToBool : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node) 
        {
            string code = node.ToFullString();

            var matches = Regex.Matches(code, @"WaterLevel\..+");

            foreach(Match match in matches) 
            {
                var group0 = match.Groups[0].ToString();

                if(Regex.IsMatch(group0, @"\s*(false|true)\s*,\s*null\s*,\s*(false|true)"))
                {
                    code = code.Replace(group0, Regex.Replace(group0, @"(false|true),\snull", "$1"));
                }
                else
                {
                    code = code.Replace(group0, Regex.Replace(group0, @"(false|true),\snull", "$1, $1"));
                }
            }

            return ToSyntaxNode(code);
        }
    }
}
