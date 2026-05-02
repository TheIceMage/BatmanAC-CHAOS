using static ChaosScript;

namespace Chaos.Effects;

public class FullOnAttackEffect : ChaosComponent
{
    public FullOnAttackEffect()
    {
        Name = "Relentless Enemies";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPawnCombat bFullOnAttack true");
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set BmGame.RPawnCombat bFullOnAttack false");
    }
}
