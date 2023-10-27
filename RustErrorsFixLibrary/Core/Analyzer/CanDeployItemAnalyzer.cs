using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class CanDeployItemAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "CanDeployItemError";
    private static readonly LocalizableString Title = "CanDeployItem detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'CanDeployItem(BasePlayer player, Deployer deployer, uint entityId)' на 'CanDeployItem(BasePlayer player, Deployer deployer, NetworkableId entityId)'";
    private static readonly LocalizableString Description = "The code should not contain 'CanDeployItem'";
    private const string Category = "Syntax";

    private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

    public override void Initialize(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.MethodDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        string text = context.Node.ToFullString();
        if (Regex.IsMatch(text, @"CanDeployItem\(BasePlayer\s[\d\w]+,Deployer\s[\d\w]+,uint\s[\d\w]+\)"))
        {
            var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
            context.ReportDiagnostic(diag);
        }
    }
}