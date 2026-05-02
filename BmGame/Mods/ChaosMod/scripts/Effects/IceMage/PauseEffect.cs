using static ChaosScript;

namespace Chaos.Effects;

public class PauseEffect : ChaosComponent
{
    public PauseEffect()
    {
        Name = "Pause";
        LifeTime = 0;
    }
    public override void Start()
    {
        RPC.Pause();
    }
}
