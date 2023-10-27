using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class CanMoveItemAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CanMoveItemError";
    private static readonly LocalizableString Title = "CanMoveItem detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'CanMoveItem(Item item, PlayerInventory playerLoot, uint targetContainer, int targetSlot, int amount)' на 'CanMoveItem(Item item, PlayerInventory playerLoot, ItemContainerId targetContainer, int targetSlot, int amount)'";
    private static readonly LocalizableString Description = "The code should not contain 'CanMoveItem'";
    private const string Category = "Syntax";

    private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (Regex.IsMatch(context.Node.ToFullString(), @"CanMoveItem\(\s*Item\s[\d\w]+,\s*PlayerInventory\s[\d\w]+,\s*uint\s[\d\w]+,\s*int\s[\d\w]+,\s*int\s[\d\w]+\)"))
        {
            var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
            context.ReportDiagnostic(diag);
        }
    }
}