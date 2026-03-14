using UnityEngine;

public interface IActionStrategy
{
    void Execute(EnemyEntity self, GameObject targetGem);
    bool IsFinished { get; }
    void ResetAction();
}