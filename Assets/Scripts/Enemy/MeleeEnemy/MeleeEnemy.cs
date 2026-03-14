using UnityEngine;

public class MeleeEnemyAI : BaseAI
{
    protected override void Start()
    {
        base.Start();

        IActionCondition condition = new RangeAttackCondition();
        IActionStrategy strategy = new MeleeAttackStrategy();

        SetStrategy(condition, strategy);
    }
}