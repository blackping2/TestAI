using UnityEngine;

public class RangeAttackCondition : IActionCondition
{
    public bool CanExecute(EnemyEntity self, GameObject targetGem)
    {
        if (self == null || targetGem == null) return false;

        bool inRange = self.DistanceTo(targetGem) <= self.attackRange;
        bool cooldownReady = self.IsCooldownReady();

        return inRange && cooldownReady;
    }
}