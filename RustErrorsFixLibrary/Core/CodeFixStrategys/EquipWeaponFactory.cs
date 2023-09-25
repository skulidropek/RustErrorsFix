using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RustErrorsFixLibrary.Core.Abstract;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class EquipWeaponFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, @"override void EquipWeapon\((?=\))", "$0bool skipDeployDelay = false");

            code = Regex.Replace(code, @"EquipWeapon\((?=\))", "$0skipDeployDelay");

            return ToSyntaxNode(code);
        }
    }
}
