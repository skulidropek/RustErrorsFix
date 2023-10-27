using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OnCropGatherAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "OnCropGatherError";
    private static readonly LocalizableString Title = "OnCropGather detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'OnCropGather' на 'OnGrowableGathered'";
    private static readonly LocalizableString Description = "The code should not contain 'OnCropGather'";
    private const string Category = "Syntax";

    private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.ToFullString().Contains("OnCropGather"))
        {
            var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
            context.ReportDiagnostic(diag);
        }
    }
}