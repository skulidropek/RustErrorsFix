using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core.Extensions
{
    public static class SyntaxTreeExtensions
    {
        public static CSharpCompilation CreateCompilation(this SyntaxTree source, string compilationName, string managedFolder)
        {
            List<MetadataReference> references = new List<MetadataReference>();

            foreach (var path in Directory.GetFiles(managedFolder).Where(f => !f.Contains("Newtonsoft.Json.dll")))
            {
                references.Add(MetadataReference.CreateFromFile(path.Replace("\n", "").Replace("\r", "")));
            }

            return CSharpCompilation.Create(compilationName,
                    syntaxTrees: new[]
                    {
                    source
                    },
                    references: references,
                    options: new CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary)
                );
        }
    }
}
