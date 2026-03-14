using UnityEngine;

public class MeleeAttackStrategy : IActionStrategy
{
    public bool IsFinished { get; private set; }

    public void Execute(EnemyEntity self, GameObject targetGem)
    {
        if (IsFinished) return;

        self.PlayAttackAnimation();

        Gem gem = targetGem.GetComponent<Gem>();

        if (gem != null)
        {
            gem.TakeDamage(self.attackDamage);
        }

        IsFinished = true;
    }

    public void ResetAction()
    {
        IsFinished = false;
    }
}