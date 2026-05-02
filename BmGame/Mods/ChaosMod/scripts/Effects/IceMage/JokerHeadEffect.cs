using BmSDK.Engine;
using System.Numerics;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class JokerHeadEffect : ChaosComponent
{
    SkeletalMeshComponent SK;
    public JokerHeadEffect()
    {
        Name = "Me, stuck deep inside you.";
        LifeTime = 90;
    }

    public override void Start()
    {
        Game.LoadPackage("GCPD_A1_Ch5");
        var depthtest = new SkeletalMeshComponent.FDepthBiasData
        {
            DepthBias = -255,
            AlternateDepthBias = -255,
            DepthBiasCalculationType = SkeletalMeshComponent.EDepthBiasCalculationType.DEPTHBIASCALCULATIONTYPE_Constant,
            DepthBiasApplicationType = SkeletalMeshComponent.EDepthBiasApplicationType.DEPTHBIASAPPLICATIONTYPE_World,
            DepthBiasMinDistanceFromCameraPlaneOverride = 0,
            MinDepthBiasMultiplier = 1
        };
        SK = new SkeletalMeshComponent(RPP);
        SK.SetLightEnvironment(RPP.Mesh.LightEnvironment);
        SK.SetLightingChannels(RPP.Mesh.LightingChannels);
        SK.SetSkeletalMesh(Game.FindObject<SkeletalMesh>("MrFreeze.Mesh.Joker_HeadFreezeBoss"));
        SK.SetDepthBias(depthtest);
        RPP.Mesh.AttachComponent(SK, "Bip01_Head", new Vector3(-20, 0, 0));
        RPC.RadioStart(Game.FindObject<RDialogueLine>("WwSpch-GCPD.Joker_laugh.Joker_laugh_02"), true);
    }

    public override void End()
    {
        RPP.Mesh.DetachComponent(SK);
    }
}