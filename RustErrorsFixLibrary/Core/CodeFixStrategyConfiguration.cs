using RustErrorsFix.Roslyn.Factory;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.CodeFixStrategys;
using RustErrorsFixLibrary.Core.CodeFixStrategysGPTStringGetLine;
using RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine;
using RustErrorsFixLibrary.Core.CodeFixStrategysString;
using RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine;
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
                ErrorText = @"Не удалось найти тип или имя пространства имен ""Apex""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(RemoveApexUsing) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ListHashSet<BasePlayer>"" не содержит определения ""ForEach"", и не удалось найти доступный метод расширения ""ForEach"", принимающий тип ""ListHashSet<BasePlayer>"" в качестве первого аргумента \(возможно, пропущена директива using или ссылка на сборку\)",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(AddUsingLinq) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""uint"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(ReplaceUInt32ToUInt64Factory) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""(NetworkableId|ItemId|ItemContainerId|ulong)"" в ""uint""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(ReplaceUInt32ToUInt64Factory) }
            },
            //new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @"не удается преобразовать из ""(NetworkableId|ItemId|ItemContainerId)"" в ""uint""",
            //    RequiresAnalysis = false,
            //    FixStrategies = new List<Type>() { typeof(ReplaceUInt32ToUInt64Factory), typeof(ReplaceIDToIDValueFactory), typeof(ReplaceUidToUidValueFactory) }
            //},
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""\?\?"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""(int|uint|ulong)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceIDToIDValueFactory), typeof(ReplaceUidToUidValueFactory) }
            },    
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""=="" невозможно применить к операнду типа ""ulong"" и ""(NetworkableId|ItemId|ItemContainerId)\??""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ReplaceIDToIDValueFactory), typeof(ReplaceUidToUidValueFactory) }
            },
            // new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @"Не удается неявно преобразовать тип ""(NetworkableId|ItemId|ItemContainerId|ulong)"" в ""uint""",
            //    RequiresAnalysis = false,
            //    FixStrategies = new List<Type>() { typeof(ReplaceUidToUidValueFactory) }
            //},
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (тип|из) ""(uint|ulong)"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(CanMoveItemFactory),  typeof(UInt64ToNetworkabledIdFactory) }
            },
            //typeof(ReplaceUidToUidValueFactory),
            //new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @"""=="" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)"" и ""(uint|ulong)""",
            //    RequiresAnalysis = true,
            //    FixStrategies = new List<Type>() { typeof(CanMoveItemFactory), typeof(ReplaceUidToUidValueFactory), typeof(UInt64ToNetworkabledIdFactory) }
            //},
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно )?преобразовать (тип|из) ""(NetworkableId|ItemId|ItemContainerId)"" в ""(uint|ulong)""",
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
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""(waves|volumes)"" из ""WaterLevel\.(Factor|Test|GetWaterDepth|GetOverallWaterDepth|GetWaterInfo)",
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
                ErrorText = @""".+"": не все пути к коду возвращают значение.",
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
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""player"" из ""BuildingBlock.(CanAffordUpgrade|CanChangeToGrade)",
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
                FixStrategies = new List<Type>() { typeof(ReplaceUintToUlong), typeof(ReplaceUintStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Перед директивами препроцессору могут находиться только пробельные знаки",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(UndregionFix) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Непредвиденная директива препроцессору",
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
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""Scientist""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ScientistToScientistNPC) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается преобразовать группу методов ""Scientist"" в тип, не являющийся делегатом ""BaseEntity"". Предполагалось вызывать этот метод?",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ScientistToScientistNPC) }
            },  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Для нестатического поля, метода или свойства ""FileSystemBackend.cache"" требуется ссылка на объект",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(FileSystemBackendCache) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""ulong"" в ""DecayEntity""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(DecayEntityUInt64ToUInt32) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""CSharpExtension"" не содержит определение для ""SandboxEnabled""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(SandboxRemove) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""(int|ulong)"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(UlongToNetworkabledParametrStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""ulong"" в ""uint""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(UlongToUintParametrStringGetLine) }
            }, 
            //new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""(int|ulong)"" в ""(ItemId)""",
            //    RequiresAnalysis = true,
            //    FixStrategies = new List<Type>() { typeof(UlongToItemIdGetLine) }
            //},
            // new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""(int|ulong)"" в ""(ItemContainerId)""",
            //    RequiresAnalysis = true,
            //    FixStrategies = new List<Type>() { typeof(UlongToItemContainerIdGetLine) }
            //},
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""(NetworkableId|ItemId|ItemContainerId)"" в ""ulong""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemIdToUlongStringGetLine) }
            },   
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""(!=|=|<|>|>=|<=)"" невозможно применить к операнду типа ""uint"" и ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(EqualsUlongToNetworkabledIdStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""StorageContainer"" не содержит определения ""dropChance"", и не удалось найти доступный метод расширения ""dropChance""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(RemoveDropChanceStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ResearchTable"" не содержит определение для ""GetBlueprintTemplate""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ResearchTableGetBlueprintTemplateStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""MiniCopter""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(MiniCopterStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""MiniCopter"" не существует в текущем контексте",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(MiniCopterStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""PlayerInventory"" не содержит определения ""FindItemUID"", и не удалось найти доступный метод расширения ""FindItemUID"", принимающий тип ""PlayerInventory"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PlayerInventoryFindItemUIDStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""PlayerInventory"" не содержит определения ""FindItemIDs"", и не удалось найти доступный метод расширения ""FindItemIDs"", принимающий тип ""PlayerInventory"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PlayerInventoryFindItemsByItemIDStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ItemContainer"" не содержит определения ""FindItemsByItemName"", и не удалось найти доступный метод расширения ""FindItemsByItemName"", принимающий тип ""ItemContainer""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemContainerFindItemsByItemNameStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Доступ к члену ""ResearchTable.ScrapForResearch\(Item\)"" через ссылку на экземпляр невозможен; вместо этого уточните его, указав имя типа",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ResearchTableScrapForResearchStringGetLine) }
            },  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""PlayerInventory"" не содержит определения ""FindItemID"", и не удалось найти доступный метод расширения ""FindItemID"", принимающий тип ""PlayerInventory"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PlayerInventoryFindItemIDStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""MLRS"" не содержит определение для "".+""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(MLRSProjectileDistToGravityStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""BaseHelicopterVehicle""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseHelicopterVehicleToBaseHelicopterStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""PatrolHelicopterVehicle""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PatrolHelicopterVehicleToPatrolHelicopterStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""(BasePlayer|NPCPlayer)"" в ""IAmmoContainer""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BasePlayerIAmmoContainerStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Недопустимый оператор объявления элемента инициализатор",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(InvalidStatementStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Условное выражение не может использоваться напрямую в интерполяции строк, так как интерполяция заканчивается на "":"". Заключите условное выражение в скобки",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(InvalidStatementStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseHelicopter"" не содержит определения "".+"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(BaseHelicopterStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""PatrolHelicopter"" в ""BaseHelicopter""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(PatrolHelicopterBaseHelicopterStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""SupplyDrop"" не содержит определения ""parachute"", и не удалось найти доступный метод расширения ""parachute"", принимающий тип ""SupplyDrop"" в качестве первого аргумента",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(SupplyDropParachuteStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseProjectile.Magazine"" не содержит определения ""Reload"", и не удалось найти доступный метод расширения ""Reload"", принимающий тип ""BaseProjectile.Magazine"" в качестве первого аргумента",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(BaseProjectileMagazineStringGetLine) }
            },  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Minicopter"" не содержит определения ""waterSample"", и не удалось найти доступный метод расширения ""waterSample"", принимающий тип ""Minicopter"" в качестве первого аргумента",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(MinicopterWaterSampleStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Ни одна из перегрузок метода ""CanMoveTo"" не принимает 3 аргументов",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(ItemCanMoveToStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Имя ""Net"" не существует в текущем контексте",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(NetStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Minicopter"" является тип, который недопустим в данном контексте",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(MinicopterFieldStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""!="" невозможно применить к операнду типа ""NetworkableId"" и ""(uint|int|ulong)""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(NetworkableIdIntUIntULongNotEqualsStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""BaseHelicopter"" в ""PatrolHelicopter""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseHelicopterPatrolHelicopterStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""JunkPile"" не содержит определения ""PlayersNearby"", и не удалось найти доступный метод расширения ""PlayersNearby"", принимающий тип ""JunkPile"" в качестве первого аргумент",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PlayersNearbyJunkPileStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""TrainEngine"" не содержит определения ""decayDuration"", и не удалось найти доступный метод расширения ""decayDuration"", принимающий тип ""TrainEngine"" в качестве первого аргумента ",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(TrainEnginedecayDurationStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ulong"" не содержит определения ""Value"", и не удалось найти доступный метод расширения ""Value"", принимающий тип ""ulong"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongValueStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""MotorRowboat"" не содержит определения ""dying"", и не удалось найти доступный метод расширения ""dying"", принимающий тип ""MotorRowboat"" в качестве первого аргумента (возможно, пропущена директива using или ссылка на сборку)",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(MotorRowboatdyingStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Ни одна из перегрузок метода ""Add"" не принимает 2 аргументов",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(AddNotTwoAgrumentsStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Недопустимый термин ""(.+)"" в выражении",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongRemoveStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается преобразовать тип ""System.Collections.Generic.KeyValuePair<.+, BaseNetworkable>"" в ""System.Collections.Generic.KeyValuePair<ulong, BaseNetworkable>""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(KeyValuePairBaseNetworkableStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ulong"" не содержит определения ""Value"", и не удалось найти доступный метод расширения ""Value"", принимающий тип ""ulong"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongValueValueStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось изменить возвращаемое значение "".+.uid"", т. к. оно не является переменной",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(EntityRefUidStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""(NetworkableId|ItemId|ItemContainerId)"" в ""ulong""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemIdulongStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ulong"" является тип, который недопустим в данном контексте",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""ulong"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ValueRemoveStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""??"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""int""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ValueIntAddStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается преобразовать тип ""(NetworkableId|ItemId|ItemContainerId)"" в ""int""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemContainerIdToIntParametrStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Оператор ""=="" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)"" и ""int""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(NetworkableIdItemIdItemContainerIdEqualsIntParametrStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается преобразовать группу методов ""Value"" в тип, не являющийся делегатом ""ulong"". Предполагалось вызывать этот метод?",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ValueValueRemoveStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""RelationshipManager"" не содержит определения ""playerGangs"", и не удалось найти доступный метод расширения ""playerGangs"", принимающий тип ""RelationshipManager"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(RelationshipManagerPlayerGangsStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ListHashSet<BasePlayer>"" не содержит определения ""ToArray"", и не удалось найти доступный метод расширения ""ToArray""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(AddLinqStringGetLine) }
            },     
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnCropGather' на 'OnGrowableGathered'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnCropGatherReplaceOnGrowableGatheredString) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnPlayerInit' на 'OnPlayerConnected'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnPlayerInitReplaceOnPlayerConnectedString) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""PlantEntity""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(PlantEntityReplaceGrowableEntityStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""TriggerRadiation"" не содержит определения ""radiationSize"", и не удалось найти доступный метод расширения ""radiationSize"", принимающий тип ""TriggerRadiation"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(TriggerRadiationradiationSizeStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseBoat"" не содержит определения ""myRigidBody"", и не удалось найти доступный метод расширения ""myRigidBody"", принимающий тип ""BaseBoat"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseBoatmyRigidBodyStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""NPCPlayerApex""",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(NPCPlayerApexReplaceBaseNpcStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Имя ""NPCPlayerApex"" не существует в текущем контексте.",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(NPCPlayerApexReplaceBaseNpcStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'CanAffordUpgrade\(BasePlayer player, BuildingBlock block, BuildingGrade.Enum grade\)' на 'CanAffordUpgrade\(BasePlayer player, BuildingBlock block, BuildingGrade.Enum grade, ulong skin\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(CanAffordUpgradeReplaceStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'CanDeployItem\(BasePlayer player, Deployer deployer, uint entityId\)' на 'CanDeployItem\(BasePlayer player, Deployer deployer, NetworkableId entityId\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(CanDeployItemReplaceStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'CanMoveItem\(Item item, PlayerInventory playerLoot, uint targetContainer, int targetSlot, int amount\)' на 'CanMoveItem\(Item item, PlayerInventory playerLoot, ItemContainerId targetContainer, int targetSlot, int amount\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(CanMoveItemReplaceStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnActiveItemChange\(BasePlayer player, Item oldItem, uint newItemId\)' на 'OnActiveItemChange\(BasePlayer player, Item oldItem, ItemId newItemId\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnActiveItemChangeReplaceStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnStructureUpgrade\(BaseCombatEntity entity, BasePlayer player, BuildingGrade.Enum grade\)' на 'OnStructureUpgrade\(BasePlayer player, BuildingGrade.Enum grade, ulong skin\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnStructureUpgradeReplaceStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnItemCraftCancelled\(ItemCraftTask task\)' на 'OnItemCraftCancelled\(ItemCraftTask task, ItemCrafter itemCrafter\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnItemCraftCancelledReplaceStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnItemCraftFinished\(ItemCraftTask task, Item item\)' на 'OnItemCraftFinished\(ItemCraftTask task, Item item, ItemCrafter itemCrafter\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnItemCraftFinishedReplaceStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ListHashSet<.+>"" не содержит определения ""Find"", и не удалось найти доступный метод расширения ""Find"", принимающий тип ""ListHashSet<.+>"" в качестве первого аргумента",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ListHashSetFindToFirtOrDefaultStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""ListHashSet<.+>"" не содержит определения ""Find"", и не удалось найти доступный метод расширения ""Find"", принимающий тип ""ListHashSet<.+>"" в качестве первого аргумента",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(AddLinqStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""BaseCar""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseCarToBasicCarStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""ListHashSet<.+>"" в ""System.Collections.Generic.List<(.+)>""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ListHashSetToListParametrStringGetLine), }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""List<.+>"" не содержит определения ""Length"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ListLengthReplaceCountStringGetLine), }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Аргументы типа для метода ""Array.Resize<T>\(ref T\[\], int\)"" не могут определяться по использованию",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ArrayResizeStringGetLine), }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Требуется "";""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(NeedEndStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""string"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(stringNetworkableIdStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""KeyValuePair<ulong, ApprovedSkinInfo>"" не содержит определения ""(.+)"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ApprovedSkinInfoParametrStringGetLine), }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Данный код устарел! Замените 'OnPlayerChat\(ConsoleSystem.Arg arg\)' на 'OnPlayerChat\(BasePlayer player, string message, Chat.ChatChannel channel\)'",
                RequiresAnalysis = false,
                FixStrategies = new List<Type>() { typeof(OnPlayerChatReplaceStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Не удается неявно преобразовать тип ""ulong"" в ""string""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongToStringParametrStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""=="" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)"" и ""(uint|ulong)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ulongNetworkableIdParametrStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""bool"" в ""ItemContainer""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemContainerBoolParametrStringGetLine) }
            },
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""dt"" из ""AutoTurret.UpdateAiming\(float\)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(AutoTurretUpdateAimingParametrStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseProjectile.Magazine"" не содержит определения ""SwitchAmmoTypesIfNeeded"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseProjectileMagazineSwitchAmmoTypesIfNeededParametrStringGetLine) }
            },  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""Layer"" не содержит определение для ""Debris""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(DerbisParametrStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"не удается преобразовать из ""ItemContainer"" в ""BaseEntity""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(ItemContainerBaseEntityStringGetLine) }
            },  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseProjectile.Magazine"" не содержит определения ""TryReload"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseProjectileMagazineTryReloadStringGetLine) }
            },  
            //new CompilationErrorConfigurationModel()
            //{
            //    ErrorText = @"""=="" невозможно применить к операнду типа ""(uint|ulong)"" и ""(NetworkableId|ItemId|ItemContainerId)""",
            //    RequiresAnalysis = true,
            //    FixStrategies = new List<Type>() { typeof(BaseProjectileMagazineTryReloadStringGetLine) }
            //},  
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"""BaseRidableAnimal"" не содержит определения ""inventory"", и не удалось найти доступный метод расширения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseRidableAnimalInventoryReloadStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Невозможно присвоить значение свойству или индексатору ""BasePlayer.serverInput"" — доступ только для чтения",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BasePlayerServerInputStringGetLine) }
            }, 
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Для нестатического поля, метода или свойства ""BaseMountable.TryFireProjectile\(StorageContainer, AmmoTypes, Vector3, Vector3, BasePlayer, float, float, out ServerProjectile\)""",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(BaseMountableTryFireProjectileStringGetLine) }
            },    
            new CompilationErrorConfigurationModel()
            {
                ErrorText = @"Ни одна из перегрузок метода ""GetGrade"" не принимает 3 аргументов",
                RequiresAnalysis = true,
                FixStrategies = new List<Type>() { typeof(GetGradeStringGetLine) }
            },  
        };
    }
}

