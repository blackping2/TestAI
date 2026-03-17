using UnityEngine;

// BaseAI는 모든 적 AI의 "공통 FSM 로직"을 담당하는 클래스
// 각 몬스터는 이 클래스를 상속받고,
// 행동은 Strategy Pattern으로 외부에서 주입받는다.
public class BaseAI : MonoBehaviour
{
    // 현재 AI 상태 (Idle / Move / Action)
    protected AIState currentState = AIState.Idle;

    // 자기 자신 (EnemyEntity: 이동, 체력, 애니메이션 담당)
    protected EnemyEntity self;

    // AI의 목표 대상 (디펜스 게임에서는 Gem)
    protected GameObject targetGem;

    // 행동 조건 (언제 행동할지 결정)
    protected IActionCondition actionCondition;

    // 행동 전략 (무엇을 할지 결정)
    protected IActionStrategy actionStrategy;

    protected virtual void Awake()
    {
        // EnemyEntity 컴포넌트 참조
        self = GetComponent<EnemyEntity>();
    }

    protected virtual void Start()
    {
        // GameManager에서 Gem 객체를 가져옴 (싱글톤)
        targetGem = GameManager.Instance.GemObject;
    }

    protected virtual void Update()
    {
        // 매 프레임 상태 업데이트
        UpdateState();
    }

    // 현재 상태에 따라 각 상태 함수 실행
    protected virtual void UpdateState()
    {
        if (targetGem == null) return;

        switch (currentState)
        {
            case AIState.Idle:
                UpdateIdle();
                break;

            case AIState.Move:
                UpdateMove();
                break;

            case AIState.Action:
                UpdateAction();
                break;
        }
    }

    // Idle 상태 (대기 상태)
    protected virtual void UpdateIdle()
    {
        // 이동하지 않으므로 Walking 애니메이션 OFF
        self.PlayWalkingAnimation(false);

        // 기본적으로 바로 Move 상태로 전환
        currentState = AIState.Move;
    }

    // Move 상태 (Gem을 향해 이동)
    protected virtual void UpdateMove()
    {
        // 행동 조건을 만족하면 Action 상태로 전환
        if (actionCondition != null && actionCondition.CanExecute(self, targetGem))
        {
            // 새로운 행동 시작 전 상태 초기화
            actionStrategy?.ResetAction();

            currentState = AIState.Action;
            return;
        }

        // Gem 방향으로 이동
        self.MoveTo(targetGem.transform.position);

        // 이동 애니메이션 ON
        self.PlayWalkingAnimation(true);
    }

    // Action 상태 (공격, 자폭 등 행동 수행)
    protected virtual void UpdateAction()
    {
        // 조건 또는 전략이 없으면 실행 불가
        if (actionCondition == null || actionStrategy == null)
            return;

        // 조건이 더 이상 만족되지 않으면 Idle로 복귀
        if (!actionCondition.CanExecute(self, targetGem))
        {
            currentState = AIState.Idle;
            return;
        }

        // 행동 실행 (Strategy Pattern)
        actionStrategy.Execute(self, targetGem);

        // 행동이 완료되면 Idle로 복귀
        if (actionStrategy.IsFinished)
        {
            currentState = AIState.Idle;
        }
    }

    // 외부에서 행동 조건과 전략을 주입
    // → 몬스터마다 다른 행동을 적용 가능 (OCP 준수)
    public void SetStrategy(IActionCondition condition, IActionStrategy strategy)
    {
        actionCondition = condition;
        actionStrategy = strategy;
    }
}