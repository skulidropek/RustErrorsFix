using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RustErrorsFixLibrary.Core.Abstract;


namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class ItemCraftTask : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            code = Regex.Replace(code, @"(OnItemCraft\([^,)]+)\)", "$1, BasePlayer player, Item item1)");

            if (code.Contains("OnItemCraft"))
                code = Regex.Replace(code, ".+ player = task.owner;", "");

            code = Regex.Replace(code, @"(?!.*\bItemCrafter\s+itemCrafterOwner\b)((?=.*\b(OnItemCraftFinished|OnItemCraftCancelled)\()[^)]+)", "$1, ItemCrafter itemCrafterOwner");

            //code = Regex.Replace(code, @"(.+ (OnItemCraftFinished\([^)]+)|(OnItemCraftCancelled\([^)]+))", "$1, ItemCrafter itemCrafterOwner");

            if (code.Contains("itemCrafterOwner"))
            {
                var taskName = Regex.Match(code, @"ItemCraftTask ([^,^)]+)").Groups[1].ToString();
                code = Regex.Replace(code, $@"{taskName}()\??.owner", "itemCrafterOwner.owner");
            }

            return ToSyntaxNode(code);
        }
    }
}
