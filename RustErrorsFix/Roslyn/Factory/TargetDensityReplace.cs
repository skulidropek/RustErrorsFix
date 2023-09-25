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
    internal class TargetDensityReplace : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, @"([^\s]+)\._targetDensity", "($1 as DensitySpawnPopulation)._targetDensity");

            return code.ToSyntaxNode();
        }
    }
}
