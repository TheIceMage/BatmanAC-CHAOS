using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class BoogalooEffect : ChaosComponent
{
    private float Timer = 0.1f, CurrentTimer = 0.1f;
    public BoogalooEffect()
    {
        Name = "Electric Boogaloo";
        LifeTime = 30;
    }
    public override void Update()
    {
        RPawnCombat.FDamageInfo dmginfo;
        dmginfo.DamageAmount = 0;
        dmginfo.Attacker = null;
        dmginfo.DamageType = RDmgType_Electricity.StaticClass();
        CurrentTimer += Game.GetDeltaTime();
        if (CurrentTimer >= Timer)
        {
            (Owner as RPawnPlayer).DamagedBy(dmginfo);
            CurrentTimer -= Timer;
        }
    }
}
