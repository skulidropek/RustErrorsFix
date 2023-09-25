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

namespace Roslyn_test.Factory
{
    internal class FindContainerFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            CompilationUnitSyntax root = node.SyntaxTree.GetCompilationUnitRoot();

            // Используем SyntaxWalker для нахождения вызовов метода inventory.FindContainer
            var methodInvocationVisitor = new MethodInvocationVisitor();
            methodInvocationVisitor.Visit(root);

            // Получаем аргументы вызова метода
            var arguments = methodInvocationVisitor.Arguments;

            // Если найден хотя бы один вызов метода
            if (arguments.Count > 0)
            {
                // Получаем тип данных аргумента
                var argumentType = arguments[0];

                Console.WriteLine("Тип данных аргумента метода inventory.FindContainer: " + argumentType);
            }
            else
            {
                Console.WriteLine("Вызов метода inventory.FindContainer не найден");
            }

            return node;
        }

        class MethodInvocationVisitor : CSharpSyntaxWalker
        {
            public List<ArgumentSyntax> Arguments { get; } = new List<ArgumentSyntax>();

            public override void VisitCompilationUnit(CompilationUnitSyntax node)
            {
                Console.WriteLine(node + "\n\nфывфыв");
                base.VisitCompilationUnit(node);
            }

            public override void VisitInvocationExpression(InvocationExpressionSyntax node)
            {
                if (node.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    if (memberAccess.Name.Identifier.ValueText == "FindContainer")
                    {
                        var argument = node.ArgumentList.Arguments.FirstOrDefault();
                        if (argument != null)
                        {
                            Arguments.Add(argument);
                        }
                    }
                }

                base.VisitInvocationExpression(node);
            }
        }
    }
}
