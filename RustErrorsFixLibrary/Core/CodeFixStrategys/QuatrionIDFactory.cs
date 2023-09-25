using Microsoft.CodeAnalysis;
using RustErrorsFixLibrary.Core.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class QuatrionIDFactory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToFullString();

            code = code.Replace("Quaternion.ID.Valueentity", "Quaternion.identity");

            return ToSyntaxNode(code);
        }
    }
}
