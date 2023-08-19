using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Interface
{
    public interface ICodeEditor
    {
        SyntaxNode Visit(SyntaxNode node, IEnumerable<CompilationErrorModel> errors);
    }
}
