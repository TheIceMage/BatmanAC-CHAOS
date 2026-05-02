using static ChaosScript;

namespace Chaos.Effects;

public class HighFOVEffect : ChaosComponent
{
    public HighFOVEffect()
    {
        Name = "High FoV";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Update()
    {
        RPC.PlayerCamera.SetFOV(170);
    }
    public override void End()
    {
        RPC.PlayerCamera.SetFOV(RPC.PlayerCamera.DefaultFOV);
    }
}
