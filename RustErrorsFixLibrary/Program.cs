using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RustErrorsFix.Roslyn.Factory;
using RustErrorsFixLibrary.Core;
using RustErrorsFixLibrary.Core.Abstract;
using RustErrorsFixLibrary.Core.CodeFixStrategys;
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
                ;

            return serviceProvider;
        }
    }
}
