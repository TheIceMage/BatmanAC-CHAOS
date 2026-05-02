using BmSDK.BmGame;
using BmSDK.Engine;
using static ChaosScript;

namespace Chaos.Effects.AC;

public class SwitchEffect : ChaosComponent
{
    public SwitchEffect()
    {
        Name = "Switch Playable";
        LifeTime = 0;
    }
    public override void Start()
    {

        List<RPlayerController.FPlayableCharacterItem> PC2 = new List<RPlayerController.FPlayableCharacterItem> {
                new RPlayerController.FPlayableCharacterItem { BaseId = -1, Name = "BruceWayne"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Year_One"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Earth1"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Beyond"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Seventies"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Corps"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 0, Name = "Batman_Inc"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 3, Name = "NightWing"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 3, Name = "NightWing_Animated"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 2, Name = "Robin"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 2, Name = "Robin_Animated"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 2, Name = "Robin_Red"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 1, Name = "Catwoman"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 1, Name = "Catwoman_Animated"},
                new RPlayerController.FPlayableCharacterItem { BaseId = 1, Name = "Catwoman_Halloween"}
            };
        var C_PC2 = Game.GetPlayerController(0).PlayableCharactersV2;
        //foreach (var OG in C_PC2)
        //{
        //    PC2.Add(OG);
        //}
        var SP = Game.SpawnActor<PlayerStart>(Owner.Location, Owner.Rotation);
        var act = new RSeqAct_SwitchPlayerCharacter(Game.GetWorldInfo());
        var character = PC2[new Random().Next(PC2.Count)];
        switch (character.BaseId)
        {
            case -1: Game.LoadPackage("Playable_BruceWayne_SF"); break;
            case 0: Game.LoadPackage("Playable_Batman_SF"); break;
            case 1: Game.LoadPackage("Playable_Catwoman_SF"); break;
            case 2: Game.LoadPackage("Playable_Robin_SF"); break;
            case 3: Game.LoadPackage("Playable_Nightwing_SF"); break;
            case 4: Game.LoadPackage("Playable_Joker_SF"); break;
        }
        if (!character.Name.ToString().Contains("_"))
        {
            Game.LoadPackage("Playable_" + character.Name + "_Std_SF");
        }
        else
        {
            Game.LoadPackage("Playable_" + character.Name + "_SF");
        }
        act.CharacterName = character.Name;
        act.PlayerStartPoint = SP;
        Game.GetGameInfo().LoadPC("Playable_" + character.Name);
        RPC.PrepareForPlayerSwitch();
        act.RestartPlayer(RPC);
        RPP = Game.GetPlayerPawn(0) as RPawnPlayer;
        foreach (var comp in Owner.ScriptComponents)
        {
            if (comp is ChaosComponent && !(comp is SwitchEffect))
            {
                var ccomp = (comp as ChaosComponent).Clone();
                ccomp.Owner.DetachScriptComponent(ccomp);
                ccomp.bStart = false;
                RPP.AttachScriptComponent(ccomp);
                Debug.Log("Replicated " + ccomp.Name + " to " + ccomp.Owner.ToString() + " with " + ccomp.CurrentTime);
            }
        }
        Owner.Destroy();
    }
}
