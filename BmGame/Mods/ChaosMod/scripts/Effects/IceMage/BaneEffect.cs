using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class BaneEffect : ChaosComponent
{
    public BaneEffect()
    {
        Name = "Surprised to see me, Batman?";
        LifeTime = 0;
    }
    public override void Start()
    {
        Game.LoadPackage("BaneSS_B1");
        SpawnCharacters<RPawnVillainBane, RCharacter_Bane>(4);
    }
}
