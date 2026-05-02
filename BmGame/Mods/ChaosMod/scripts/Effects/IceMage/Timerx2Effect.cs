using static ChaosScript;

namespace Chaos.Effects;

public class Timerx2Effect : ChaosComponent
{
    public Timerx2Effect()
    {
        Name = "Timer x2";
        LifeTime = 90;
    }
    public override void Start()
    {
        MainScript.Timer *= 2;
    }
    public override void End()
    {
        MainScript.Timer /= 2;
    }
}
