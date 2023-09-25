using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RustErrorsFixLibrary.Core.Abstract;
using System.Text.RegularExpressions;


namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class ReplaceUidToUidValueFactory : CodeFixStrategy
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

            string code = node.ToFullString().Replace(".Value.Value", ".Value");

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

            return ToSyntaxNode(code);
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
