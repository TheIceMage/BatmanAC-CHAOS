using BmSDK;
using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects;

public class SkeletonEffect : ChaosComponent
{
    SkeletalMeshComponent SK;
    public SkeletonEffect()
    {
        Name = "Spooky Scary";
        LifeTime = 90;
    }

    public override void Start()
    {
        Game.LoadPackage("CHAOS_Skeletons_SF");
        var depthtest = new SkeletalMeshComponent.FDepthBiasData
        {
            DepthBias = -255,
            AlternateDepthBias = -255,
            DepthBiasCalculationType = SkeletalMeshComponent.EDepthBiasCalculationType.DEPTHBIASCALCULATIONTYPE_Constant,
            DepthBiasApplicationType = SkeletalMeshComponent.EDepthBiasApplicationType.DEPTHBIASAPPLICATIONTYPE_Screen,
            DepthBiasMinDistanceFromCameraPlaneOverride = 0,
            MinDepthBiasMultiplier = 1
        };
        SK = new SkeletalMeshComponent(RPP);
        SK.SetLightEnvironment(RPP.Mesh.LightEnvironment);
        SK.SetLightingChannels(RPP.Mesh.LightingChannels);
        SK.SetParentAnimComponent(RPP.Mesh);
        if (RPC.GetCharacterName() == "Catwoman") SK.SetSkeletalMesh(Game.FindObject<SkeletalMesh>("Skeletons.Mesh.Catwoman_Skeleton_Skin"));
        else SK.SetSkeletalMesh(Game.FindObject<SkeletalMesh>("Skeletons.Mesh.Batman_Skeleton_Skin"));
        SK.SetDepthBias(depthtest);
        RPP.AttachComponent(SK);
    }

    public override void End()
    {
        RPP.DetachComponent(SK);
    }
}