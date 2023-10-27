using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Abstract
{
    public abstract class CodeFixStrategyGetLine
    {
        public abstract SyntaxNode Fix(SyntaxNode model, (int, int) errorLine, CompilationErrorModel location);
    }
}
