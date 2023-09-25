using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslyn_test.Abstract;
using Roslyn_test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Factory
{
    internal class ListHashSetFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();
            code = Regex.Replace(code, @"(BasePlayer\.activePlayerList)(\.ForEach\(.+\))", "$1.ToList()$2");

            return code.ToSyntaxNode();
        }
    }
}
