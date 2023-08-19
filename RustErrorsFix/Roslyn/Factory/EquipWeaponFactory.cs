using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Roslyn_test.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Roslyn_test.Extensions;

namespace RustErrorsFix.Roslyn.Factory
{
    internal class EquipWeaponFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();
            code = Regex.Replace(code, @"override void EquipWeapon\((?=\))", "$0bool skipDeployDelay = false");

            code = Regex.Replace(code, @"EquipWeapon\((?=\))", "$0skipDeployDelay");

            return code.ToSyntaxNode();
        }
    }
}
