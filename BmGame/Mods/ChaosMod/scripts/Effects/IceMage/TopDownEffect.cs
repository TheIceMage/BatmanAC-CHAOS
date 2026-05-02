using BmSDK.BmGame;
using BmSDK.Engine;
using System.Numerics;
using static ChaosScript;

namespace Chaos.Effects;

public class TopDownEffect : ChaosComponent
{
    private RCameraActor cam;
    private float h;
    public TopDownEffect()
    {
        Name = "TopDown Camera";
        LifeTime = 60;
    }
    public override void Start()
    {
        cam = Game.SpawnActor<RCameraActor>();
    }
    public override void Update()
    {
        UpdateCamera();
    }
    public override void End()
    {
        RPC.SetViewTarget(RPP);
    }

    private void UpdateCamera()
    {
        if (cam == null) return;
        h = RPC.IsInCombat() ? 1000 : 500;
            cam.Trace(
    out Vector3 HitLocation,
    out Vector3 HitNormal,
    RPP.Location with { Z = RPP.Location.Z + RPP.GetCollisionHeight() * 2 + h },
    RPP.Location with { Z = RPP.Location.Z + RPP.GetCollisionHeight() * 2 },
    true,
    new Vector3(0),
    out Actor.FTraceHitInfo HitInfo,
    0);
            cam.SetLocation(HitInfo.HitComponent == null ?
                RPP.Location with { Z = RPP.Location.Z + RPP.GetCollisionHeight() * 2 + h } : HitLocation);
        RPC.SetViewTarget(cam);
        cam.SetRotation(new()
        {
            Pitch = -90,
        });
    }
}
