using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    internal class DensitySpawnPopulation : CodeFixStrategy
    {
        private List<string> _names = new List<string>() 
        {
            "ScaleWithLargeMaps",
            "ScaleWithSpawnFilter",
            "ResourceList",
            "ResourceFolder",
            "_targetDensity",
        };

        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            foreach(var name in _names)
            {
                code = Regex.Replace(code, @"(\b(?!DensitySpawnPopulation)\b[^\s,)(]+)\." + name, $"($1 as DensitySpawnPopulation).{name}");
            }

            return ToSyntaxNode(code);
        }
    }
}
