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
    public class GetIdealContainerFactory : CodeFixStrategy
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
            if (!invocationNodeString.Contains("GetIdealContainer")) return invocationNode;

            if (Regex.IsMatch(invocationNodeString, @",true")) return invocationNode;

            var argumentList = invocationNode.ArgumentList;

            var length = argumentList.ChildNodes().Count();

            ArgumentListSyntax newArgumentList;

            newArgumentList = argumentList.AddArguments(
                SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression))
                );

            var newInvocationNode = invocationNode.WithArgumentList(newArgumentList);

            return invocationNode.ReplaceNode(invocationNode, newInvocationNode);
        }
    }
}
