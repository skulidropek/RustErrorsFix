using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OnItemCraftCancelledAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "OnItemCraftCancelledError";
    private static readonly LocalizableString Title = "OnItemCraftCancelled detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'OnItemCraftCancelled(ItemCraftTask task)' на 'OnItemCraftCancelled(ItemCraftTask task, ItemCrafter itemCrafter)'";
    private static readonly LocalizableString Description = "The code should not contain 'OnItemCraftCancelled'";
    private const string Category = "Syntax";

    private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        string code = context.Node.ToFullString();
        if (code.Contains("OnItemCraftCancelled"))
        {
            if(Regex.IsMatch(code, @"OnItemCraftCancelled\(\s*ItemCraftTask\s[\d\w]+\)"))
            {
                var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
                context.ReportDiagnostic(diag);
            }
        }
    }
}