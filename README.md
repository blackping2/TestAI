# 🧠 Enemy AI System (FSM + Strategy Pattern)

디펜스 게임의 적 AI를 **FSM(Finite State Machine)**과 **Strategy Pattern**을 결합하여 설계한 시스템입니다.
공통 상태 관리와 개별 행동 로직을 분리하여 **확장성, 유지보수성, 재사용성**을 극대화하는 것을 목표로 합니다.

---

## 📌 Overview

기존 AI 구조는 행동이 코드에 강하게 결합되어 있어
몬스터 종류가 늘어날수록 유지보수가 어려워지는 문제가 있습니다.

본 시스템은 다음과 같은 설계를 통해 이를 해결합니다:

* ✔ 모든 AI는 동일한 상태 흐름(FSM)을 공유
* ✔ 행동 로직은 Strategy Pattern으로 분리
* ✔ 새로운 몬스터 추가 시 기존 코드 수정 없음 (OCP)

---

## 🎯 Target

* 모든 적 AI는 맵 내 단일 오브젝트인 **Gem**을 목표로 동작합니다.

---

## 🔄 FSM Architecture

모든 적은 아래의 공통 상태를 가집니다.

| State      | Description       |
| ---------- | ----------------- |
| **Idle**   | 초기 대기 상태          |
| **Move**   | Gem을 향해 이동        |
| **Action** | 공격, 자폭 등 고유 행동 수행 |

### State Flow

```text
Idle → Move → Action → Idle (Loop)
```

### Transition Conditions

* `Idle → Move` : 자동 전이
* `Move → Action` : 행동 조건 만족 시
* `Action → Idle` : 행동 종료 시

---

## 🧩 Strategy Pattern

AI 행동을 **조건(Condition)**과 **행동(Strategy)**으로 분리하여 구현합니다.

### Interface

```csharp
public interface IActionCondition {
    bool CanExecute(Entity self, GameObject targetGem);
}

public interface IActionStrategy {
    void Execute(Entity self, GameObject targetGem);
}
```

---

## ⚙️ Core Components

### 🔹 BaseAI

* FSM 상태 관리
* 상태 전이 및 Update 루프 처리

### 🔹 EnemyEntity

* 체력, 이동속도, 공격력 관리
* 애니메이션 제어 (Animator)
* 이동 및 데미지 처리

### 🔹 Condition

| Class                  | Description  |
| ---------------------- | ------------ |
| `RangeAttackCondition` | 사거리 + 쿨타임 체크 |

### 🔹 Strategy

| Class                 | Description |
| --------------------- | ----------- |
| `MeleeAttackStrategy` | 근접 공격 수행    |

---

## 🔁 Execution Flow

```text
1. Enemy Spawn
2. Move toward Gem
3. Check Condition (CanExecute)
4. Execute Strategy
5. Return to Idle
6. Repeat
```

---

## 🎬 Animation System

애니메이션은 Unity **Animator State Machine**을 사용하여 관리됩니다.

### 🔸 Key Concept

* AI는 **상태(신호)**만 전달
* Animator는 **애니메이션 전환을 담당**

👉 AI와 애니메이션을 완전히 분리한 구조

---

### Parameters

| Name    | Type    | Description |
| ------- | ------- | ----------- |
| Walking | bool    | 이동 상태       |
| Attack  | trigger | 공격 실행       |
| Death   | trigger | 사망          |

---

### Animator Flow

```text
Idle ↔ Walking
Walking → Attack
Attack → Idle
```

---

## 🧱 System Features

* ✔ FSM + Strategy Pattern 결합
* ✔ AI 로직과 행동 로직 분리
* ✔ 애니메이션과 로직 분리
* ✔ 확장성 높은 구조
* ✔ 유지보수 용이

---

## 🚀 Extensibility

다음과 같은 기능 확장이 가능합니다:

* 원거리 공격 AI
* 자폭형 몬스터
* 힐러 / 버퍼 AI
* 보스 패턴 AI (멀티 전략)
* 이동 전략 분리 (Pathfinding)

---

## 💡 Design Philosophy

* 행동 변경이 잦은 게임 특성을 고려하여 Strategy Pattern 적용
* FSM을 통해 상태 흐름을 명확히 관리
* 애니메이션 시스템과 로직을 분리하여 협업 효율 향상

---

## 🧪 Example Explanation (For Presentation)

> 본 AI 시스템은 FSM과 Strategy Pattern을 결합하여 설계되었습니다.
> 모든 적은 Idle, Move, Action의 공통 상태를 가지며 FSM으로 관리됩니다.
> 각 몬스터의 행동은 인터페이스 기반 전략 패턴으로 분리되어
> 다양한 행동을 코드 수정 없이 확장할 수 있습니다.
>
> 또한 애니메이션은 Animator 상태 머신을 통해
> AI 로직과 분리된 형태로 동작하도록 구성했습니다.

---

## 📝 Summary

> FSM으로 상태 흐름을 관리하고,
> Strategy Pattern으로 행동을 분리한
> 확장 가능한 디펜스 게임 AI 시스템입니다.
