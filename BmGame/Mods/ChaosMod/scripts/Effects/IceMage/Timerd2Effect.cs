using static ChaosScript;

namespace Chaos.Effects;

public class Timerd2Effect : ChaosComponent
{
    public Timerd2Effect()
    {
        Name = "Timer /2";
        LifeTime = 90;
    }
    public override void Start()
    {
        MainScript.Timer /= 2;
    }
    public override void End()
    {
        MainScript.Timer *= 2;
    }
}
