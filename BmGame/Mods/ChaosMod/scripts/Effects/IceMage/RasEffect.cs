using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class RasEffect : ChaosComponent
{
    RSeqAct_RasBossLogic test = null;
    public RasEffect()
    {
        Name = "Lag Ninja";
        LifeTime = 15;
    }
    public override void Start()
    {

        Game.LoadPackage("Under_S3_Ch4");
        test = new RSeqAct_RasBossLogic(Game.GetWorldInfo());
        test.Activated();
        test.DoSpawn(Owner, false);

    }
    public override void Update()
    {

        test.DuplicateRas(false);
    }
    public override void End()
    {

    }
}
