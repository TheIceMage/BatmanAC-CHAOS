using static ChaosScript;

namespace Chaos.Effects;

public class SmollEffect : ChaosComponent
{
    public SmollEffect()
    {
        Name = "smoll";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (RPP.DrawScale / 4).ToString());
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (RPP.DrawScale * 4).ToString());
    }
}
