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
    internal class ItemContainerOnlyAllowItem : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            if (Regex.IsMatch(code, @"onlyAllowedItem\s=\s.+inventory\.onlyAllowedItem"))
                code = Regex.Replace(code, @"onlyAllowedItem\s=(\s.+inventory\.onlyAllowedItem)", "onlyAllowedItems = $1s");
            else
                code = Regex.Replace(code, @"onlyAllowedItem\s*=\s*([^;]+)", "SetOnlyAllowedItem($1)");

            return ToSyntaxNode(code);
        }
    }
}
