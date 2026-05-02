using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class InvertCommandsEffect : ChaosComponent
{
    public InvertCommandsEffect()
    {
        Name = "Inverted Commands";
        LifeTime = 90;
    }
    public override void Start()
    {
        var RPI = RPC.PlayerInput as RPlayerInput;
        RPI.bInvertJoystick = true;
        RPI.bInvertBatarang = true;
        RPI.bInvertedCeilingControls = true;
        RPI.bInvertCapeGlide = true;
        RPI.bInvertMouse = true;
        RPI.bInvertTurn = true;
        RPI.bSticksInverted = true;
        RPI.SaveConfig();
    }
    public override void End()
    {
        var RPI = RPC.PlayerInput as RPlayerInput;
        RPI.bInvertJoystick = false;
        RPI.bInvertBatarang = false;
        RPI.bInvertedCeilingControls = false;
        RPI.bInvertCapeGlide = false;
        RPI.bInvertMouse = false;
        RPI.bInvertTurn = false;
        RPI.bSticksInverted = false;
        RPI.SaveConfig();
    }
}
