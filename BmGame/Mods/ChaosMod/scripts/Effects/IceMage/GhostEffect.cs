using static ChaosScript;

namespace Chaos.Effects;

public class GhostEffect : ChaosComponent
{
    float h = 69420;
    public GhostEffect()
    {
        Name = "Ghost";
        LifeTime = 30;
    }
    public override void Start()
    {
        Game.GetCheatManager().Ghost();
    }

    public override void Update()
    {
        if (RPP.Health != h)
        {
            h = RPP.Health;
            Game.GetCheatManager().Ghost();
        }
    }

    public override void End()
    {

        Game.GetCheatManager().Walk();
    }
}
