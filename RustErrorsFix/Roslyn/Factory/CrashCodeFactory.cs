using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Roslyn_test.Abstract;

namespace Roslyn_test.Factory
{
    internal class CrashCodeFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            var method = node as MethodDeclarationSyntax;

            if (method.ReturnType.ToString() == "void") return node;

            if (method.Body.ToString().Contains("return")) return node;

            return null;// syntaxTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().First();
        }
    }
}
