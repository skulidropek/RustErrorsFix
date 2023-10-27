using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OnActiveItemChangeAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "OnActiveItemChangeError";
    private static readonly LocalizableString Title = "OnActiveItemChange detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'OnActiveItemChange(BasePlayer player, Item oldItem, uint newItemId)' на 'OnActiveItemChange(BasePlayer player, Item oldItem, ItemId newItemId)'";
    private static readonly LocalizableString Description = "The code should not contain 'OnActiveItemChange'";
    private const string Category = "Syntax";

    private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (Regex.IsMatch(context.Node.ToFullString(), @"OnActiveItemChange\(BasePlayer player, Item oldItem, uint newItemId\)"))
        {
            var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
            context.ReportDiagnostic(diag);
        }
    }
}