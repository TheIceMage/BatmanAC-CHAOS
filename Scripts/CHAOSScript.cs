using BmSDK;
using BmSDK.BmGame;
using BmSDK.BmScript;
using BmSDK.Engine;
using System.Numerics;

[Script]
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
public class CHAOSScript : Script
{

    private bool bStartTimer = false;
    private float CurrentTime = 0;
    public float Timer = 30;
    public bool bEffectsHUD = false;

    public List<CHAOSComponent> CHAOSEffects = new List<CHAOSComponent>() {

        new MenuPPEffect(),
        new LigmaEffect(),
        new UndiesEffect(),
        new BigEffect(),
        new ExtraGEffect(),
        new InvGEffect(),
        new RemoveColorsEffect(),
        new SlowMoEffect(),
        new SpinEffect(),
        new FastMoEffect(),
        new HeavenEffect(),
        //new RasEffect(),
        new GodEffect(),
        new AllGodEffect(),
        new PacifistEffect(),
        new FlightEffect(),
        new GhostEffect(),
        new SmollEffect(),
        new SwitchEffect(), // new SwitchEffect(), new SwitchEffect(), new SwitchEffect(),
        new PauseEffect(),
        new CanAlwaysTakedownEffect(),
        new StickBiasEffect(),
        new FullOnAttackEffect(),
        new WireframeEffect(),
        new RampageEffect(),
        new BoogalooEffect(),
        new GunsEffect(),
        new HUDEffect(),
        new AslumEffect(),
        new PhotoRealEffect(),
        new WalkEffect(),
        new RagdollEffect(),    
        new ParentsEffect(),
        new BigHeadEffect(),
        new SnakeEffect(),
        new TeleportEffect(),
        new OneTapEffect(),
        new KillAllEffect(),
        new InvertCommandsEffect(),
        new WrapPawnsEffect(),
        new BaneEffect(),
        new QuickFireEffect(),
        new StaticEffect(),
        new Timerx2Effect(),
        new Timerd2Effect()
    };

    public override void OnKeyDown(Keys key)
    {
        if (key == Keys.F12)
        {
            if (Game.GetPlayerPawn(0).IsValid())
            {
                if (!bStartTimer)
                {
                    bStartTimer = true;
                    RiddlerPrompt(bEffectsHUD, "ACTIVE");
                }
                else
                {
                    bStartTimer = false;
                    RiddlerPrompt(bEffectsHUD, "PAUSED");
                }
            }
        }
        if (key == Keys.F11)
        {
            bEffectsHUD = !bEffectsHUD;
            RiddlerPrompt(bEffectsHUD, "Effects HUD");
        }
    }

    public override void OnEnterMenu()
    {
        //Game.GetConsole().ConsoleCommand("start DebugFrontend");
    }
    public override void OnTick()
    {
        if (Game.GetPlayerController(0).Pawn == null)
        {
            return;
        }
            if (bStartTimer == true)
        {
            CurrentTime += Game.GetDeltaTime() / Game.GetWorldInfo().TimeDilation;
            UpdateHUDTimer(Timer - CurrentTime);
    
            if (CurrentTime >= Timer - 0.1)
            {
                ExecuteRandomCHAOSComponent();
                CurrentTime = 0;
            }
        }
    }

    private void ExecuteRandomCHAOSComponent()
    {
            ExecuteCHAOSComponent(CHAOSEffects[new Random().Next(CHAOSEffects.Count)]);
    }
    
    private void ExecuteCHAOSComponent(CHAOSComponent Effect)
    {
        if (Effect.Owner != null)
        {
            Effect.Owner.DetachScriptComponent(Effect);
            Debug.Log(Effect.Name + " - Prevented crash with Detach");
        }
        if (Game.GetPlayerController(0).Pawn != null)
        {
            Effect.MainScript = this;
            Effect.MainScript.Timer = Timer;
            Debug.Log(Effect);
            RiddlerPrompt(bEffectsHUD, Effect.Name);
            Game.GetPlayerPawn(0).AttachScriptComponent(Effect);
        }
    }

    private static void UpdateHUDTimer(float Time)
    {
        if (Game.GetPlayerController(0).Pawn == null)
        {
            return;
        }
        var act = new RSeqAct_HUDTimer(Game.GetWorldInfo());
        act.SetClockTimer(Game.GetPlayerController(), MathF.Truncate(Time).ToString(), true, false);
        act.Activated();
    }

    private static void HUDPrompt(FString Text)
    {
        if (Game.GetPlayerController(0).Pawn == null)
        {
            return;
        }
        var act = new RSeqAct_HelpText(Game.GetWorldInfo());
        act.FakeRiddleText = Text;
        act.Activated();
    }
    private static void RiddlerPrompt(bool bShow, FString Text)
    {
        if (Game.GetPlayerController(0).Pawn == null)
        {
            return;
        }
        Game.GetPlayerController(0).bEnygmaScreenTutorial = bShow;
            Game.GetPlayerController(0).HudMovieNew.GeneralMovie.ShowRiddle(bShow, Text);
    }


    /// <summary>
    /// CHAOS EFFECTS
    /// </summary>
    public class CHAOSComponent : ScriptComponent
    {
        public CHAOSScript MainScript;
        public RPawnPlayer RPP;
        public RPlayerControllerCombat RPC;
        public string Name = "CHAOSComponent";
        public float CurrentTime = 0;
        public float LifeTime = 90;
        public bool bEnd = false;
        public bool bStart = true;
        public bool bRetriggerable = false;
        public CHAOSComponent Clone()
        {
               return this.MemberwiseClone() as CHAOSComponent;
        }
        public override void OnAttach()
        {
            if (!Owner.IsValid() || !bStart)
            {
                Debug.Log(Name + " - Prevented crash with " + (LifeTime - CurrentTime).ToString() + " s left");
                return;
            }
            Debug.Log(Name + " - Started from " + Owner.Name.ToString());
            RPP = Game.GetPlayerPawn(0) as RPawnPlayer;
            RPC = Game.GetPlayerController(0) as RPlayerControllerCombat;
            Start();
        }
        public override void OnTick()
        {
            if (!Owner.IsValid())
            {
                Debug.Log(Name + " - Prevented crash with " + (LifeTime - CurrentTime).ToString() + " s left");
                return;
            }
            Update();
            
            CurrentTime += Game.GetDeltaTime();
            if (bRetriggerable)
            {
                if (CurrentTime >= MainScript.Timer)
                {
                    Start();
                    LifeTime -= MainScript.Timer;
                    CurrentTime -= MainScript.Timer;
                }
            }
            if (CurrentTime >= LifeTime - 0.1)
            {
                    bEnd = true;
                Owner.DetachScriptComponent(this);
            }
        }
        public override void OnDetach()
        {
            if (!Owner.IsValid() || !bEnd)
            {
                Debug.Log(Name + " - Prevented crash with " + (LifeTime - CurrentTime).ToString() + " s left");
                return;
            }
                Debug.Log(Name + " - Ended from " + Owner.Name.ToString());
            RPP = Game.GetPlayerPawn(0) as RPawnPlayer;
            RPC = Game.GetPlayerController(0) as RPlayerControllerCombat;
            End();
            
        }
        public virtual void Start()
        {

        } 

        public virtual void Update()
        {
           
        }

        public virtual void End()
        {

        }

        public static void SpawnCharacters<SpawnPawn, SpawnCharacter>(int Amount)
        where SpawnPawn : RBMPawnAI, IGameObject
        where SpawnCharacter : RCharacter, IGameObject
        {
            var player = Game.GetPlayerPawn();
            for (int i = 0; i < Amount; i++)
            {
                var cock = Game.SpawnCharacter<SpawnPawn, SpawnCharacter>(
                    player.Location, //with
                                     //{
                                     //Z = player.Location.Z + 100 * i
                                     //},
                    player.Rotation
                    );
                RPawnCombat.FDamageInfo dmginfo;
                dmginfo.DamageAmount = 0;
                cock.DamagedBy(dmginfo);
            }
        }
    }

    public class LigmaEffect : CHAOSComponent
    {
        RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
        public LigmaEffect()
        {
            Name = "LIGMA";
        }
        public override void Start()
        {
            
            //NO SUPPORT FOR AKBANK; CAN'T OPTIMIZE SF
            Game.LoadPackage("Under_C2_Ch4");
            var act = new RSeqAct_SetPoisonLevel(Game.GetWorldInfo());
            act.PoisonLevel = RPlayerController.EBatmanPoisonLevel.BPL_PoisonMaximum;
            act.Activated();
            var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Batman_Level_Moves.Under_C2.1stPersonCollapse");
            Loc.Location = RPP.Location;
            Loc.Normal = InvertDirection((RotationDirection(RPP.Rotation)));
            RMC.TriggerSpecialMove(RPC, Loc, true);
        }
        public override void Update()
        {

            //if (CurrentTime >= Timer)
            //{
            //    var act = new RSeqAct_BatmanCough(Game.GetWorldInfo());
            //    act.RPC = Game.GetPlayerController(0);
            //    act.Activated();
            //    CurrentTime -= Timer;
            //}
            RPC.Stunned();
        }

    }

    public class RasEffect : CHAOSComponent
    {
        RSeqAct_RasBossLogic test = null;
        public RasEffect()
        {
            Name = "Lag Ninja";
            LifeTime = 30;
        }
        public override void Start()
        {
            
            Game.LoadPackage("Under_S3_Ch4");
            test = new RSeqAct_RasBossLogic(Game.GetWorldInfo());
            test.Activated();
            test.DoSpawn(Owner, false);

        }
        public override void Update()
        {
            
            test.DuplicateRas(false);
        }
        public override void End()
        {
            
        }
    }
    public class SpinEffect : CHAOSComponent
    {
        private bool bDisabled = false;
        public SpinEffect()
        {
            Name = "Like a record baby!";
        }
        public override void Start()
        {
            foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
            {
                if (pion != Owner && pion.IsValid())
                {
                    var s = new SpinEffect();
                    s.bStart = false;
                    pion.AttachScriptComponent(s);
                }
            }
        }
        public override void OnTick()
        {
            Update();
        }
        public override void Update()
        {
            if (!bDisabled)
            {
                if (!Owner.IsValid())
                {
                    Debug.Log(Name + " - Prevented crash with " + (LifeTime - CurrentTime).ToString() + " s left");
                    return;
                }
                CurrentTime += Game.GetDeltaTime();
                if (CurrentTime >= LifeTime - 0.1)
                {
                    bDisabled = true;
                    Debug.Log(Name + "Disabled on " + Owner.Name);
                }
                Owner.Rotation = Owner.Rotation with
                {
                    Yaw = (int)MathF.Round(Owner.Rotation.Yaw + (690420f * Game.GetDeltaTime()))
                };
            }
        }
    }

    public class BigEffect : CHAOSComponent
    {
        public BigEffect()
        {
            Name = "BIG";
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale * 4).ToString());
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (Owner.DrawScale / 4).ToString());
        }
    }
    public class SmollEffect : CHAOSComponent
    {
        public SmollEffect()
        {
            Name = "smoll";
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (RPP.DrawScale / 4).ToString());
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set rpawn drawscale " + (RPP.DrawScale * 4).ToString());
        }
    }
    public class SlowMoEffect : CHAOSComponent
    {
        public SlowMoEffect()
        {
            Name = "SlowMo";
            LifeTime = 30;
        }
        public override void Start()
        {
            Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed / 4);
        }
        public override void End()
        {
            
            Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed * 4);
        }
    }
    public class FastMoEffect : CHAOSComponent
    {
        public FastMoEffect()
        {
            LifeTime = 30;
            Name = "FastMo";
        }
        public override void Start()
        {
            Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed * 4);
        }
        public override void End()
        {
            
            Game.GetGameInfo().SetDebugGameSpeed(Game.GetGameInfo().DebugGameSpeed / 4);
        }
    }
    public class ExtraGEffect : CHAOSComponent
    {
        public ExtraGEffect()
        {
            Name = "Intense Gravity";
        }
        public override void Start()
        {
            
            Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * 1000);
        }
        public override void End()
        {
            
            Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() / 1000);
        }
    }

    public class InvGEffect : CHAOSComponent
    {
        public InvGEffect()
        {
            Name = "Inverted Gravity";
        }
        public override void Start()
        {
            
            Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * -1);
        }
        public override void End()
        {
            
            Game.GetCheatManager().SetGravity(Game.GetWorldInfo().GetGravityZ() * -1);
        }
    }

    public class RemoveColorsEffect : CHAOSComponent
    {
        public RemoveColorsEffect()
        {
            Name = "Removed Colors";
        }
        public override void Start()
        {
            
            Game.GetGameViewportClient().bOverrideDiffuseAndSpecular = true;
        }
        public override void End()
        {
            
            Game.GetGameViewportClient().bOverrideDiffuseAndSpecular = false;
        }
    }

    public class UndiesEffect : CHAOSComponent
    {
        private SkeletalMesh SK;
        public UndiesEffect()
        {
            Name = "Honey, where are my paaaaaanntss!?";
        }
        public override void Start()
        {
            SK = RPP.SecondaryMesh.SkeletalMesh;
            Game.LoadPackage("CHAOS_Undies_SF");
            var Undies = Game.FindObject<SkeletalMesh>("IceMage.Mesh.Batman_Undies_Skin");
            (Owner as RPawn).SecondaryMesh.SetSkeletalMesh(Undies);
        }
       public override void End()
       {

            (Owner as RPawn).SecondaryMesh.SetSkeletalMesh(SK);
       }
    }

    public class HeavenEffect : CHAOSComponent
    {
        public HeavenEffect()
        {
            Name = "Heaven";
            LifeTime = 0;
        }
        public override void Start()
        {
            
            RPP.SetLocation(Owner.Location with { Z = 69420 });
        }
    }
    public class FlightEffect : CHAOSComponent
    {
        float h = 69420;
        public FlightEffect()
        {
            Name = "Flight";
            LifeTime = 30;
        }
        public override void Start()
        {
            
            Game.GetCheatManager().Fly();
        }
        public override void Update()
        {
            if (RPP.Health != h)
            {
                h = RPP.Health;
                Game.GetCheatManager().Fly();
            }
        }
        public override void End()
        {
            
            Game.GetCheatManager().Walk();
        }
    }
    public class GhostEffect : CHAOSComponent
    {
        float h = 69420;
        public GhostEffect()
        {
            Name = "Ghost";
            LifeTime = 30;
        }
        public override void Start()
        {
            Game.GetCheatManager().Ghost();
        }

        public override void Update()
        {
            if (RPP.Health != h)
            {
                h = RPP.Health;
                Game.GetCheatManager().Ghost();
            }
        }

        public override void End()
        {
            
            Game.GetCheatManager().Walk();
        }
    }
    public class PauseEffect : CHAOSComponent
    {
        public PauseEffect()
        {
            Name = "Pause";
            LifeTime = 0;
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("Pause");
        }
    }
    public class SwitchEffect : CHAOSComponent
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
                case 0: Game.LoadPackage("Playable_Batman_SF");  break;
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
                if (comp is CHAOSComponent && !(comp is SwitchEffect)) {
                    var ccomp = (comp as CHAOSComponent).Clone();
                    ccomp.Owner.DetachScriptComponent(ccomp);
                    ccomp.bStart = false;
                    RPP.AttachScriptComponent(ccomp);
                    Debug.Log("Replicated " + ccomp.Name + " to " + ccomp.Owner.ToString() + " with " + ccomp.CurrentTime);
                        }
            }
            Owner.Destroy();
        }
    }
    public class MenuPPEffect : CHAOSComponent
    {
        public MenuPPEffect()
        {
            Name = "Menu Filter";
        }
        public override void Update()
        {

            RPC.TurnOnMenuPP();
        }
        public override void End()
        {

            RPC.TurnOffMenuPP();
        }
    }
    public class CanAlwaysTakedownEffect : CHAOSComponent
    {
        public CanAlwaysTakedownEffect()
        {
            Name = "Unlimited Takedowns";
            bRetriggerable = true;
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData Debug_Combat_CanAlwaysTakedown true");
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData Debug_Combat_CanAlwaysTakedown false");
        }
    }


    public class FullOnAttackEffect : CHAOSComponent
    {
        public FullOnAttackEffect()
        {
            Name = "Relentless Enemies";
            bRetriggerable = true;
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPawnCombat bFullOnAttack true");
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPawnCombat bFullOnAttack false");
        }
    }

    public class StickBiasEffect : CHAOSComponent
    {
        public StickBiasEffect()
        {
            Name = "Stick Drift";
        }
        public override void Update()
        {

            Game.GetPlayerController(0).PlayerInput.aStrafe += 0.5f;
            Game.GetPlayerController(0).PlayerInput.aBaseY += 0.5f;
        }
    }

    public class RampageEffect : CHAOSComponent
    {
        private ParticleSystemComponent FistLPSC;
        private ParticleSystemComponent FistRPSC;
        private ParticleSystemComponent TrailLPSC;
        private ParticleSystemComponent TrailRPSC;
        public RampageEffect()
        {
            Name = "B.A.T. Mode";
        }
        public override void Start()
        {
            FistLPSC = new ParticleSystemComponent(Game.GetWorldInfo());
           FistRPSC = new ParticleSystemComponent(Game.GetWorldInfo());
           TrailLPSC  = new ParticleSystemComponent(Game.GetWorldInfo());
           TrailRPSC = new ParticleSystemComponent(Game.GetWorldInfo());
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
            if (FistLPSC != null && FistRPSC != null && TrailLPSC != null && TrailRPSC != null  )
                {
                FistLPSC.DeactivateSystem();
                FistRPSC.DeactivateSystem();
                TrailLPSC.DeactivateSystem();
                TrailRPSC.DeactivateSystem();
            }
        }
    }
    public class WireframeEffect : CHAOSComponent
    {
        public WireframeEffect()
        {
            Name = "Wireframe";
            bRetriggerable = true;
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set Engine.Material Wireframe true");
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set Engine.Material Wireframe false");
        }
    }
    public class BoogalooEffect : CHAOSComponent
    {
        private float Timer = 0.1f, CurrentTimer = 0.1f;
        public BoogalooEffect()
        {
            Name = "Electric Boogaloo";
            LifeTime = 30;
        }
        public override void Update()
        {
            RPawnCombat.FDamageInfo dmginfo;
            dmginfo.DamageAmount = 0;
            dmginfo.Attacker = null;
            dmginfo.DamageType = RDmgType_Electricity.StaticClass();
            CurrentTimer += Game.GetDeltaTime();
            if (CurrentTimer >= Timer)
            {
                (Owner as RPawnPlayer).DamagedBy(dmginfo);
                CurrentTimer -= Timer;
            }

        }
    }
    public class AslumEffect : CHAOSComponent
    {

        public AslumEffect()
        {
            Name = "Take me on home!";
            LifeTime = 0;
            // 45071.51, Y = 69551.78, Z = 6167.5103
        }
        public override void Start()
        {
            
            GameObject.FVector Aslum;
            Aslum.X = 45000; Aslum.Y = 69000; Aslum.Z = 6900;
            Owner.SetLocation(Aslum);


        }
    }

    public class GunsEffect : CHAOSComponent
    {
        public GunsEffect()
        {
            Name = "Now who's left all those dangerous-looking weapons there!";
            LifeTime = 0;
        }
        public override void Start()
        {
            
            Game.LoadPackage("StreamedWeapon_CombatRifle_SF");
            foreach (var pion in GameObject.FindObjectsSlow<RBMPawnAI>())
            {
                if (pion.IsValid() && pion != Owner)
                {
                    //var weapon = (RBMWeaponCombatRifle)pion.CreateInventory(RBMWeaponCombatRifle.StaticClass());
                    //pion.ChangeWeapon(weapon, weapon, true);
                    pion.SpawnWeapon(RBMWeaponCombatRifle.StaticClass(), true);
                }
            };
        }
    }
    public class GodEffect : CHAOSComponent
    {
        public GodEffect()
        {
            Name = "Invincibility";
            bRetriggerable = true;
        }
        public override void Start()
        {
            
            Game.GetPlayerController(0).bGodMode = true;
            
        }
        public override void End()
        {

            Game.GetPlayerController(0).bGodMode = false;

        }
    }

    public class AllGodEffect : CHAOSComponent
    {
        public AllGodEffect()
        {
            Name = "Everyone's Invincible";
            bRetriggerable = true;
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData bAIInvulnerable true");

        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("set BmGame.RPersistentDebugData bAIInvulnerable false");

        }
    }

    public class PacifistEffect : CHAOSComponent
    {
        public PacifistEffect()
        {
            Name = "Pacifist Enemies";
        }
        public override void Start()
        {

            Game.GetCheatManager().ToggleEnemiesCanAttack();

        }
        public override void End()
        {

            Game.GetCheatManager().ToggleEnemiesCanAttack();

        }
    }

    public class HUDEffect : CHAOSComponent
    {
        public HUDEffect()
        {
            Name = "No HUD";
        }
        public override void Start()
        {
            
            Game.GetConsole().ConsoleCommand("ToggleHUD");
        }
        public override void End()
        {
            
            Game.GetConsole().ConsoleCommand("ToggleHUD");
        }
    }

    public class RagdollEffect : CHAOSComponent
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

    public class PhotoRealEffect : CHAOSComponent
    {
        public PhotoRealEffect()
        {
            Name = "Photorealistic ReShade";
        }
        public override void Start()
        {
            
            Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier = Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier / 10;
            Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier = Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier / 10;
            Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier = Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier * 10;
        }
        public override void End()
        {
            
            Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier = Game.GetEngine().GamePlayers[0].PP_DesaturationMultiplier * 10;
            Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier = Game.GetEngine().GamePlayers[0].PP_HighlightsMultiplier * 10;
            Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier = Game.GetEngine().GamePlayers[0].PP_MidTonesMultiplier / 10;
        }
    }

    public class WalkEffect : CHAOSComponent
    {
        RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
        public WalkEffect()
        {
            Name = "I'm gonna walk 5000 miles!";
            LifeTime = 30;
        }
        public override void Start()
        {
            Game.LoadPackage("Under_S1_Ch4b");
            var RMC = Game.FindObject<RSpecialMoveConfig_CustomWalk>("Batman_Level_Moves.Sick.SickWalk");
            Loc.Location = RPP.Location;
            Loc.Normal = InvertDirection((RotationDirection(RPP.Rotation)));
            Game.GetCheatManager().Walk();
            RMC.TriggerSpecialMove(RPC, Loc, true);
        }
        public override void End()
        {
            if (RPP.CurrentSpecialMove != null)
            {
                RPP.CurrentSpecialMove.FinishSpecialMove();
            }
            RPP.BeginChangePose();
            RPP.ChangePose("Standing", "Relaxed");
            RPP.EndChangePose();
        }
    }

    public class ParentsEffect : CHAOSComponent
    {
        private Package UPK;
        RPawnPlayer.FEnvironmentSpecialMoveLocator Loc;
        public ParentsEffect()
        {
            Name = "I've fallen, and I can't get up!";
            LifeTime = 15;

            //CHAOSScript.cs: FVector { X = 5840, Y = -5232, Z = 3640.1548 }
            //CHAOSScript.cs: FVector { X = 6031.7007, Y = -5272.888, Z = 3757.4639 }

        }
        public override void Start()
        {
            UPK = Game.LoadPackage("Under_S1_Ch4b");
            //UPK.AddToRoot();
            var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Anim_Level_Batman.SpecialMoves.UnderworldS1_ND_SeeParents");
            Loc.Location = RPP.Location with { Z = RPP.Location.Z - 85 };
            Loc.Normal = InvertDirection((RotationDirection(RPP.Rotation)));
            RMC.TriggerSpecialMove(RPC, Loc, true);
        }
        public override void End()
        {
            //if (!bStart) { Debug.Log("Parents crash avoided"); return; }
            //var RMC = Game.FindObject<RSpecialMoveConfigConfigurable>("Anim_Level_Batman.SpecialMoves.UnderworldS1_ND_Out");
            //UPK.RemoveFromRoot();
            //RMC.TriggerSpecialMove(RPC, Loc, true);
            Game.GetCheatManager().Fly();
            Game.GetCheatManager().Walk();
        }
    }
    public class BigHeadEffect : CHAOSComponent
    {
        public BigHeadEffect()
        {
            Name = "BigHead Mode";
        }
        public override void Start()
        {
            RPC.BigHeadMode();
        }
        public override void End()
        {
            RPC.BigHeadMode();
        }
    }

    public class SnakeEffect : CHAOSComponent
    {
        public SnakeEffect()
        {
            Name = "When you can't even say my name";
            bRetriggerable = true;
        }
        public override void Start()
        {
            foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
            {
                if (pion.IsValid())
                {
                    if (!pion.bHidden)
                    {
                        pion.SetHidden(true);
                    }
                }
            }
        }
        public override void End()
        {
            foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
            {
                if (pion.IsValid())
                {
                    pion.SetHidden(false);
                }
            }
        }
    }

    public class KillAllEffect : CHAOSComponent
    {
        public KillAllEffect()
        {
            Name = "Omae wa mou shindeiru";
            LifeTime = 0;
        }
        public override void Start()
        {
            Game.GetCheatManager().DebugKillAllEnemies();
        }
    }

    public class OneTapEffect : CHAOSComponent
    {
        public OneTapEffect()
        {
            Name = "OneTap";
            LifeTime = 0;
        }
        public override void Start()
        {
            RPP.DebugRemoveArmour();
            RPP.Health = 1;
        }
    }

    public class TeleportEffect : CHAOSComponent
    {
        public TeleportEffect()
        {
            Name = "Teleport";
            LifeTime = 0;
        }
        public override void Start()
        {
            Game.GetCheatManager().Teleport();
        }
    }

    public class InvertCommandsEffect : CHAOSComponent
    {
        public InvertCommandsEffect()
        {
            Name = "Inverted Commands";
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

    public class WrapPawnsEffect : CHAOSComponent
    {
        public WrapPawnsEffect()
        {
            Name = "Everyone teleports to you";
            LifeTime = 0;
        }
        public override void Start()
        {
            foreach (var pion in GameObject.FindObjectsSlow<RPawn>())
            {
                if (pion != Owner && pion.IsValid())
                {
                    pion.SetLocationIgnoringCollision(Owner.Location);
                }
            }
        }
    }

    public class QuickFireEffect : CHAOSComponent
    {
        public QuickFireEffect()
        {
            Name = "QuickFire";
        }
        public override void Update()
        {
            RPC.QuickFireBatarang();
        }
    }

    public class BaneEffect : CHAOSComponent
    {
        public BaneEffect()
        {
            Name = "Surprised to see me, Batman?";
            LifeTime = 0;
        }
        public override void Start()
        {
            Game.LoadPackage("BaneSS_B1");
            SpawnCharacters<RPawnVillainBane, RCharacter_Bane>(4);
        }
    }

    public class StaticEffect : CHAOSComponent
    {
        public StaticEffect()
        {
            Name = "FREEZE";
            LifeTime = 30;
            bRetriggerable = true;
        }
        public override void Start()
        {
                    Owner.bStatic = true;
        }
        public override void End()
        {
                Owner.bStatic = false;
        }
    }

    public class Timerx2Effect : CHAOSComponent
    {
        public Timerx2Effect()
        {
            Name = "Timer x2";
        }
        public override void Start()
        {
            MainScript.Timer *= 2;
        }
        public override void End()
        {
            MainScript.Timer /= 2;
        }
    }

    public class Timerd2Effect : CHAOSComponent
    {
        public Timerd2Effect()
        {
            Name = "Timer /2";
        }
        public override void Start()
        {
            MainScript.Timer /= 2;
        }
        public override void End()
        {
            MainScript.Timer *= 2;
        }
    }

    static GameObject.FVector RotationDirection(GameObject.FRotator rot)
    {
        Vector3 vv;
        vv.X = MathF.Cos(rot.Pitch * MathF.PI / 32768.0f) * MathF.Cos(rot.Yaw * MathF.PI / 32768.0f);
        vv.Y = MathF.Cos(rot.Pitch * MathF.PI / 32768.0f) * MathF.Sin(rot.Yaw * MathF.PI / 32768.0f);
        vv.Z = MathF.Sin(rot.Pitch * MathF.PI / 32768.0f);
        GameObject.FVector v;
        vv = Vector3.Normalize(vv);

        v.X = vv.X; v.Y = vv.Y; v.Z = vv.Z;

        return v;
    }

    static GameObject.FVector InvertDirection(GameObject.FVector dir)
    {
        dir.X = dir.X * -1;
        dir.Y = dir.Y * -1;
        dir.Z = dir.Z * -1;
        return dir;
    }
}