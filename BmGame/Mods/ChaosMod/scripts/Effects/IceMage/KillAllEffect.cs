using static ChaosScript;

namespace Chaos.Effects;

public class KillAllEffect : ChaosComponent
{
    public KillAllEffect()
    {
        Name = "Omae wa mou shindeiru";
        LifeTime = 0;
    }
    public override void Start()
    {
        Game.GetCheatManager().DebugKillAllEnemies();
    }
}
