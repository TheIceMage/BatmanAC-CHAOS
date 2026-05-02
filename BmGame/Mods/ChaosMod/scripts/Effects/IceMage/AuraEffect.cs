using BmSDK.BmScript;
using static ChaosScript;

namespace Chaos.Effects.AC;

//
//  _____         __  __                  
// |_   _|       |  \/  |                 
//   | |  ___ ___| \  / | __ _  __ _  ___ 
//   | | / __/ _ \ |\/| |/ _` |/ _` |/ _ \
//  _| || (_|  __/ |  | | (_| | (_| |  __/
// |_____\___\___|_|  |_|\__,_|\__, |\___|
//                              __/ |     
//                             |___/      
//

public class AuraEffect : ChaosComponent
{
    RSeqAct_ProtectiveAura aura;
    public AuraEffect()
    {
        Name = "Aura";
        LifeTime = 90;
    }
    public override void Start()
    {
        Game.LoadPackage("WayneArmory");
        aura = new RSeqAct_ProtectiveAura(Game.GetWorldInfo());
        aura.Activated();
        aura.bSuccessfullyInitialised = false;
    }
    public override void End()
    {
        aura.ClearAuras();
    }
}
