using static ChaosScript;

namespace Chaos.Effects;

public class SlowMoEffect : ChaosComponent
{
    public SlowMoEffect()
    {
        Name = "SlowMo";
        LifeTime = 30;
    }
    public override void Start()
    {
        Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed / 4);
    }
    public override void End()
    {

        Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed * 4);
    }
}
