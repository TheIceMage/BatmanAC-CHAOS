using static ChaosScript;

namespace Chaos.Effects;

public class PacifistEffect : ChaosComponent
{
    public PacifistEffect()
    {
        Name = "Pacifist Enemies";
        LifeTime = 90;
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
