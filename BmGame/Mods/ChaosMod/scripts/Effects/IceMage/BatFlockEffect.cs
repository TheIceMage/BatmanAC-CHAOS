using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects;

public class BatFlockEffect : ChaosComponent
{

    public BatFlockEffect()
    {
        Name = "Bats";
        LifeTime = 90;
    }
    public override void Update()
    {
        //var tiara = new TArray<Actor>(1);
        //var act = new RSeqAct_BatFlock(Game.GetWorldInfo());
        //tiara.Add(Owner as Actor);
        //act.AttractorList = tiara;
        //act.SpawnNum = 100;
        //act.Activated();

        var act = new RSeqAct_BatSpawner(Game.GetWorldInfo());
        act.StartActor = Owner as Actor;
        act.EndActor = Owner as Actor;
        act.SpawnNum = 1;
        act.Activated();

    }
}
