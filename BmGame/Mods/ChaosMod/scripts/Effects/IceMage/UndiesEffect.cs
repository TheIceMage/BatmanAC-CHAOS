using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects;

public class UndiesEffect : ChaosComponent
{
    private SkeletalMesh SK;
    public UndiesEffect()
    {
        Name = "Honey, where are my paaaaaanntss!?";
        LifeTime = 90;
    }
    public override void Start()
    {
        SK = RPP.SecondaryMesh.SkeletalMesh;
        Game.LoadPackage("CHAOS_Undies_SF");
        var Undies = Game.FindObject<SkeletalMesh>("IceMage.Mesh.Batman_Undies_Skin");
        (Owner as RPawn).SecondaryMesh.SetSkeletalMesh(Undies);
    }
    public override void End()
    {

        (Owner as RPawn).SecondaryMesh.SetSkeletalMesh(SK);
    }
}
