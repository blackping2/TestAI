using UnityEngine;

public interface IActionCondition
{
    bool CanExecute(EnemyEntity self, GameObject targetGem);
}
