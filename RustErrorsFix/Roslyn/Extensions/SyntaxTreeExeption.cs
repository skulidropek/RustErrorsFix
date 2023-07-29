using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RustErrorsFix.Roslyn.Managers;

namespace Roslyn_test.Extensions;

internal static class SyntaxTreeExeption
{
    public static CSharpCompilation CreateCompilation(this SyntaxTree source, string compilationName)
    {
        List<MetadataReference> references = new List<MetadataReference>();

        foreach (var path in Directory.GetFiles(RustReferenseManager.Path).Where(f => !f.Contains("Newtonsoft.Json.dll")))
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
