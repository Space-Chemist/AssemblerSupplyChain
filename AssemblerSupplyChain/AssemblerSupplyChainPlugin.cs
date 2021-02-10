using Torch;
using Torch.API;

namespace AssemblerSupplyChain
{
    public class AssemblerSupplyChainPlugin
    {
        public class NoIdlePlugin : TorchPluginBase
        {
            public override void Init(ITorchBase torch)
            {
                base.Init(torch);
            }
        }
    }
}