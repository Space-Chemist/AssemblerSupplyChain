using System;
using System.Reflection;
using NLog;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.Entities.Inventory;
using Sandbox.Game.Weapons;
using Torch.Managers.PatchManager;
using VRage.Game.Entity;
using VRage.Game.ModAPI;

namespace AssemblerSupplyChain
{
    public class Patch
    {
        [PatchShim]
        public static class MyAssemblerBasePatch {

            public static readonly Logger Log = LogManager.GetCurrentClassLogger();

            internal static readonly MethodInfo UpdateMethod =
                typeof(MyAssembler).GetMethod(nameof(MyAssembler.UpdateBeforeSimulation100), BindingFlags.Instance | BindingFlags.Public) ??
                throw new Exception("Failed to find patch method");

            internal static readonly MethodInfo IdleSupplyPatch =
                typeof(MyAssemblerBasePatch).GetMethod(nameof(IdleSupply), BindingFlags.Static | BindingFlags.Public) ??
                throw new Exception("Failed to find patch method");

            public static void Patch(PatchContext ctx) {

                ctx.GetPattern(UpdateMethod).Prefixes.Add(IdleSupplyPatch);

                Log.Debug("Patching Successful Assembler!");
            }

            public static void IdleSupply(MyAssembler __instance) {
          
                if (!(__instance is MyAssembler assembler))
                    return;

                if (assembler.IsQueueEmpty)
                {
                    var amount = assembler.InventoryCount;
                    remove => this.CurrentStateChanged -= this.GetDelegate(value);
                }

                return;
            }
        }
    }
}