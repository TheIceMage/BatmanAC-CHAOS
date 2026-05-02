using BmSDK;
using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class ParentsEffect : ChaosComponent
{
    private Package UPK;
    RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
    public ParentsEffect()
    {
        Name = "I've fallen, and I can't get up!";
        LifeTime = 15;

        //ChaosScript.cs: FVector { X = 5840, Y = -5232, Z = 3640.1548 }
        //ChaosScript.cs: FVector { X = 6031.7007, Y = -5272.888, Z = 3757.4639 }

    }
    public override void Start()
    {
        UPK = Game.LoadPackage("Under_S1_Ch4b");
        //UPK.AddToRoot();
        var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Anim_Level_Batman.SpecialMoves.UnderworldS1_ND_SeeParents");
        Loc.Location = RPP.Location with { Z = RPP.Location.Z - 85 };
        Loc.Normal = ChaosScript.RotationDirection(RPP.Rotation) * -1;
        RMC.TriggerSpecialMove(RPC, Loc, true);
    }
    public override void End()
    {
        //if (!bStart) { Debug.Log("Parents crash avoided"); return; }
        //var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Anim_Level_Batman.SpecialMoves.UnderworldS1_ND_Out");
        //UPK.RemoveFromRoot();
        //RMC.TriggerSpecialMove(RPC, Loc, true);
        Game.GetCheatManager().Fly();
        Game.GetCheatManager().Walk();
    }
}
