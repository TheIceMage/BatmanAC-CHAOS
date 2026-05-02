using static ChaosScript;

namespace Chaos.Effects;

public class QuickFireEffect : ChaosComponent
{
    public QuickFireEffect()
    {
        Name = "QuickFire";
        LifeTime = 90;
    }
    public override void Update()
    {
        RPC.QuickFireBatarang();
    }
}
