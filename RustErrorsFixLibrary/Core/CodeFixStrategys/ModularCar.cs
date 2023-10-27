using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    internal class ModularCar : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = Regex.Replace(code, "(?<!engineController)\\.FinishStartingEngine", ".engineController.FinishStartingEngine");
            code = Regex.Replace(code, @"\.carLock", ".CarLock");
            code = Regex.Replace(code, @"\.GetFuelSystem(?!\()", ".GetFuelSystem()");
            code = Regex.Replace(code, @"\.fuelSystem", ".GetFuelSystem()");
            code = Regex.Replace(code, "CanRunEngines", "engineController.CanRunEngine");

            return ToSyntaxNode(code);
        }
    }
}
