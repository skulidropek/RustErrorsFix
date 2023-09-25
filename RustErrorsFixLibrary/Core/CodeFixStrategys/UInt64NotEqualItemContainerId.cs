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
    public class UInt64NotEqualItemContainerId : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            foreach(Match match in Regex.Matches(code, @".uid.Value\s*!=\s*([^\s)]+)"))
            {
                if(!Regex.IsMatch(code, @"ItemContainerId\s" + match.Groups[1].ToString()))
                    continue;

                var group0 = match.Groups[0].ToString();

                code = code.Replace(group0, group0.Replace(".Value", ""));
            }

            return ToSyntaxNode(code);
        }
    }
}
