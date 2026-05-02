using static ChaosScript;

namespace Chaos.Effects;

public class BigHeadEffect : ChaosComponent
{
    public BigHeadEffect()
    {
        Name = "BigHead Mode";
        LifeTime = 90;
    }
    public override void Start()
    {
        RPC.BigHeadMode();
    }
    public override void End()
    {
        RPC.BigHeadMode();
    }
}
