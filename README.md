# Hell of Bullets

A top-down 2D bullet hell game built with Unity (minimum **Unity 6.5.6f1**). You control a character, move across a tilemap arena, and fight swarms of pooled enemies.

## Architecture Overview

Game code lives under `Assets/HellOfBullets/Scripts/` and is split by responsibility:

```
Scripts/
├── Abstracts/             # AEntityData — base ScriptableObject for entity data
├── DataScriptableObjects/ # HealthData, EnemyData, PlayerData (tunable stats)
├── Interfaces/            # IDamagable, IDamager, IHealthInit
├── Components/            # Reusable behaviours (health, movement, damage flash)
├── Enemies/               # Enemy base class
├── ObjectPooling/         # EnemyPool — pre-instantiated enemy pool
└── Weapons/               # LaserBullet
```

### Data-driven design
Entity stats (max health, detection range, attack cooldown, movement speed) live in **Scriptable Object** assets (`Assets/HellOfBullets/Data/`), not in code. New enemy types are configured with data assets rather than new scripts.

### Damage flow
- `IDamager` — marker for anything that deals damage.
- `IDamagable` — `TakeDamage(float)` / `Die()`, implemented by `HealthComponent`.
- `IHealthInit` — `InitializeHealth()`, used to re-arm enemies when recycled from the pool.
- Weapons (e.g. `LaserBullet`) resolve `IDamagable` from collisions — damage sources never reference concrete enemy types.

### Components
| Component | Responsibility |
|---|---|
| `HealthComponent` | Health state, death, damage flash trigger; implements `IDamagable` / `IHealthInit` |
| `DamageFlashComponent` | Coroutine-driven shader flash (`_DamageFlashAmount`) on hit |
| `PlayerMovementComponent` | Input-driven movement + camera follow (Unity Input System) |
| `EnemyCenterMovementComponent` | Base "seek player" behaviour for enemies |

### Object pooling
`EnemyPool` pre-instantiates a pool of enemies under a `[EnemyPool]` parent GameObject and reuses them instead of instantiating/destroying per spawn, avoiding GC and instantiation cost at bullet-hell enemy counts. Pooled enemies are re-initialized through `IHealthInit`.

### Input
Uses the new **Unity Input System** with actions defined in `Assets/Settings/InputSystem_Actions.inputactions`.

## Controls & Gameplay

| Action | Input |
|---|---|
| Move | WASD / Arrow keys |
| Attack | Left Mouse Button (only for testing purposes) |

- The main scene is `Assets/HellOfBullets/Scenes/Main.unity` — open it and press Play.
- The camera follows the player; the arena is rendered with animated tilemap tiles (custom Shader Graph in `Assets/HellOfBullets/Shaders/`).
