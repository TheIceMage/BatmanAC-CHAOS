using static ChaosScript;

namespace Chaos.Effects;

public class RemoveColorsEffect : ChaosComponent
{
    public RemoveColorsEffect()
    {
        Name = "Removed Colors";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetGameViewportClient().bOverrideDiffuseAndSpecular = true;
    }
    public override void End()
    {

        Game.GetGameViewportClient().bOverrideDiffuseAndSpecular = false;
    }
}
