using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class RiddlerTrophiesEffect : ChaosComponent
{
    public RiddlerTrophiesEffect()
    {
        Name = "Nearest Riddle Trophy";
        LifeTime = 0;
    }
    public override void Start()
    {
        RPickup_Riddler riddler = null;
        foreach (var pickup in GameObject.FindObjectsSlow<RPickup_Riddler>())
        {
            if (pickup.IsValid && !pickup.IsClassDefaultObject)
            {
                if (
                    riddler == null ||
                    ChaosScript.VectorLength(Owner.Location - pickup.Location) <
                    ChaosScript.VectorLength(Owner.Location - riddler.Location)
                    )
                    riddler = pickup;
            }
        }
        if (riddler != null) Owner.Location = riddler.Location;

    }
}
