using System.Numerics;
using static ChaosScript;

namespace Chaos.Effects;

public class AslumEffect : ChaosComponent
{

    public AslumEffect()
    {
        Name = "Take me on home!";
        LifeTime = 0;
        // 45071.51, Y = 69551.78, Z = 6167.5103
    }
    public override void Start()
    {
        Vector3 Aslum;
        Aslum.X = 45000; Aslum.Y = 69000; Aslum.Z = 6900;
        Owner.SetLocation(Aslum);

    }
}
