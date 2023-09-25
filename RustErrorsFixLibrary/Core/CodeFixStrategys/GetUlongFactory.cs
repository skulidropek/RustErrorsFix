using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Factory
{
    public class GetUlongFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            return ToSyntaxNode(code);
        }
    }
}
