using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn_test.Abstract;
using System.Text.RegularExpressions;
using Roslyn_test.Extensions;

namespace Roslyn_test.Factory
{
    internal class ReplaceUidToUidValueFactory : AbstractFactory
    {
        List<string> _netStructs = new List<string>() 
        {
            "NetworkableId",
            "ItemId",
            "ItemContainerId"
        };


        public override SyntaxNode Fix(SyntaxNode node)
        {
            var rewriter = new EntityIdRewriter();

            node = rewriter.Visit(node);

            string code = node.ToString().Replace(".Value.Value", ".Value");

            foreach(Match match in Regex.Matches(code, @"([^\s()]+)(\s=\s.+\.uid\.Value)"))
            {
                var group1 = match.Groups[1].ToString();

                foreach(var netStruct in _netStructs)
                {
                    if (Regex.IsMatch(code, netStruct + @"\s" + group1))
                    {
                        var replace = group1 + match.Groups[2].ToString();

                        code = code.Replace(replace, replace.Replace(".Value", ""));
                    }
                }
            }

            return code.ToSyntaxNode();
        }

        public class EntityIdRewriter : CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (node.Identifier.ValueText == "uid" && node.Parent is MemberAccessExpressionSyntax memberAccess &&
                    memberAccess.Name.Identifier.ValueText != "Value")
                {
                    return SyntaxFactory.IdentifierName("uid.Value");
                }

                return base.VisitIdentifierName(node);
            }
        }
    }
}
