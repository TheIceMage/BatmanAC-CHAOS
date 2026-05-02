using BmSDK;
using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects;

public class SkeletonsEffect : ChaosComponent
{
    SkeletalMeshComponent SK;
    public SkeletonsEffect()
    {
        Name = "Spooky Scary Skeletons";
        LifeTime = 90;
    }

    public override void Start()
    {
        if (Owner == RPP)
        {
            Game.LoadPackage("CHAOS_Skeletons_SF");
            foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
            {
                if (pion != Owner && pion.IsValid && !pion.IsClassDefaultObject && pion.Mesh.SkeletalMesh != null)
                {
                    var s = new SkeletonsEffect();
                    pion.AttachScriptComponent(s);
                }
            }
        }
        else
        {
            var own = Owner as RPawn;
            var depthtest = new SkeletalMeshComponent.FDepthBiasData
            {
                DepthBias = -255,
                AlternateDepthBias = -255,
                DepthBiasCalculationType = SkeletalMeshComponent.EDepthBiasCalculationType.DEPTHBIASCALCULATIONTYPE_Constant,
                DepthBiasApplicationType = SkeletalMeshComponent.EDepthBiasApplicationType.DEPTHBIASAPPLICATIONTYPE_World,
                DepthBiasMinDistanceFromCameraPlaneOverride = 0,
                MinDepthBiasMultiplier = 1
            };
            SK = new SkeletalMeshComponent(own);
            SK.SetLightEnvironment(own.Mesh.LightEnvironment);
            SK.SetLightingChannels(own.Mesh.LightingChannels);
            SK.SetParentAnimComponent(own.Mesh);
            SK.SetSkeletalMesh(Game.FindObject<SkeletalMesh>("Mental_Patient.Mesh.Scarecrow_Skeleton"));
            SK.SetDepthBias(depthtest);
            own.AttachComponent(SK);
        }
        }

    public override void End()
    {
        if(SK != null)
        Owner.DetachComponent(SK);
    }
}