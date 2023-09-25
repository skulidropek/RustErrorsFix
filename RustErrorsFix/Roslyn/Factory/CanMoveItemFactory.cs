using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslyn_test.Abstract;
using Roslyn_test.Extensions;
using Roslyn_test.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Factory
{
    internal class CanMoveItemFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            if (!Regex.IsMatch(code, @"(.+ CanMoveItem\([^)]+)"))
                return node;

            var canMoveItem = Regex.Match(code, @"(.+ CanMoveItem\([^)]+)").Groups[1].ToString();

            if(canMoveItem.Contains("ItemContainerId")) return node;

            code = code.Replace(canMoveItem, canMoveItem.Replace("ulong", "ItemContainerId") + ", bool fag");

            return code.ToSyntaxNode();
        }
    }
}
