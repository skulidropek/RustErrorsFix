using Microsoft.CodeAnalysis.Diagnostics;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core
{
    public class AnalyzerConfiguration
    {
        public List<Type> Configuration { get; } = new List<Type>
        {
            typeof(OnPlayerInitAnalyzer),
            typeof(OnCropGatherAnalyzer),
            typeof(CanAffordUpgradeAnalyzer),
            typeof(CanDeployItemAnalyzer),
            typeof(CanMoveItemAnalyzer),
            typeof(OnActiveItemChangeAnalyzer),
            typeof(OnStructureUpgradeAnalyzer),
            typeof(OnItemCraftCancelledAnalyzer),
            typeof(OnItemCraftFinishedAnalyzer),
            typeof(OnPlayerChatAnalyzer),
        };
    }
}
