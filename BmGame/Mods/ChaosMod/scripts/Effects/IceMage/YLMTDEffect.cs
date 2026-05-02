using BmSDK.BmGame;
using static ChaosScript;

namespace Chaos.Effects;

public class YLMTDEffect : ChaosComponent
{
    private float Timer = 2.586f, CurrentTimer = 2.586f;

    public YLMTDEffect()
    {
        LifeTime = 30;
        Name = "You left me to die";
    }
    public override void Update()
    {
        CurrentTimer += Game.GetDeltaTime();
        if (CurrentTimer >= Timer)
        {
            var act = new RSeqAct_FullScreenPlayer(Game.GetWorldInfo());
            act.bSkippable = true;
            act.TheMovieName = "PDLC_Intro_JKTV";
            act.InputPlay();
            act.EndCinematicMode();
            CurrentTimer -= Timer;
        }
    }
}
