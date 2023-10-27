using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.Extensions;
using RustErrorsFixLibrary.Core.Interface;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RustErrorsFixLibrary.Core
{
    public class CodeFixManager// : ICodeFixManager
    {
        private readonly List<CompilationErrorModel> _errors = new List<CompilationErrorModel>();
        private readonly CodeFixStrategyConfiguration _configuration;
        private readonly CodeEditor _codeEditor;
        private readonly AnalyzerConfiguration _analyzerConfiguration;
        private readonly IServiceProvider _serviceProvider;
        private SyntaxTree _pluginSyntaxTree;

        public SyntaxTree SyntaxTree => _pluginSyntaxTree;

        public CodeFixManager(AnalyzerConfiguration analyzerConfiguration, CodeEditor codeEditor, CodeFixStrategyConfiguration configuration, IServiceProvider serviceProvider)
        {
            _analyzerConfiguration = analyzerConfiguration;
            _codeEditor = codeEditor;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<string> GetErrors()
        {
            return _errors.Select(e => $"[{e.Line},{e.Symbol}]{e.Text}");
        }

        public ImmutableArray<Diagnostic> GetDiagnostics(CSharpCompilation compilation)
        {
            List<DiagnosticAnalyzer> diagnosticAnalyzers = new List<DiagnosticAnalyzer>();

            foreach(var diagnostic in _analyzerConfiguration.Configuration)
            {
                diagnosticAnalyzers.Add((DiagnosticAnalyzer)_serviceProvider.GetRequiredService(diagnostic));
            }

            var analyzers = diagnosticAnalyzers.ToImmutableArray();
            var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);
            var diagnostics = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().GetAwaiter().GetResult();

            diagnostics = diagnostics.AddRange(compilation.GetDiagnostics());

            return diagnostics;
        }

        public void Compilation(SyntaxTree tree, string librarisFolder)
        {
            _errors.Clear();
            _pluginSyntaxTree = tree;

            foreach (var diagnostic in GetDiagnostics(tree.CreateCompilation("Plugin", librarisFolder)))//)
            {
                if (diagnostic.DefaultSeverity == DiagnosticSeverity.Error)
                {
                    var match = Regex.Match(diagnostic.ToString(), @"\((.+),(.+)\):(.+)");

                    _errors.Add(new CompilationErrorModel()
                    {
                        Line = int.Parse(match.Groups[1].ToString()),
                        Symbol = int.Parse(match.Groups[2].ToString()),
                        Text = match.Groups[3].ToString(),
                        Location = diagnostic.Location
                    });
                }
            }

            if (_errors.Count == 0)
                Console.WriteLine("Ошибки не обнаружены");
        }

        public void SubscribeToAnalysis(string nameOrRegex, IEnumerable<CodeFixStrategy> abstractErrorModel)
        {
            var errors = Find(nameOrRegex);

            if (errors == null)
                return;

            foreach (var error in errors)
                error.CodeFixStrategy.AddRange(abstractErrorModel);
        }

        public void RunFixErrorNow(string nameOrRegex, IEnumerable<CodeFixStrategy> abstractFactories)
        {
            var errors = Find(nameOrRegex);

            if (errors == null)
                return;

            foreach (var error in errors)
            {
                foreach (var abstractFactory in abstractFactories)
                {
                    _pluginSyntaxTree = abstractFactory.Fix(_pluginSyntaxTree.GetRoot(), (error.Line, error.Symbol), error).SyntaxTree;
                }
            }
        }

        public SyntaxNode RunFix()
        {
            foreach (var errorModel in _configuration.Configuration)
            {
                if (!errorModel.IsActive) continue;

                var fixs = errorModel.FixStrategies.Select(f => (CodeFixStrategy)_serviceProvider.GetRequiredService(f));

                if (errorModel.RequiresAnalysis)
                {
                    SubscribeToAnalysis(errorModel.ErrorText, fixs);
                    continue;
                }

                RunFixErrorNow(errorModel.ErrorText, fixs);
            }

            return _codeEditor.Visit(_pluginSyntaxTree.GetRoot(), _errors);
        }

        public bool HaveError(string nameOrRegex)
        {
            return Find(nameOrRegex) != null;
        }

        private IEnumerable<CompilationErrorModel> Find(string nameOrRegex)
        {
            var compilationErrorModels = _errors.Where(e => Regex.IsMatch(e.Text, nameOrRegex));

            foreach(var compilationErrorModel in compilationErrorModels)
                compilationErrorModel.Parametrs = Regex.Match(compilationErrorModel.Text, nameOrRegex).Groups;

            return compilationErrorModels;
        }
    }
}
