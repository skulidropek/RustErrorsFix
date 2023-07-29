using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RustErrorsFix.Legasy
{
    internal class NpcFixError : IErrorFixer
    {
        public string Fix(string plugin)
        {
            plugin = plugin.Replace("Assets/Prefabs/NPC/Scientist/ScientistStationary.prefab", "assets/rust.ai/agents/npcplayer/humannpc/scientist/scientistnpc_heavy.prefab")
                .Replace("NPCPlayerApex", "ScientistNPC")
                .Replace("scanRadius", "mostRecentTargetType.scanRadius")
                .Replace("mostRecentTargetType.mostRecentTargetType", "mostRecentTargetType")
                .Replace("PlantEntity", "GrowableEntity")
                .Replace("OnPlayerInit", "OnPlayerConnected")
                .Replace("OnCropGather", "OnGrowableGathered")
                .Replace("BaseCar", "BasicCar")
                ;


            return plugin;
        }
    }
}
