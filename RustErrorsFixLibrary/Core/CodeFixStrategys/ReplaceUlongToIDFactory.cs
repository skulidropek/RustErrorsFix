using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RustErrorsFixLibrary.Core.Abstract;
using System.Text.RegularExpressions;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class ReplaceUlongToIDFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            code = Regex.Replace(code, @"\.instanceData\.subEntity\s=\s((?!new)[^;]+)", ".instanceData.subEntity = new NetworkableId($1)");

            return ToSyntaxNode(code);
        }
    }
}
