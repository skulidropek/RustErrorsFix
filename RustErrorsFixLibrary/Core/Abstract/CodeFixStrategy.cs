using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    public abstract class CodeFixStrategy
    {
        public static SyntaxNode ToSyntaxNode(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            var getRoot = syntaxTree.GetRoot().DescendantNodes().FirstOrDefault();
            return getRoot;
        }

        public abstract SyntaxNode Fix(SyntaxNode model);
    }
}
