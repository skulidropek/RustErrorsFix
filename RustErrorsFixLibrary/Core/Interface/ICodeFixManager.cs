using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Interface
{
    public interface ICodeFixManager
    {
        IEnumerable<string> GetErrors();
        void Compilation(SyntaxTree tree);
        void SubscribeToAnalysis(string nameRegex, IEnumerable<CodeFixStrategy> abstractErrorModel);
        void RunFixErrorNow(string nameRegex, IEnumerable<CodeFixStrategy> abstractFactories);
        SyntaxNode RunFix();
        bool HaveError(string nameRegex);
    }
}
