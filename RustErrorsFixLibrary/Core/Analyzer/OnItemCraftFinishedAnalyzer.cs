using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OnItemCraftFinishedAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "OnItemCraftFinishedError";
    private static readonly LocalizableString Title = "OnItemCraftFinished detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'OnItemCraftFinished(ItemCraftTask task, Item item)' на 'OnItemCraftFinished(ItemCraftTask task, Item item, ItemCrafter itemCrafter)'";
    private static readonly LocalizableString Description = "The code should not contain 'OnItemCraftFinished'";
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
        if (code.Contains("OnItemCraftFinished"))
        {
            if(Regex.IsMatch(code, @"OnItemCraftFinished\(\s*ItemCraftTask\s[\d\w]+,\s*Item\s[\d\w]+\)"))
            {
                var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
                context.ReportDiagnostic(diag);
            }
        }
    }
}