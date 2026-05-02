using static ChaosScript;

namespace Chaos.Effects;

public class FastMoEffect : ChaosComponent
{
    public FastMoEffect()
    {
        LifeTime = 30;
        Name = "FastMo";
    }
    public override void Start()
    {
        Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed * 4);
    }
    public override void End()
    {

        Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed / 4);
    }
}
