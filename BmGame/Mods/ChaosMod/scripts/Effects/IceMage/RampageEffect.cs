using BmSDK;
using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class RampageEffect : ChaosComponent
{
    private ParticleSystemComponent FistLPSC;
    private ParticleSystemComponent FistRPSC;
    private ParticleSystemComponent TrailLPSC;
    private ParticleSystemComponent TrailRPSC;
    public RampageEffect()
    {
        Name = "B.A.T. Mode";
        LifeTime = 90;
    }
    public override void Start()
    {
        FistLPSC = new ParticleSystemComponent(RPC);
        FistRPSC = new ParticleSystemComponent(RPC);
        TrailLPSC = new ParticleSystemComponent(RPC);
        TrailRPSC = new ParticleSystemComponent(RPC);
        (Owner as RPawnPlayer).bIncreasedCombatPower = true;
        //SO MANY FUCKING DEPENDENCIES!
        Game.LoadPackage("Museum_S2_Ch3c");
        Game.LoadPackage("Under_S3_Ch4");
        var FistPS = Game.FindObject<ParticleSystem>("nv_Museum.Particles.ElectricCore");
        var TrailPS = Game.FindObject<ParticleSystem>("BFX_RazAlGul.Particles.ArcaneShuriken");
        ParticleAttachRPP(FistLPSC, "Bip01_L_Hand", FistPS);
        ParticleAttachRPP(FistRPSC, "Bip01_R_Hand", FistPS);
        ParticleAttachRPP(TrailLPSC, "Bip01_L_Hand", TrailPS);
        ParticleAttachRPP(TrailRPSC, "Bip01_R_Hand", TrailPS);
        FistLPSC.SetScale(0.1f);
        FistRPSC.SetScale(0.1f);
        TrailLPSC.SetScale(0.1f);
        TrailRPSC.SetScale(0.1f);
    }
    private void ParticleAttachRPP(ParticleSystemComponent PSC, FName Bone, ParticleSystem PS)
    {
        (Owner as RPawn).Mesh.AttachComponent(PSC, Bone);
        PSC.SetTemplate(PS);
        PSC.ActivateSystem();

    }
    public override void End()
    {

        (Owner as RPawnPlayer).bIncreasedCombatPower = true;
        if (FistLPSC != null && FistRPSC != null && TrailLPSC != null && TrailRPSC != null)
        {
            FistLPSC.DeactivateSystem();
            FistRPSC.DeactivateSystem();
            TrailLPSC.DeactivateSystem();
            TrailRPSC.DeactivateSystem();
        }
    }
}