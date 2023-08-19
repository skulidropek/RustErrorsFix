using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.Extensions;
using RustErrorsFixLibrary.Core.Interface;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core
{
    public class CodeFixManager// : ICodeFixManager
    {
        private readonly List<CompilationErrorModel> _errors = new List<CompilationErrorModel>();
        private readonly CodeFixStrategyConfiguration _configuration;
        private readonly CodeEditor _codeEditor;
        private readonly IServiceProvider _serviceProvider;
        private SyntaxTree _pluginSyntaxTree;

        public CodeFixManager(CodeEditor codeEditor, CodeFixStrategyConfiguration configuration, IServiceProvider serviceProvider)
        {
            _codeEditor = codeEditor;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<string> GetErrors()
        {
            return _errors.Select(e => $"[{e.Line},{e.Symbol}]{e.Text}");
        }

        public void Compilation(SyntaxTree tree, string librarisFolder)
        {
            _errors.Clear();
            _pluginSyntaxTree = tree;

            foreach (var diagnostic in tree.CreateCompilation("Plugin", librarisFolder).GetDiagnostics())
            {
                if (diagnostic.DefaultSeverity == DiagnosticSeverity.Error)
                {
                    var match = Regex.Match(diagnostic.ToString(), @"\((.+),(.+)\):(.+)");

                    _errors.Add(new CompilationErrorModel()
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
            var error = Find(nameOrRegex);

            if (error == null)
                return;

            foreach (var abstractFactory in abstractFactories)
                _pluginSyntaxTree = abstractFactory.Fix(_pluginSyntaxTree.GetRoot()).SyntaxTree;
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
            return _errors.Where(e => Regex.IsMatch(e.Text, nameOrRegex));
        }
    }
}
