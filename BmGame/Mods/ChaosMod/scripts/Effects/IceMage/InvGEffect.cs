using static ChaosScript;

namespace Chaos.Effects;

public class InvGEffect : ChaosComponent
{
    public InvGEffect()
    {
        Name = "Inverted Gravity";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * -1);
    }
    public override void End()
    {

        Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * -1);
    }
}
