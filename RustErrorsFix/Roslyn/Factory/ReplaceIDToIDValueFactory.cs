﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn_test.Abstract;
using System.Text.RegularExpressions;

namespace Roslyn_test.Factory
{
    internal class ReplaceIDToIDValueFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            var rewriter = new EntityIdRewriter();

            node = rewriter.Visit(node);

            return node;
        }

        public class EntityIdRewriter : CSharpSyntaxRewriter
        {
            public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (node.Identifier.ValueText == "ID")
                {
                    if(Regex.IsMatch(node.Parent.Parent.ToString(), @"\??\.net\??.ID"))
                    {
                        if (node.ChildNodes().Count() == 0)
                        {
                            return SyntaxFactory.IdentifierName("ID.Value");
                        }
                    }
                }

                return base.VisitIdentifierName(node);
            }
        }
    }
}
