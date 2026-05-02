using static ChaosScript;

namespace Chaos.Effects;

public class StickBiasEffect : ChaosComponent
{
    public StickBiasEffect()
    {
        Name = "Stick Drift";
        LifeTime = 90;
    }
    public override void Update()
    {

        Game.GetPlayerController(0).PlayerInput.aStrafe += 0.5f;
        Game.GetPlayerController(0).PlayerInput.aBaseY += 0.5f;
    }
}
