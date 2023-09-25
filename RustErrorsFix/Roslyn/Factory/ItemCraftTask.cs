using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Roslyn_test.Abstract;
using System.Windows.Forms;
using Roslyn_test.Extensions;

namespace Roslyn_test.Factory
{
    internal class ItemCraftTask : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            code = Regex.Replace(code, @"(.+ (OnItemCraftFinished\([^)]+)|(OnItemCraftCancelled\([^)]+))", "$1, ItemCrafter itemCrafterOwner");

            var taskName = Regex.Match(code, @"ItemCraftTask ([^,^)]+)").Groups[1].ToString();

            code = Regex.Replace(code, $@"{taskName}\??.owner", "itemCrafterOwner.owner");

            return code.ToSyntaxNode();
        }
    }
}
