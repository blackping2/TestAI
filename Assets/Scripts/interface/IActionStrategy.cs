using UnityEngine;

// AI의 "행동(무엇을 할지)"을 정의하는 인터페이스 (Strategy Pattern)
public interface IActionStrategy
{
    // 행동 실행 (공격, 자폭 등)
    void Execute(EnemyEntity self, GameObject targetGem);

    // 행동 완료 여부 (Action → Idle 전환 판단)
    bool IsFinished { get; }

    // 행동 초기화 (새 행동 시작 전 리셋)
    void ResetAction();
}