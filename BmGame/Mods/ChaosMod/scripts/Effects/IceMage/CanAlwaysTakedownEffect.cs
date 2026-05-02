using static ChaosScript;

namespace Chaos.Effects;

public class CanAlwaysTakedownEffect : ChaosComponent
{
    public CanAlwaysTakedownEffect()
    {
        Name = "Unlimited Takedowns";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData Debug_Combat_CanAlwaysTakedown true");
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData Debug_Combat_CanAlwaysTakedown false");
    }
}
