using BmSDK.BmGame;
using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class WalkEffect : ChaosComponent
{
    RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
    public WalkEffect()
    {
        Name = "I'm gonna walk 5000 miles!";
        LifeTime = 30;
    }
    public override void Start()
    {
        Game.LoadPackage("Under_S1_Ch4b");
        var RMC = Game.FindObject<RSpecialMoveConfig_CustomWalk>("Batman_Level_Moves.Sick.SickWalk");
        Loc.Location = RPP.Location;
        Loc.Normal = ChaosScript.RotationDirection(RPP.Rotation) * -1;
        Game.GetCheatManager().Walk();
        RMC.TriggerSpecialMove(RPC, Loc, true);
    }
    public override void End()
    {
        if (RPP.CurrentSpecialMove != null)
        {
            RPP.CurrentSpecialMove.FinishSpecialMove();
        }
        RPP.BeginChangePose();
        RPP.ChangePose("Standing", "Relaxed");
        RPP.EndChangePose();
    }
}
