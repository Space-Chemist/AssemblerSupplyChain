using System.Runtime.InteropServices;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Torch;
using Torch.API;
using Torch.Session;
using VRage.Network;
using VRage.Sync;

namespace AssemblerSupplyChain
{
    public class AssemblerSupplyChainPlugin : TorchPluginBase 
    {
        public MyInventoryConstraint _inventoryConstraint = new MyInventoryConstraint(string.Empty);
        public static AssemblerSupplyChainPlugin Instance { get; private set; }
        public readonly int MAX_ITEMS_TO_PULL_IN_ONE_TICK = 10;
        public static TorchSessionManager SessionManager;
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);
        }
    }
}