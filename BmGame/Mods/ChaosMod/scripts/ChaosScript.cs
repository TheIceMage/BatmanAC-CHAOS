using BmSDK;
using BmSDK.BmGame;
using BmSDK.GFxUI;
using BmSDK.IceMage.GFxHUD;
using Chaos.Effects;
using System.Numerics;
using System.Reflection;


[Script]
public class ChaosScript : Script
{

    private static GFxHUD HUD = null;
    private static GFxObject CEs = null;
    private static bool bStartTimer = false;
    private static float CurrentTime = 0;
    private static float LastTime;
    public float Timer = 30;
    public bool bEffectsHUD = false;

    public List<ChaosComponent> AgnosticEffects =
    Assembly.GetExecutingAssembly().GetTypes().Where(
        t => t.IsClass &&
             (t.Namespace == "Chaos.Effects" || t.Namespace == "Chaos.Effects.AC") &&
            !t.IsAbstract &&
            typeof(ChaosComponent).IsAssignableFrom(t))
        .Select(t => (ChaosComponent)Activator.CreateInstance(t)!).ToList();

    public override void OnEnterGame()
    {
        HUD = new GFxHUD("ChaosHud", "Chaos");
    }

    public override void OnLoad()
    {
        HUD.OpenSWF(HUD.SwfName, HUD.SwfPath);
    }

    [Redirect(typeof(RPlayerController), nameof(RPlayerController.InitStandardHUD))]
    public static void InitStandardHud(RPlayerController self)
    {
        self.InitStandardHUD();
        if (self.HudMovieNew != null)
        {
            HUD.CreateHudExtension(self);
        }
    }

    public override void OnKeyDown(Keys key)
    {
        if (key == Keys.F12)
        {
            if (Game.GetPlayerController(0).Pawn == null) return;
                if (!bStartTimer)
                {
                    bStartTimer = true;
                    InitHUDTimer();
                }
                else
                {
                    bStartTimer = false;
                    HideHUDTimer();
                }
        }
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
                AttachRandomChaosComponent();
                CurrentTime = 0;
            }
        }
    }

    private void AttachRandomChaosComponent(bool bShowTimer = true)
    {
        AttachChaosComponent(AgnosticEffects[new Random().Next(AgnosticEffects.Count)], bShowTimer);
    }

    private void AttachChaosComponent(ChaosComponent Effect, bool bShowTimer = false)
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
            AddHUDEffect(Effect.Name, Effect.LifeTime, bShowTimer);
            Game.GetPlayerPawn(0).AttachScriptComponent(Effect);
        }
    }
    private static void UpdateHUDTimer(float Time)
    {
        if (Game.GetPlayerController(0).Pawn == null) return;
        if (!HUD.GetVariable("bTimerInitiated", Game.GetPlayerController(0)).B)
        {
            InitHUDTimer();
        }
        var arg = new TArray<GFxMoviePlayer.FASValue>(1);
        arg.Add(new GFxMoviePlayer.FASValue { Type = GFxMoviePlayer.ASType.AS_String, S = MathF.Truncate(Time).ToString() });
        HUD.Invoke("SetTimerText", arg, Game.GetPlayerController(0));
        if (MathF.Truncate(Time) != MathF.Truncate(LastTime))
        {
            HUD.ActionScriptVoid("AgeEffects", Game.GetPlayerController(0));
        }
        LastTime = Time;
    }

    private static void InitHUDTimer()
    {
        if (Game.GetPlayerController(0).Pawn == null) return;
        var arg = new TArray<GFxMoviePlayer.FASValue>(1);
        arg.Add(new GFxMoviePlayer.FASValue { Type = GFxMoviePlayer.ASType.AS_String, S = "000" });
        var hm = HUD.Invoke("InitTimer", arg, Game.GetPlayerController(0));
    }

    private static void HideHUDTimer()
    {
        if (Game.GetPlayerController(0).Pawn == null) return;
        HUD.ActionScriptVoid("HideTimer", Game.GetPlayerController(0));
    }

    private static void AddHUDEffect(FString Text, float Time, bool bShowTimer = true)
    {
        if (Game.GetPlayerController(0).Pawn == null) return;
        var args = new TArray<GFxMoviePlayer.FASValue>(3);
        if (Time == 0)
        {
            Time = 90;
            bShowTimer = false;
        }
        args.Add(new GFxMoviePlayer.FASValue { Type = GFxMoviePlayer.ASType.AS_String, S = Text });
        args.Add(new GFxMoviePlayer.FASValue { Type = GFxMoviePlayer.ASType.AS_String, S = Time.ToString() });
        args.Add(new GFxMoviePlayer.FASValue { Type = GFxMoviePlayer.ASType.AS_Boolean, B = bShowTimer });
        var hm = HUD.Invoke("AddEffect", args, Game.GetPlayerController(0));
    }



    public static Vector3 RotationDirection(Rotator rot)
    {
        Vector3 vv;
        vv.X = MathF.Cos(rot.Pitch) * MathF.Cos(rot.Yaw);
        vv.Y = MathF.Cos(rot.Pitch) * MathF.Sin(rot.Yaw);
        vv.Z = MathF.Sin(rot.Pitch);
        vv = Vector3.Normalize(vv);

        return vv;

        //DEPRICATED AFTER v0.1
        //
        //vv.X = MathF.Cos(FRotatorDegrees(rot.Pitch)) * MathF.Cos(FRotatorDegrees(rot.Yaw));
        //vv.Y = MathF.Cos(FRotatorDegrees(rot.Pitch)) * MathF.Sin(FRotatorDegrees(rot.Yaw));
        //vv.Z = MathF.Sin(FRotatorDegrees(rot.Pitch));
        //
        //GameObject.FVector v;
        //v.X = vv.X; v.Y = vv.Y; v.Z = vv.Z;
        //return v;
    }

    public static float VectorLength(Vector3 v)
    {
        return MathF.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
    }


    private static void LoadBane()
    {
        var console = Game.GetConsole();
        console.ConsoleCommand(
            "start batentry?Players=Playable_Batman?Area=BaneSS,BaneSS_B1?Chapters=1"
        );
    }




    /// <summary>
    /// CHAOS EFFECTS
    /// </summary>
    public abstract class ChaosComponent : ScriptComponent
    {
        public ChaosScript MainScript;
        public RPawnPlayer RPP;
        public RPlayerControllerCombat RPC;
        public string Name = "ChaosComponent";
        public float CurrentTime = 0;
        public float LifeTime = 90;
        public bool bEnd = false;
        public bool bStart = true;
        public bool bRetriggerable = false;
        public ChaosComponent Clone()
        {
            return this.MemberwiseClone() as ChaosComponent;
        }
        public override void OnAttach()
        {
            if (!Owner.IsValid || !bStart)
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
            if (!Owner.IsValid)
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
            if (CurrentTime >= LifeTime - Game.GetDeltaTime())
            {
                bEnd = true;
                Owner.DetachScriptComponent(this);
            }
        }
        public override void OnDetach()
        {
            if (!Owner.IsValid || !bEnd)
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

}