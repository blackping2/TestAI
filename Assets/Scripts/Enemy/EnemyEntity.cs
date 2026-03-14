using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float actionCooldown = 1.5f;
    public float maxHp = 100f;

    [Header("Runtime")]
    public float currentHp;
    public float lastActionTime = -999f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
    }

    public bool IsCooldownReady()
    {
        return Time.time >= lastActionTime + actionCooldown;
    }

    public void MarkActionUsed()
    {
        lastActionTime = Time.time;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public float DistanceTo(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void PlayWalkingAnimation(bool moving)
    {
        animator.SetBool("Walking", moving);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}