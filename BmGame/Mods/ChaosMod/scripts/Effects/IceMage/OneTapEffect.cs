using static ChaosScript;

namespace Chaos.Effects;

public class OneTapEffect : ChaosComponent
{
    public OneTapEffect()
    {
        Name = "OneTap";
        LifeTime = 0;
    }
    public override void Start()
    {
        RPP.DebugRemoveArmour();
        RPP.Health = 1;
    }
}
