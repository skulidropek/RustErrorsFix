using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class OnStructureUpgradeAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "OnStructureUpgradeError";
    private static readonly LocalizableString Title = "OnStructureUpgrade detected";
    private static readonly LocalizableString MessageFormat = "Данный код устарел! Замените 'OnStructureUpgrade(BaseCombatEntity entity, BasePlayer player, BuildingGrade.Enum grade)' на 'OnStructureUpgrade(BasePlayer player, BuildingGrade.Enum grade, ulong skin)'";
    private static readonly LocalizableString Description = "The code should not contain 'OnStructureUpgrade'";
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
        if (code.Contains("OnStructureUpgrade"))
        {
            if(Regex.IsMatch(code, @"OnStructureUpgrade\(\s*BaseCombatEntity\s[\d\w]+,\s*BasePlayer\s[\d\w]+,\s*BuildingGrade.Enum\s[\d\w]+\)"))
            {
                var diag = Diagnostic.Create(Rule, context.Node.GetLocation());
                context.ReportDiagnostic(diag);
            }
        }
    }
}