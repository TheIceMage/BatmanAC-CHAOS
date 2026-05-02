using BmSDK;
using BmSDK.BmGame;
using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects;

public class GunsEffect : ChaosComponent
{
    public GunsEffect()
    {
        Name = "Now who's left all those dangerous-looking weapons there!";
        LifeTime = 0;
    }
    public override void Start()
    {

        Game.LoadPackage("StreamedWeapon_CombatRifle_SF");
        foreach (var pion in GameObject.FindObjectsSlow<RBMPawnAI>())
        {
            if (pion.IsValid && pion != Owner)
            {
                //var weapon = (RBMWeaponCombatRifle)pion.CreateInventory(RBMWeaponCombatRifle.StaticClass());
                //pion.ChangeWeapon(weapon, weapon, true);
                pion.SpawnWeapon(RBMWeaponCombatRifle.StaticClass(), true);
            }
        }
        ;
    }
}
