using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class WarpPawnsEffect : ChaosComponent
{
    public WarpPawnsEffect()
    {
        Name = "Everyone teleports to you";
        LifeTime = 0;
    }
    public override void Start()
    {
        foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
        {
            if (pion != Owner && pion.IsValid)
            {
                pion.SetLocationIgnoringCollision(Owner.Location);
            }
        }
    }
}
