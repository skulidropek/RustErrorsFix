using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn_test.Abstract;
using Roslyn_test.Core;
using Roslyn_test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roslyn_test.Managers
{
    internal class ErrorManager
    {
        private List<Error> _errors = new List<Error>();
        private SyntaxTree _pluginSyntaxTree;

        public List<string> GetErrors()
        {
            return _errors.Select(e => "[" + e.Line + "," +e.Symbol + "]" + e.Text).ToList();
        }

        public void Compilation(SyntaxTree tree)
        {
            _pluginSyntaxTree = tree;
            foreach (var diagnostic in tree.CreateCompilation("Plugin").GetDiagnostics())
            {
                if (diagnostic.DefaultSeverity == DiagnosticSeverity.Error)
                {
                    var match = Regex.Match(diagnostic.ToString(), @"\((.+),(.+)\):(.+)");

                    Console.WriteLine(diagnostic);

                    _errors.Add(new Error()
                    {
                        Line = int.Parse(match.Groups[1].ToString()),
                        Symbol = int.Parse(match.Groups[2].ToString()),
                        Text = match.Groups[3].ToString()
                    });
                }
            }

            if (_errors.Count == 0)
                Console.WriteLine("Ошибки не обнаружены");
        }
        
        public void SubscribeToAnalysis(string nameOrRegex, List<AbstractFactory> abstractErrorModel)
        {
            var errors = Find(nameOrRegex);

            if (errors == null)
                return;

            foreach(var error in errors)
                error.AbstractFactorys.AddRange(abstractErrorModel);
        }
        
        public void RunFixErrorNow(string nameOrRegex, List<AbstractFactory> abstractFactories)
        {
            var error = Find(nameOrRegex);

            if (error == null)
                return;

            foreach(var abstractFactory in abstractFactories)
            {
                _pluginSyntaxTree = abstractFactory.Fix(_pluginSyntaxTree.GetRoot()).SyntaxTree;
            }
        }

        public SyntaxNode RunFix()
        {
            // Создаем редактор кода
            var editor = new CodeEditor(_errors);

            // Применяем редактор к синтаксическому дереву
            return editor.Visit(_pluginSyntaxTree.GetRoot());
        }


        public bool HaveError(string nameOrRegex)
        {
            return Find(nameOrRegex) != null;
        }

        private IEnumerable<Error> Find(string nameOrRegex)
        {
            return _errors.Where(e => Regex.IsMatch(e.Text, nameOrRegex));
        }

        class CodeEditor : CSharpSyntaxRewriter
        {
            private List<Error> _errors = new List<Error>();

            public CodeEditor(List<Error> errors)
            {
                _errors = errors;
            }

            public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
            {
                var location = GetLocation(node.GetLocation());

                if (!TryGetError(location, out Error error))
                    return node;


                foreach(var abstractFactory in error.AbstractFactorys)
                {
                    node = abstractFactory.Fix(node) as MethodDeclarationSyntax;

                    if (node == null)
                        return null;

                }

                return node;
            }

            public bool TryGetError((int, int) location, out Error errorOut)
            {
                foreach (var error in _errors)
                {
                    //Console.WriteLine($"{location.Item1} <= {error.Line} && {error.Line} <= {location.Item2}");
                    if (location.Item1 <= error.Line && error.Line <= location.Item2)
                    {
                        errorOut = error;
                        return true;
                    }
                }

                errorOut = null;
                return false;
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
}
