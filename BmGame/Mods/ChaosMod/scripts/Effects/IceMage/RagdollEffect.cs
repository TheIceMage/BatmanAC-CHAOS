using static ChaosScript;

namespace Chaos.Effects;

public class RagdollEffect : ChaosComponent
{
    public RagdollEffect()
    {
        Name = "Ragdoll";
        LifeTime = 30;
    }
    public override void Start()
    {

        Game.GetCheatManager().BmRagDoll();
    }
    public override void End()
    {

        Game.GetCheatManager().BmRagDoll();
    }
}
