using UnityEngine;

// 근접 공격을 수행하는 전략 클래스
public class MeleeAttackStrategy : IActionStrategy
{
    public bool IsFinished { get; private set; } // 행동 완료 여부

    public void Execute(EnemyEntity self, GameObject targetGem)
    {
        // 이미 실행된 경우 중복 실행 방지
        if (IsFinished) return;

        // 공격 애니메이션 실행
        self.PlayAttackAnimation();

        // Gem에 데미지 적용
        Gem gem = targetGem.GetComponent<Gem>();
        if (gem != null)
        {
            gem.TakeDamage(self.attackDamage);
        }

        // 행동 완료 처리
        IsFinished = true;
    }

    public void ResetAction()
    {
        // 다음 행동을 위해 상태 초기화
        IsFinished = false;
    }
}