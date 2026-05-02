using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class SpinEffect : ChaosComponent
{
    private bool bDisabled = false;
    public SpinEffect()
    {
        Name = "Like a record baby!";
        LifeTime = 60;
    }
    public override void Start()
    {
        foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
        {
            if (pion != Owner && pion.IsValid && !pion.IsClassDefaultObject)
            {
                var s = new SpinEffect();
                s.bStart = false;
                pion.AttachScriptComponent(s);
            }
        }
    }
    public override void OnTick()
    {
        Update();
    }
    public override void Update()
    {
        if (!bDisabled)
        {
            if (!Owner.IsValid)
            {
                Debug.Log(Name + " - Prevented crash with " + (LifeTime - CurrentTime).ToString() + " s left");
                return;
            }
            CurrentTime += Game.GetDeltaTime();
            if (CurrentTime >= LifeTime - 0.1)
            {
                bDisabled = true;
                Debug.Log(Name + "Disabled on " + Owner.Name);
            }
            Owner.Rotation = Owner.Rotation with
            {
                Yaw = (int)MathF.Round(Owner.Rotation.Yaw + (690420/6.28f * Game.GetDeltaTime()))
            };
        }
    }
}
