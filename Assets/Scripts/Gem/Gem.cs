using UnityEngine;

public class Gem : MonoBehaviour
{
    public float maxHp = 500f;
    public float currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"Gem damaged: {damage}, Remaining HP: {currentHp}");

        if (currentHp <= 0f)
        {
            Debug.Log("Gem Destroyed!");
        }
    }
}