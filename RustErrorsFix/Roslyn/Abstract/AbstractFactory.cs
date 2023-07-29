using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roslyn_test.Abstract
{
    internal abstract class AbstractFactory
    {
        public abstract SyntaxNode Fix(SyntaxNode model);
    }
}
