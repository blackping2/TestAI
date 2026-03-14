using UnityEngine;

public class BaseAI : MonoBehaviour
{
    protected AIState currentState = AIState.Idle;

    protected EnemyEntity self;
    protected GameObject targetGem;

    protected IActionCondition actionCondition;
    protected IActionStrategy actionStrategy;

    protected virtual void Awake()
    {
        self = GetComponent<EnemyEntity>();
    }

    protected virtual void Start()
    {
        targetGem = GameManager.Instance.GemObject;
    }

    protected virtual void Update()
    {
        UpdateState();
    }

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

    protected virtual void UpdateIdle()
    {
        self.PlayWalkingAnimation(false);
        currentState = AIState.Move;
    }

    protected virtual void UpdateMove()
    {
        if (actionCondition != null && actionCondition.CanExecute(self, targetGem))
        {
            actionStrategy?.ResetAction();
            currentState = AIState.Action;
            return;
        }

        self.MoveTo(targetGem.transform.position);

        // 이동 애니메이션
        self.PlayWalkingAnimation(true);
    }

    protected virtual void UpdateAction()
    {
        if (actionCondition == null || actionStrategy == null)
            return;

        if (!actionCondition.CanExecute(self, targetGem))
        {
            currentState = AIState.Idle;
            return;
        }

        actionStrategy.Execute(self, targetGem);

        if (actionStrategy.IsFinished)
        {
            currentState = AIState.Idle;
        }
    }

    public void SetStrategy(IActionCondition condition, IActionStrategy strategy)
    {
        actionCondition = condition;
        actionStrategy = strategy;
    }
}