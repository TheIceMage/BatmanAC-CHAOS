using static ChaosScript;

namespace Chaos.Effects;

public class InvScaleEffect : ChaosComponent
{
    public InvScaleEffect()
    {
        Name = "Inverted";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale * -1).ToString());
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale * -1).ToString());
    }
}
