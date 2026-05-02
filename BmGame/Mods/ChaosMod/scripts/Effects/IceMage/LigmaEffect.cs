using BmSDK.BmGame;
using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class LigmaEffect : ChaosComponent
{
    RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
    public LigmaEffect()
    {
        Name = "LIGMA";
        LifeTime = 90;
    }
    public override void Start()
    {

        //NO SUPPORT FOR AKBANK; CAN'T OPTIMIZE SF
        Game.LoadPackage("Under_C2_Ch4");
        var act = new RSeqAct_SetPoisonLevel(Game.GetWorldInfo());
        act.PoisonLevel = RPlayerController.EBatmanPoisonLevel.BPL_PoisonMaximum;
        act.Activated();
        var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Batman_Level_Moves.Under_C2.1stPersonCollapse");
        Loc.Location = RPP.Location;
        Loc.Normal = ChaosScript.RotationDirection(RPP.Rotation) * -1;
        RMC.TriggerSpecialMove(RPC, Loc, true);
    }
    public override void Update()
    {

        //if (CurrentTime >= Timer)
        //{
        //    var act = new RSeqAct_BatmanCough(Game.GetWorldInfo());
        //    act.RPC = Game.GetPlayerController(0);
        //    act.Activated();
        //    CurrentTime -= Timer;
        //}
        RPC.Stunned();
    }
    public override void End()
    {
        Game.LoadPackage("Under_C2_Ch4");
        var act = new RSeqAct_SetPoisonLevel(Game.GetWorldInfo());
        act.PoisonLevel = RPlayerController.EBatmanPoisonLevel.BPL_NoPoison;
        act.Activated();
    }

}
