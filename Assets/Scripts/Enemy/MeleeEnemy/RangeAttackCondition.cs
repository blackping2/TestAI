using UnityEngine;

// 사거리 내 진입 + 쿨타임 완료 시 행동 가능하도록 판단하는 조건
public class RangeAttackCondition : IActionCondition
{
    public bool CanExecute(EnemyEntity self, GameObject targetGem)
    {
        // 유효성 체크
        if (self == null || targetGem == null) return false;

        // 공격 범위 내인지 확인
        bool inRange = self.DistanceTo(targetGem) <= self.attackRange;

        // 쿨타임이 끝났는지 확인
        bool cooldownReady = self.IsCooldownReady();

        // 두 조건을 모두 만족해야 행동 가능
        return inRange && cooldownReady;
    }
}