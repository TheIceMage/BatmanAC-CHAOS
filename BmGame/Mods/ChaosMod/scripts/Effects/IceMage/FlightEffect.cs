using static ChaosScript;

namespace Chaos.Effects;

public class FlightEffect : ChaosComponent
{
    float h = 69420;
    public FlightEffect()
    {
        Name = "Flight";
        LifeTime = 30;
    }
    public override void Start()
    {

        Game.GetCheatManager().Fly();
    }
    public override void Update()
    {
        if (RPP.Health != h)
        {
            h = RPP.Health;
            Game.GetCheatManager().Fly();
        }
    }
    public override void End()
    {

        Game.GetCheatManager().Walk();
    }
}
