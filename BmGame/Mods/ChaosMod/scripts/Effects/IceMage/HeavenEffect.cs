using static ChaosScript;

namespace Chaos.Effects;

public class HeavenEffect : ChaosComponent
{
    public HeavenEffect()
    {
        Name = "Heaven";
        LifeTime = 0;
    }
    public override void Start()
    {

        RPP.SetLocation(Owner.Location with { Z = 69420 });
    }
}
