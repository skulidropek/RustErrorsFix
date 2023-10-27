using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RustErrorsFixLibrary.Core.Abstract;

namespace RustErrorsFixLibrary.Core.CodeFixStrategys
{
    public class ReplaceUInt32ToUInt64Factory : CodeFixStrategy
    {
        public override SyntaxNode Fix(SyntaxNode root)
        {
            // Находим все упоминания типа uint
            var uintReferences = root.DescendantNodes().OfType<PredefinedTypeSyntax>()
                .Where(type => type.Keyword.Kind() == SyntaxKind.UIntKeyword);

            // Заменяем упоминания типа uint на ulong
            SyntaxNode updatedRoot = root.ReplaceNodes(uintReferences, (original, _) =>
                SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ULongKeyword))
                    .WithTriviaFrom(original));

            var code = updatedRoot.ToFullString();

            code = code.Replace("UInt32", "UInt64");

            return ToSyntaxNode(code);
        }
    }
}
