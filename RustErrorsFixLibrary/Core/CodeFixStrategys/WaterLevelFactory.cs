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
    public class WaterLevelFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node) 
        {
            foreach (var invocationExpressionSyntax in node.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                var newInvocationExpressionSyntax = Replace(invocationExpressionSyntax);

                if (invocationExpressionSyntax != newInvocationExpressionSyntax)
                    node = node.ReplaceNode(invocationExpressionSyntax, newInvocationExpressionSyntax);
            }

            return node;
        }

        public InvocationExpressionSyntax Replace(InvocationExpressionSyntax invocationNode)
        {
            var invocationNodeString = invocationNode.ToString();
            if (!invocationNodeString.Contains("WaterLevel.")) return invocationNode;

            if (Regex.IsMatch(invocationNodeString, @",true\s*,\s*true")) return invocationNode;

            var argumentList = invocationNode.ArgumentList;

            var length = argumentList.ChildNodes().Count();

            if (length > 3) return invocationNode;

            ArgumentListSyntax newArgumentList;

            if (length > 1)
            {
                var lastArgument = argumentList.ChildNodes().Last();
                argumentList = argumentList.RemoveNode(lastArgument, SyntaxRemoveOptions.KeepEndOfLine);

                if(lastArgument.ToString() == "true" || lastArgument.ToString() == "false")
                {
                    newArgumentList = argumentList.AddArguments(
                        SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                        lastArgument as ArgumentSyntax
                    );
                }
                else
                {
                    newArgumentList = argumentList.AddArguments(
                        SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                        SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                        lastArgument as ArgumentSyntax
                    );
                }
              
            }
            else
            {
                newArgumentList = argumentList.AddArguments(
                    SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression)),
                    SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression))
                    );
            }

            var newInvocationNode = invocationNode.WithArgumentList(newArgumentList);

            return invocationNode.ReplaceNode(invocationNode, newInvocationNode);
        }
    }
}
