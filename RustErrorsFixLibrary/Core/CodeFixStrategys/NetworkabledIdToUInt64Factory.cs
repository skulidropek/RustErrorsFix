using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Operations;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class NetworkabledIdToUInt64Factory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, @"subEntity\s=\s.+instanceData.subEntity", "$0.Value");

            return ToSyntaxNode(code);
        }
    }
}
