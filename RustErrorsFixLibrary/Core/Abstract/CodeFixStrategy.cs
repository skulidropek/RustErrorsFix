using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    public abstract class CodeFixStrategy : CodeFixStrategyGetLine
    {
        protected static SyntaxNode ToSyntaxNode(string code)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
            var getRoot = syntaxTree.GetRoot().DescendantNodes().FirstOrDefault();
            return getRoot;
        }

        public override SyntaxNode Fix(SyntaxNode model, (int, int) errorLine, CompilationErrorModel location)
        {
            return Fix(model);
        }

        public abstract SyntaxNode Fix(SyntaxNode model);
    }
}
