using static ChaosScript;

namespace Chaos.Effects;

public abstract class AllGodEffect : ChaosComponent
{
    public AllGodEffect()
    {
        Name = "Everyone's Invincible";
        bRetriggerable = true;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData bAIInvulnerable true");

    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData bAIInvulnerable false");

    }
}
