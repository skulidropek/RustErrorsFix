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
    public class InstanceToServerInstance : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode node)
        {
            string code = node.ToFullString();

            code = code.Replace(".Instance.", ".ServerInstance.");
            code = code.Replace("._instance.", ".ServerInstance.");

            return ToSyntaxNode(code);
        }
    }
}
