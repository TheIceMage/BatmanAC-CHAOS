using static ChaosScript;

namespace Chaos.Effects;

public class NoCrouchEffect : ChaosComponent
{
    public NoCrouchEffect()
    {
        Name = "No Crouch";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {
        RPP.bCanCrouch = false;
    }
    public override void End()
    {
        RPP.bCanCrouch = true;

    }
}
