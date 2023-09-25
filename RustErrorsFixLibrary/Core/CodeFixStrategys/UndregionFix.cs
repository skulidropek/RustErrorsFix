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
    internal class UndregionFix : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();
            code = Regex.Replace(code, @"#endregion.*", "");
            code = Regex.Replace(code, @"#region.*", "");
            return ToSyntaxNode(code);
        }
    }
}
