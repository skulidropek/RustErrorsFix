using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Model
{
    public class CompilationErrorModel
    {
        public int Line;
        public int Symbol;
        public string Text;

        public List<CodeFixStrategy> CodeFixStrategy = new List<CodeFixStrategy>();
    }
}
