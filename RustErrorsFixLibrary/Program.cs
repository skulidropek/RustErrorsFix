﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RustErrorsFix.Roslyn.Factory;
using RustErrorsFixLibrary.Core;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.CodeFixStrategys;
using RustErrorsFixLibrary.Core.CodeFixStrategysGPTStringGetLine;
using RustErrorsFixLibrary.Core.CodeFixStrategysParametrStringGetLine;
using RustErrorsFixLibrary.Core.CodeFixStrategysString;
using RustErrorsFixLibrary.Core.CodeFixStrategysStringGetLine;
using RustErrorsFixLibrary.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFixLibrary
{
    public class Program
    {
        public static IServiceCollection CreateBuilder()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<CodeFixManager>()
                .AddTransient<CodeEditor>()
                .AddSingleton<CodeFixStrategyConfiguration>()
                .AddSingleton<AnalyzerConfiguration>()
                .AddScoped<CanMoveItemFactory>()
                .AddScoped<CrashCodeFactory>()
                .AddScoped<EquipWeaponFactory>()
                .AddScoped<BaseEntityExValid>()
                .AddScoped<FindContainerFactory>()
                .AddScoped<GetIdealContainerFactory>()
                .AddScoped<GetUlongFactory>()
                .AddScoped<ItemCraftTask>()
                .AddScoped<ListHashSetFactory>()
                .AddScoped<QuatrionIDFactory>()
                .AddScoped<ReplaceIDToIDValueFactory>()
                .AddScoped<ReplaceUidToUidValueFactory>()
                .AddScoped<ReplaceUInt32ToUInt64Factory>()
                .AddScoped<TargetDensityReplace>()
                .AddScoped<UInt64ToNetworkabledIdFactory>()
                .AddScoped<WaterLevelFactory>()
                .AddScoped<SpawnPopulationNotFound>()
                .AddScoped<GetTargetCount>()
                .AddScoped<SpawnPopulationFilter>()
                .AddScoped<DensitySpawnPopulation>()
                .AddScoped<ReplaceUlongToIDFactory>()
                .AddScoped<NetworkabledIdToUInt64Factory>()
                .AddScoped<ReplaceUintToUlong>()
                .AddScoped<UndregionFix>()
                .AddScoped<WaterLevelBoolToNull>()
                .AddScoped<WaterLevelNullToBool>()
                .AddScoped<HNTPlayerToScientistNPC>()
                .AddScoped<NPCMurdererToScarecrowNPC>()
                .AddScoped<InstanceToServerInstance>()
                .AddScoped<NetSv>()
                .AddScoped<UInt64NotEqualItemContainerId>()
                .AddScoped<ModularCar>()
                .AddScoped<ItemContainerOnlyAllowItem>()
                .AddScoped<DropChanceRecylerRemove>()
                .AddScoped<UpgradeFactory>()
                .AddScoped<RemoveApexUsing>()
                .AddScoped<ScientistToScientistNPC>()
                .AddScoped<FileSystemBackendCache>()
                .AddScoped<DecayEntityUInt64ToUInt32>()
                .AddScoped<AddUsingLinq>()
                .AddScoped<SandboxRemove>()
                .AddScoped<UlongToNetworkabledParametrStringGetLine>()
                .AddScoped<ItemIdToUlongStringGetLine>()
                .AddScoped<EqualsUlongToNetworkabledIdStringGetLine>()
                .AddScoped<UlongToItemContainerIdGetLine>()
                .AddScoped<UlongToItemIdGetLine>()
                .AddScoped<UlongToUintParametrStringGetLine>()
                .AddScoped<RemoveDropChanceStringGetLine>()
                .AddScoped<ResearchTableGetBlueprintTemplateStringGetLine>()
                .AddScoped<MiniCopterStringGetLine>()
                .AddScoped<PlayerInventoryFindItemUIDStringGetLine>()
                .AddScoped<PlayerInventoryFindItemsByItemIDStringGetLine>()
                .AddScoped<ItemContainerFindItemsByItemNameStringGetLine>()
                .AddScoped<ResearchTableScrapForResearchStringGetLine>()
                .AddScoped<PlayerInventoryFindItemIDStringGetLine>()
                .AddScoped<MLRSProjectileDistToGravityStringGetLine>()
                .AddScoped<BaseHelicopterVehicleToBaseHelicopterStringGetLine>()
                .AddScoped<BasePlayerIAmmoContainerStringGetLine>()
                .AddScoped<InvalidStatementStringGetLine>()
                .AddScoped<BaseHelicopterStringGetLine>()
                .AddScoped<PatrolHelicopterBaseHelicopterStringGetLine>()
                .AddScoped<BaseHelicopterPatrolHelicopterStringGetLine>()
                .AddScoped<SupplyDropParachuteStringGetLine>()
                .AddScoped<BaseProjectileMagazineStringGetLine>()
                .AddScoped<TestGPTStringGetLine>()
                .AddScoped<PatrolHelicopterVehicleToPatrolHelicopterStringGetLine>()
                .AddScoped<MinicopterWaterSampleStringGetLine>()
                .AddScoped<ItemCanMoveToStringGetLine>()
                .AddScoped<NetStringGetLine>()
                .AddScoped<MinicopterFieldStringGetLine>()
                .AddScoped<NetworkableIdIntUIntULongNotEqualsStringGetLine>()
                .AddScoped<PlayersNearbyJunkPileStringGetLine>()
                .AddScoped<TrainEnginedecayDurationStringGetLine>()
                .AddScoped<ulongValueStringGetLine>()
                .AddScoped<MotorRowboatdyingStringGetLine>()
                .AddScoped<AddNotTwoAgrumentsStringGetLine>()
                .AddScoped<ulongRemoveStringGetLine>()
                .AddScoped<ReplaceUintStringGetLine>()
                .AddScoped<KeyValuePairBaseNetworkableStringGetLine>()
                .AddScoped<ulongValueValueStringGetLine>()
                .AddScoped<EntityRefUidStringGetLine>()
                .AddScoped<ItemIdulongStringGetLine>()
                .AddScoped<ulongStringGetLine>()
                .AddScoped<ValueRemoveStringGetLine>()
                .AddScoped<ItemContainerIdToIntParametrStringGetLine>()
                .AddScoped<NetworkableIdItemIdItemContainerIdEqualsIntParametrStringGetLine>()
                .AddScoped<ValueValueRemoveStringGetLine>()
                .AddScoped<RelationshipManagerPlayerGangsStringGetLine>()
                .AddScoped<AddLinqStringGetLine>()
                .AddScoped<OnPlayerInitAnalyzer>()
                .AddScoped<OnCropGatherAnalyzer>()
                .AddScoped<OnCropGatherReplaceOnGrowableGatheredString>()
                .AddScoped<OnPlayerInitReplaceOnPlayerConnectedString>()
                .AddScoped<PlantEntityReplaceGrowableEntityStringGetLine>()
                .AddScoped<TriggerRadiationradiationSizeStringGetLine>()
                .AddScoped<BaseBoatmyRigidBodyStringGetLine>()
                .AddScoped<NPCPlayerApexReplaceBaseNpcStringGetLine>()
                .AddScoped<CanAffordUpgradeAnalyzer>()
                .AddScoped<CanAffordUpgradeReplaceStringGetLine>()
                .AddScoped<CanDeployItemReplaceStringGetLine>()
                .AddScoped<CanDeployItemAnalyzer>()
                .AddScoped<CanMoveItemReplaceStringGetLine>()
                .AddScoped<CanMoveItemAnalyzer>()
                .AddScoped<OnActiveItemChangeReplaceStringGetLine>()
                .AddScoped<OnActiveItemChangeAnalyzer>()
                .AddScoped<OnStructureUpgradeReplaceStringGetLine>()
                .AddScoped<OnStructureUpgradeAnalyzer>()
                .AddScoped<OnItemCraftCancelledAnalyzer>()
                .AddScoped<OnItemCraftCancelledReplaceStringGetLine>()
                .AddScoped<OnItemCraftFinishedAnalyzer>()
                .AddScoped<OnItemCraftFinishedReplaceStringGetLine>()
                .AddScoped<ListHashSetFindToFirtOrDefaultStringGetLine>()
                .AddScoped<BaseCarToBasicCarStringGetLine>()
                .AddScoped<ListHashSetToListParametrStringGetLine>()
                .AddScoped<ListLengthReplaceCountStringGetLine>()
                .AddScoped<ArrayResizeStringGetLine>()
                .AddScoped<NeedEndStringGetLine>()
                .AddScoped<stringNetworkableIdStringGetLine>()
                .AddScoped<ApprovedSkinInfoParametrStringGetLine>()
                .AddScoped<OnPlayerChatAnalyzer>()
                .AddScoped<ulongToStringParametrStringGetLine>()
                .AddScoped<ulongNetworkableIdParametrStringGetLine>()
                .AddScoped<ItemContainerBoolParametrStringGetLine>()
                .AddScoped<AutoTurretUpdateAimingParametrStringGetLine>()
                .AddScoped<BaseProjectileMagazineSwitchAmmoTypesIfNeededParametrStringGetLine>()
                .AddScoped<DerbisParametrStringGetLine>()
                .AddScoped<ItemContainerBaseEntityStringGetLine>()
                .AddScoped<BaseProjectileMagazineTryReloadStringGetLine>()
                .AddScoped<BaseRidableAnimalInventoryReloadStringGetLine>()
                .AddScoped<BasePlayerServerInputStringGetLine>()
                .AddScoped<BaseMountableTryFireProjectileStringGetLine>()
                .AddScoped<GetGradeStringGetLine>()
                ;

            return serviceProvider;
        }
    }
}
