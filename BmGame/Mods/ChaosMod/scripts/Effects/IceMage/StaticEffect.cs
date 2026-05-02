using static ChaosScript;

namespace Chaos.Effects;

public class StaticEffect : ChaosComponent
{
    public StaticEffect()
    {
        Name = "FREEZE";
        LifeTime = 30;
        bRetriggerable = true;
    }
    public override void Start()
    {
        Owner.bStatic = true;
    }
    public override void End()
    {
        Owner.bStatic = false;
    }
}
