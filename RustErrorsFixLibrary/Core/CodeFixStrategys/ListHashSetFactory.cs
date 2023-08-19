using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RustErrorsFixLibrary.Core.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class ListHashSetFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();
            code = Regex.Replace(code, @"(BasePlayer\.activePlayerList)(\.ForEach\(.+\))", "$1.ToList()$2");

            return ToSyntaxNode(code);
        }
    }
}
