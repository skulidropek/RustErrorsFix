using Microsoft.CodeAnalysis.CSharp;
using Roslyn_test.Abstract;
using Roslyn_test.Factory;
using Roslyn_test.Managers;
using RustErrorsFix.Core.Abstract;
using RustErrorsFix.Legasy;
using RustErrorsFix.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Core.Factory
{
    internal class RoslynPluginFixerAbstractFactory : PluginFixerAbstractFactory
    {
        private List<RoslynErrorModel> _roslynErrorModels = new List<RoslynErrorModel>();

        public RoslynPluginFixerAbstractFactory(List<RoslynErrorModel> roslynErrorModels)
        {
            _roslynErrorModels = roslynErrorModels;
        }

        public List<string> GetErrors(string plugin)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(plugin);

            ErrorManager errorManager = new ErrorManager();
            errorManager.Compilation(syntaxTree);

            return errorManager.GetErrors();
        } 

        public override string FixPlugin(string pluginContent)
        {
            // Создаем синтаксическое дерево
            var syntaxTree = CSharpSyntaxTree.ParseText(pluginContent);

            ErrorManager errorManager = new ErrorManager();
            errorManager.Compilation(syntaxTree);

            foreach(var errorModel in _roslynErrorModels)
            {
                if (!errorModel.IsActive) continue;

                if(errorModel.IsAnalise)
                {
                    errorManager.SubscribeToAnalysis(errorModel.Text, errorModel.Errors);
                    continue;
                }

                errorManager.RunFixErrorNow(errorModel.Text, errorModel.Errors);
            }

            var plugin = errorManager.RunFix().ToString();

            syntaxTree = CSharpSyntaxTree.ParseText(plugin);

            errorManager = new ErrorManager();
            errorManager.Compilation(syntaxTree);


            return plugin;
        }
    }
}
