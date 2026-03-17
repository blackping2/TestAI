using UnityEngine;

// EnemyEntity는 적 캐릭터의 "데이터 + 기본 기능"을 담당하는 클래스
// AI(BaseAI)는 상태와 로직을 관리하고,
// 이 클래스는 실제 이동, 데미지, 애니메이션을 처리한다.
public class EnemyEntity : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;          // 이동 속도
    public float attackRange = 1.5f;      // 공격 가능 거리
    public float attackDamage = 10f;      // 공격 데미지
    public float actionCooldown = 1.5f;   // 행동 쿨타임
    public float maxHp = 100f;            // 최대 체력

    [Header("Runtime")]
    public float currentHp;               // 현재 체력
    public float lastActionTime = -999f;  // 마지막 행동 시간 (쿨타임 계산용)

    private Animator animator;            // 애니메이션 제어 컴포넌트

    private void Awake()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 시작 시 현재 체력을 최대 체력으로 초기화
        currentHp = maxHp;
    }

    // 현재 쿨타임이 끝났는지 확인
    // → 일정 시간마다만 공격/행동 가능하도록 제한
    public bool IsCooldownReady()
    {
        return Time.time >= lastActionTime + actionCooldown;
    }

    // 행동을 수행했을 때 호출
    // → 마지막 행동 시간을 현재 시간으로 갱신
    public void MarkActionUsed()
    {
        lastActionTime = Time.time;
    }

    // 목표 위치로 이동하는 함수
    // → AI(BaseAI)에서 호출됨
    public void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    // 특정 대상과의 거리 계산
    // → 공격 가능 여부 판단에 사용
    public float DistanceTo(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position);
    }

    // 데미지를 받는 함수
    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        // 체력이 0 이하가 되면 사망 처리
        if (currentHp <= 0f)
        {
            Die();
        }
    }

    // 사망 처리
    public void Die()
    {
        // 현재는 즉시 제거
        // → 추후 Death 애니메이션, 이펙트 추가 가능
        Destroy(gameObject);
    }

    // 이동 애니메이션 제어
    // → Walking 파라미터를 통해 Animator 상태 전이 발생
    public void PlayWalkingAnimation(bool moving)
    {
        animator.SetBool("Walking", moving);
    }

    // 공격 애니메이션 실행
    // → Trigger를 사용하여 Attack 상태로 전이
    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }
}