using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    internal abstract class CodeFixStrategyString : CodeFixStrategy
    {
        //CodeFixManager _codeFixManager;
        public abstract string Fix(string text);

        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            //model.Parent.Parent.Parent.Parent.ToString();

            code = Fix(code);

            return ToSyntaxNode(code);
        }
    }
}
