using static ChaosScript;

namespace Chaos.Effects;

public class WaypointEffect : ChaosComponent
{
    public WaypointEffect()
    {
        Name = "Waypoint";
        LifeTime = 0;
    }
    public override void Start()
    {
        RPP.SetLocation(RPC.PlayerWaypoint.Location);
        RPP.SetRotation(RPC.PlayerWaypoint.Rotation);
    }
}
