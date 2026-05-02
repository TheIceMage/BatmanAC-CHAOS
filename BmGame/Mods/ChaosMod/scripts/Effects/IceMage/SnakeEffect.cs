using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class SnakeEffect : ChaosComponent
{
    public SnakeEffect()
    {
        Name = "When you can't even say my name";
        bRetriggerable = true;
        LifeTime = 90;
    }
    public override void Start()
    {
        foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
        {
            if (pion.IsValid)
            {
                if (!pion.bHidden)
                {
                    pion.SetHidden(true);
                }
            }
        }
    }
    public override void End()
    {
        foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
        {
            if (pion.IsValid)
            {
                pion.SetHidden(false);
            }
        }
    }
}
