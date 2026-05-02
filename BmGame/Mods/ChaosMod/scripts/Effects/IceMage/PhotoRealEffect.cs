using static ChaosScript;

namespace Chaos.Effects;

public class PhotoRealEffect : ChaosComponent
{
    public PhotoRealEffect()
    {
        Name = "Photorealistic ReShade";
        LifeTime = 90;
    }
    public override void Start()
    {
        Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier /= 10;
        Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier /= 10;
        Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier *= 10;
    }
    public override void End()
    {

        Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier *= 10;
        Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier *= 10;
        Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier /= 10;
    }
}
