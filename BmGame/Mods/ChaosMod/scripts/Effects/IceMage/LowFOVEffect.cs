using static ChaosScript;

namespace Chaos.Effects;

public class LowFOVEffect : ChaosComponent
{
    public LowFOVEffect()
    {
        Name = "Low FoV";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Update()
    {

        RPC.PlayerCamera.SetFOV(15);
    }
    public override void End()
    {
        RPC.PlayerCamera.SetFOV(RPC.PlayerCamera.DefaultFOV);
    }
}
