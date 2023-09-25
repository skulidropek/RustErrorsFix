using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    internal class SpawnPopulationFilter : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            code = Regex.Replace(code, @"\.Filter", ".GetSpawnFilter()");

            return ToSyntaxNode(code);
        }
    }
}
