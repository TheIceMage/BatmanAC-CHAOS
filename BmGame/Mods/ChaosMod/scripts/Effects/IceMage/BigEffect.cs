using static ChaosScript;

namespace Chaos.Effects;

public class BigEffect : ChaosComponent
{
    public BigEffect()
    {
        Name = "BIG";
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale * 4).ToString());
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale / 4).ToString());
    }
}
