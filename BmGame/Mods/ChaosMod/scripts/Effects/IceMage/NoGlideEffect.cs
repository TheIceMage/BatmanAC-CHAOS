using static ChaosScript;

namespace Chaos.Effects;

public class NoGlideEffect : ChaosComponent
{
    public NoGlideEffect()
    {
        Name = "No Glide";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {
        RPP.bCanCapeGlide = false;
    }
    public override void End()
    {
        RPP.bCanCapeGlide = true;
    }
}
