using static ChaosScript;

namespace Chaos.Effects;

public class ExtraGEffect : ChaosComponent
{
    public ExtraGEffect()
    {
        Name = "Intense Gravity";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * 1000);
    }
    public override void End()
    {

        Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() / 1000);
    }
}
