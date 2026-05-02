using static ChaosScript;

namespace Chaos.Effects;

public class MenuPPEffect : ChaosComponent
{
    public MenuPPEffect()
    {
        Name = "Menu Filter";
        LifeTime = 90;
    }
    public override void Update()
    {

        RPC.TurnOnMenuPP();
    }
    public override void End()
    {

        RPC.TurnOffMenuPP();
    }
}
