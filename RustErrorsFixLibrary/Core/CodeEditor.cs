using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Interface;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core
{
    public class CodeEditor : CSharpSyntaxRewriter, ICodeEditor
    {
        private List<CompilationErrorModel> _errors;

        public SyntaxNode Visit(SyntaxNode node, IEnumerable<CompilationErrorModel> errors)
        {
            _errors = new List<CompilationErrorModel>(errors);
            return base.Visit(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node) => FixError(node);

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax method) => FixError(method);

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node) => FixError(node);

        public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => FixError(node);

        public SyntaxNode FixError(SyntaxNode node)
        {
            var location = GetLocation(node.GetLocation());

            if (!TryGetError(location, out List<CompilationErrorModel> errors))
                return node;

            foreach (var error in errors)
            {
                foreach (var abstractFactory in error.CodeFixStrategy)
                {
                    string nodeText = node.ToFullString();

                    node = abstractFactory.Fix(node, (error.Line - location.Item1, error.Symbol), error);

                    if (node == null)
                        return null;
                }
            }

            return node;
        }

        public bool TryGetError((int, int) location, out List<CompilationErrorModel> errorOut)
        {
            errorOut = new List<CompilationErrorModel>();

            if(location.Item1 > 250)
            {

            }
            foreach (var error in _errors)
            {
                //Console.WriteLine($"{location.Item1} <= {error.Line} && {error.Line} <= {location.Item2}");
                if (location.Item1 <= error.Line && error.Line <= location.Item2)
                {
                    errorOut.Add(error);
                }
            }

            return errorOut.Count > 0;
        }

        public (int, int) GetLocation(Location location)
        {
            // Получаем объект SyntaxTree для узла метода
            var syntaxTree = location.SourceTree;

            // Получаем объект FileLinePositionSpan для узла метода
            var span = syntaxTree.GetLineSpan(location.SourceSpan);

            // Получаем объект LinePosition для начала и конца узла метода
            var start = span.StartLinePosition;
            var end = span.EndLinePosition;

            // Получаем номера строк для начала и конца узла метода
            var startLine = start.Line + 1; // Прибавляем 1, так как LinePosition.Line начинается с 0
            var endLine = end.Line + 1; // Прибавляем 1, так как LinePosition.Line начинается с 0

            return (startLine, endLine);
        }
    }
}
