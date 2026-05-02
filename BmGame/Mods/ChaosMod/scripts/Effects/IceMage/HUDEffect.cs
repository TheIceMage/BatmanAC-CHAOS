using static ChaosScript;

namespace Chaos.Effects;

public class HUDEffect : ChaosComponent
{
    public HUDEffect()
    {
        Name = "No HUD";
        LifeTime = 90;
    }
    public override void Start()
    {

        Game.GetConsole().ConsoleCommand("ToggleHUD");
    }
    public override void End()
    {

        Game.GetConsole().ConsoleCommand("ToggleHUD");
    }
}
