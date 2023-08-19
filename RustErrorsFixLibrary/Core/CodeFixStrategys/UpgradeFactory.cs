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
    public class UpgradeFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, @"([^\s=]+)\.(?<!blockDefinition\.)GetGrade\(([^\)]+)", "$1.blockDefinition.GetGrade($2, $1.skinID");
            code = Regex.Replace(code, @"(?<=\.GetGrade\()(.+?(?=\)|,)).*\).costToBuild(?!\()", "$0($1)").Replace("costToBuild", "CostToBuild()");

            return ToSyntaxNode(code);
        }
    }
}
