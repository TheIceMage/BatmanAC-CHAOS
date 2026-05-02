using static ChaosScript;

namespace Chaos.Effects;

public class GodEffect : ChaosComponent
{
    public GodEffect()
    {
        Name = "Invincibility";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetPlayerController(0).bGodMode = true;

    }
    public override void End()
    {

        Game.GetPlayerController(0).bGodMode = false;

    }
}
