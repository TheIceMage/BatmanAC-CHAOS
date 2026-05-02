using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class CollectRiddlerTrophiesEffect : ChaosComponent
{
    public CollectRiddlerTrophiesEffect()
    {
        Name = "Collect Nearest Riddler Trophies";
        LifeTime = 0;
    }
    public override void Start()
    {
        foreach (var pickup in GameObject.FindObjectsSlow<RPickup_Riddler>())
        {
            if (pickup.IsValid && !pickup.IsClassDefaultObject)
            {
                pickup.PickedUp(RPC);
            }
        }

    }
}
