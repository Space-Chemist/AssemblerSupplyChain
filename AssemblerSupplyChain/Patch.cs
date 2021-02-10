using System;
using System.Linq;
using System.Reflection;
using NLog;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.Entities.Inventory;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GameSystems.Conveyors;
using Sandbox.Game.Weapons;
using Torch.Managers.PatchManager;
using VRage;
using VRage.Game;
using VRage.Game.Entity;
using VRage.Game.ModAPI;
using VRage.Sync;
using IMyInventoryItem = VRage.Game.ModAPI.Ingame.IMyInventoryItem;

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

                if (assembler.IsQueueEmpty && assembler.HasInventory)
                {
                    //figure out how to see if assembler is even connected to conveyors cause fuck me 
                    if (//assembler.fuckme)
                    {
                        try
                        {
                            
                            var group =assembler.CubeGrid.BigOwners;
                            var me =group.First();
                            MyInventory inventory = assembler.GetInventory();
                            if ((double) inventory.VolumeFillFactor >= 0.990000009536743)
                                return;
                            MyGridConveyorSystem.PullAllItemsForConnector((IMyConveyorEndpointBlock) assembler, inventory, me, AssemblerSupplyChainPlugin.Instance.MAX_ITEMS_TO_PULL_IN_ONE_TICK);

                        }
                        catch (Exception e)
                        {
                            Log.Fatal("fuck my whole life" + e);
                        }
                    }    
                    
                    
                    /*MyInventory inventory = assembler.GetInventory();
                    if (inventory.GetItemsCount() <= 0)
                        return;
                    var blocks = assembler.CubeGrid.GridSystems.ConveyorSystem.
                    assembler.CubeGrid.GridSystems.ConveyorSystem.PullItems(_inventoryConstraint, MyFixedPoint.MaxValue, blocks, inventory );*/

                    

                    // this probably just yeets the contents of the assembler
                    //assembler.Components.Remove<MyInventory>();


                    /*MyGridConveyorSystem.PushAnyRequest((inventory) this, inventory);
                    if (assembler.GetInventory() == null)
                    {
                        MyInventory myInventory = new MyInventory(assembler.InventorySize.Volume, this.m_conveyorSorterDefinition.InventorySize, MyInventoryFlags.CanSend);
                        assembler.Components.Add<MyInventoryBase>((MyInventoryBase) myInventory);
                        myInventory.Init(builderConveyorSorter.Inventory);
                    }
                    var a = assembler.OutputInventory.InventoryId;
                    var i = assembler.ConveyorEndpoint.CubeBlock.InventoryCount;
                    assembler.Components.Clear();
                    assembler.drainall();
                    MyConveyorSorter sorter;
                    sorter.DrainAll = true;*/
                }

                return;
            }
        }
    }
}