using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Model
{
    public class CompilationErrorConfigurationModel
    {
        public string ErrorText { get; set; }
        public bool IsActive { get; set; }
        public bool RequiresAnalysis { get; set; }
        public List<Type> FixStrategies { get; set; } = new List<Type>();
    }
}
