## 🧠 Enemy AI System Specification (전략 패턴 기반 디펜스 AI)

### 1. 📌 개요

본 시스템은 디펜스 게임에서 적 AI의 공통 로직과 개별 행동을 분리하기 위해
**FSM(Finite State Machine)**과 **Strategy Pattern**을 결합하여 설계되었다.

* 모든 적은 동일한 상태 구조를 공유한다.
* 각 적의 행동은 전략 객체로 분리하여 유연하게 확장 가능하다.
* 유지보수성과 확장성을 고려하여 OCP(Open-Closed Principle)를 준수한다.

---

### 2. 🎯 핵심 타겟

* 모든 적 AI는 맵 내 단일 오브젝트인 **Gem**을 목표로 행동한다.

---

### 3. 🔄 FSM (Finite State Machine) 구조

모든 적 AI는 아래 3가지 상태를 가진다.

| 상태         | 설명                         |
| ---------- | -------------------------- |
| **Idle**   | 초기 대기 상태                   |
| **Move**   | Gem을 향해 이동                 |
| **Action** | 공격, 자폭, 채널링 등 캐릭터 고유 행동 수행 |

#### 상태 전이 흐름

```text
Idle → Move → Action → Idle 반복
```

#### 전이 조건

* `Idle → Move` : 자동 전이
* `Move → Action` : 조건(`IActionCondition`) 충족 시
* `Action → Idle` : 행동 완료 또는 조건 미충족 시

---

### 4. 🧩 전략 패턴 (Strategy Pattern)

AI의 행동을 조건과 실행 로직으로 분리하여 구현한다.

#### 인터페이스 구조

```csharp
public interface IActionCondition {
    bool CanExecute(Entity self, GameObject targetGem);
}

public interface IActionStrategy {
    void Execute(Entity self, GameObject targetGem);
}
```

---

### 5. ⚙️ 주요 구성 요소

#### 5.1 BaseAI

* 모든 AI의 공통 FSM 로직 관리
* 상태 전이 및 업데이트 루프 처리

#### 5.2 EnemyEntity

* 체력, 공격력, 이동속도 등 스탯 관리
* 애니메이션 제어 (Animator)
* 이동 및 데미지 처리

#### 5.3 Condition (행동 조건)

| 클래스                    | 설명                |
| ---------------------- | ----------------- |
| `RangeAttackCondition` | 사거리 내 진입 + 쿨타임 체크 |

#### 5.4 Strategy (행동 로직)

| 클래스                        | 설명         |
| -------------------------- | ---------- |
| `MeleeAttackStrategy`      | 근접 공격 수행   |


---

### 6. 🔁 동작 흐름

```text
1. 적 생성
2. Gem 방향으로 이동
3. 조건 검사 (CanExecute)
4. 조건 충족 시 Action 실행
5. Action 종료 후 Idle 복귀
6. 반복
```

---

### 7. 🎬 애니메이션 시스템 (Animator)

Animator는 상태 기반으로 애니메이션을 관리한다.

#### 사용 파라미터

| 이름      | 타입      | 설명    |
| ------- | ------- | ----- |
| Walking | bool    | 이동 상태 |
| Attack  | trigger | 공격 실행 |
| Death   | trigger | 사망    |

#### 상태 구조

```text
Idle ↔ Walking
Walking → Attack
Attack → Idle
```

* 조건 기반 전이 사용
* Has Exit Time 최소화하여 즉각 반응

---

### 8. 🧱 시스템 특징

* ✔ FSM + Strategy Pattern 결합 구조
* ✔ AI 로직과 행동 로직 완전 분리
* ✔ 새로운 적 추가 시 기존 코드 수정 불필요 (OCP)
* ✔ 애니메이션과 AI 로직 분리 설계
* ✔ 유지보수 및 확장 용이

---

### 9. 🚀 확장 가능 요소

* 원거리 공격 AI
* 힐러/버퍼 AI
* 보스 패턴 AI (다중 전략)
* 이동 전략 분리 (Pathfinding, 회피 등)
* ScriptableObject 기반 데이터 관리

---


### 10. 💡 설계 의도

* 행동 변경이 잦은 게임 특성을 고려하여 전략 패턴 적용
* FSM으로 상태 흐름을 명확히 분리
* 애니메이션과 로직을 독립적으로 관리하여 협업 효율 향상

---

### 11. 📝 요약

> 본 AI 시스템은 FSM 기반 상태 관리와 전략 패턴을 결합하여
> 적 캐릭터의 행동을 유연하고 확장 가능하게 설계한 디펜스 게임 AI 구조이다.
