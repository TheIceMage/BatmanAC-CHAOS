using static ChaosScript;

namespace Chaos.Effects;

public class WireframeEffect : ChaosComponent
{
    public WireframeEffect()
    {
        Name = "Wireframe";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("set Engine.Material Wireframe true");
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("set Engine.Material Wireframe false");
    }
}
