using RustErrorsFix.Roslyn.Factory;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.CodeFixStrategys;
using RustErrorsFixLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary.Core
{
    public class CodeFixStrategyConfiguration
    {
        public List<CompilationErrorConfigurationModel> Configuration { get; } = new List<CompilationErrorConfigurationModel> {
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""uint"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(ReplaceUInt32ToUInt64Factory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""(NetworkableId|ItemId|ItemContainerId)"" в ""uint""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(ReplaceUInt32ToUInt64Factory), typeof(ReplaceIDToIDValueFactory), typeof(ReplaceUidToUidValueFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""\?\?"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""(int|uint|ulong)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceIDToIDValueFactory), typeof(ReplaceUidToUidValueFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""(uint|ulong)"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(CanMoveItemFactory), typeof(ReplaceUidToUidValueFactory), typeof(UInt64ToNetworkabledIdFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""(uint|ulong)"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(CanMoveItemFactory), typeof(ReplaceUidToUidValueFactory), typeof(UInt64ToNetworkabledIdFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""(NetworkableId|ItemId|ItemContainerId)"" в ""(uint|ulong)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceIDToIDValueFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Ни одна из перегрузок метода ""(Factor|Test|GetWaterDepth|GetOverallWaterDepth|GetWaterInfo)"" не принимает \d аргументов",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(WaterLevelFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""waves"" из ""WaterLevel\.(Factor|Test|GetWaterDepth|GetOverallWaterDepth|GetWaterInfo)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(WaterLevelFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""altMove"" из ""BasePlayer.GetIdealContainer\(BasePlayer, Item, bool\)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(GetIdealContainerFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".+\(.+\)"": не все пути к коду возвращают значение.",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(CrashCodeFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ItemCraftTask"" не содержит определения ""owner"".+",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemCraftTask) }
            },
            //new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @"""SpawnPopulationBase"" не содержит определения ""_targetDensity""",
            //    RequiresAnalysis = true,
            //    FixStrategies = new List<Type>() { typeof(TargetDensityReplace) }
            //},
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BuildingBlock"" не содержит определения ""GetGrade"", и не удалось найти доступный метод расширения ""GetGrade"", принимающий тип ""BuildingBlock"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(UpgradeFactory) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ConstructionGrade"" не содержит определения ""costToBuild"", и не удалось найти доступный метод расширения ""costToBuild"", принимающий тип ""ConstructionGrade"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(UpgradeFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @""".+.EquipWeapon\(\)"": не найден метод, пригодный для переопределения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(EquipWeaponFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ListHashSet<BasePlayer>"" не содержит определения ""ForEach"", и не удалось найти доступный метод расширения ""ForEach"", принимающий тип ""ListHashSet<BasePlayer>"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ListHashSetFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Quaternion"" не содержит определение для ""ID""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(QuatrionIDFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseEntityEx"" не содержит определение для ""IsValid""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseEntityExValid) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""SpawnPopulation""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(SpawnPopulationNotFound) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""SpawnHandler"" не содержит определения ""GetTargetCount"", и не удалось найти доступный метод расширения ""GetTargetCount"", принимающий тип ""SpawnHandler"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(GetTargetCount) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""SpawnPopulation.+"" не содержит определения ""Filter"", и не удалось найти доступный метод расширения ""Filter"", принимающий тип ""SpawnPopulation.+"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(SpawnPopulationFilter) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""SpawnPopulation.+"" не содержит определения ""(_targetDensity|ScaleWithLargeMaps|ScaleWithSpawnFilter|ResourceList|ResourceFolder)"", и не удалось найти доступный метод расширения ""(_targetDensity|ScaleWithLargeMaps|ScaleWithSpawnFilter|ResourceList|ResourceFolder)"", принимающий тип ""SpawnPopulation.+"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(DensitySpawnPopulation) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""(ulong|uint)"" в ""NetworkableId""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceUlongToIDFactory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""NetworkableId"" в ""(ulong|uint)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(NetworkabledIdToUInt64Factory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""ulong"" в ""uint"". Существует явное преобразование \(возможно, пропущено приведение типов\)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceUintToUlong) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Перед директивами препроцессору могут находиться только пробельные знаки",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(UndregionFix) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""bool"" в ""BaseEntity""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(WaterLevelBoolToNull) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""<NULL>"" в ""bool""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(WaterLevelNullToBool) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""HTNPlayer""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(HNTPlayerToScientistNPC) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""NPCMurderer""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(NPCMurdererToScarecrowNPC) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""RelationshipManager"" не содержит определение для ""(Instance|_instance)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(InstanceToServerInstance) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Server"" не содержит определения ""write"", и не удалось найти доступный метод расширения ""write"", принимающий тип ""Server"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(NetSv) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""!="" невозможно применить к операнду типа ""ItemContainerId"" и ""uint""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(UInt64NotEqualItemContainerId) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ModularCar"" не содержит определения ""(fuelSystem|FinishStartingEngine|CanRunEngines|carLock)"", и не удалось найти доступный метод расширения ""(fuelSystem|FinishStartingEngine|CanRunEngines|carLock)"", принимающий тип ""ModularCar"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ModularCar) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ItemContainer"" не содержит определения ""onlyAllowedItem"", и не удалось найти доступный метод расширения ""onlyAllowedItem"", принимающий тип ""ItemContainer"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemContainerOnlyAllowItem) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Recycler"" не содержит определения ""dropChance"", и не удалось найти доступный метод расширения ""dropChance"", принимающий тип ""Recycler"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(DropChanceRecylerRemove) }
            },
        };
    }
}
