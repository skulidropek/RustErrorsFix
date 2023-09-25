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
    public class GetTargetCount : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node) 
        {
            string code = node.ToFullString();

            code = Regex.Replace(code, @"[^\s,]+\.GetTargetCount\((.+),(.+)\)", "$1.GetTargetCount($2)");

            return ToSyntaxNode(code);
        }
    }
}
