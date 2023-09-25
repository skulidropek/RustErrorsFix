using Microsoft.CodeAnalysis;
using Roslyn_test.Abstract;
using Roslyn_test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Roslyn.Factory
{
    internal class QuatrionIDFactory : AbstractFactory
    {
        public override SyntaxNode Fix(SyntaxNode model)
        {
            string code = model.ToString();

            code = code.Replace("Quaternion.ID.Valueentity", "Quaternion.identity");

            return code.ToSyntaxNode();
        }
    }
}
