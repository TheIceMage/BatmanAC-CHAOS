using static ChaosScript;

namespace Chaos.Effects;

public class TeleportEffect : ChaosComponent
{
    public TeleportEffect()
    {
        Name = "Teleport";
        LifeTime = 0;
    }
    public override void Start()
    {
        Game.GetCheatManager().Teleport();
    }
}
