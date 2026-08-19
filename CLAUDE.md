# CLAUDE.md

This file provides guidance to Claude Code when working with code in this repository.
Behavioral guidelines to reduce common LLM coding mistakes. Merge with project-specific instructions as needed.

**Tradeoff:** These guidelines bias toward caution over speed. For trivial tasks, use judgment.

## 1. Think Before Coding

**Don't assume. Don't hide confusion. Surface tradeoffs.**

Before implementing:
- State your assumptions explicitly. If uncertain, ask.
- If multiple interpretations exist, present them - don't pick silently.
- If a simpler approach exists, say so. Push back when warranted.
- If something is unclear, stop. Name what's confusing. Ask.

## 2. Simplicity First

**Minimum code that solves the problem. Nothing speculative.**

- No features beyond what was asked.
- No abstractions for single-use code.
- No "flexibility" or "configurability" that wasn't requested.
- No error handling for impossible scenarios.
- If you write 200 lines and it could be 50, rewrite it.

Ask yourself: "Would a senior engineer say this is overcomplicated?" If yes, simplify.

## 3. Surgical Changes

**Touch only what you must. Clean up only your own mess.**

When editing existing code:
- Don't "improve" adjacent code, comments, or formatting.
- Don't refactor things that aren't broken.
- Match existing style, even if you'd do it differently.
- If you notice unrelated dead code, mention it - don't delete it.

When your changes create orphans:
- Remove imports/variables/functions that YOUR changes made unused.
- Don't remove pre-existing dead code unless asked.

The test: Every changed line should trace directly to the user's request.

## 4. Goal-Driven Execution

**Define success criteria. Loop until verified.**

Transform tasks into verifiable goals:
- "Add validation" → "Write tests for invalid inputs, then make them pass"
- "Fix the bug" → "Write a test that reproduces it, then make it pass"
- "Refactor X" → "Ensure tests pass before and after"

For multi-step tasks, state a brief plan:
```
1. [Step] → verify: [check]
2. [Step] → verify: [check]
3. [Step] → verify: [check]
```

Strong success criteria let you loop independently. Weak criteria ("make it work") require constant clarification.


## Project Overview

This is a Unity 3D project for a bullet hell game, likely named "Hell of Bullets". The project features:
- Object pooling system for enemies
- Input system using Unity's new Input System package
- Basic enemy management and spawning mechanics
- Tilemap-based level design
- Pixel art character assets

## Key Components

### Project Structure
```
Assets/
├── HellOfBullets/              # Main game code
│   ├── Scripts/
│   │   ├── Enemies/            # Enemy-related scripts
│   │   ├── ObjectPooling/      # Object pooling system (EnemyPool.cs)
│   │   └── Interfaces/         # Interfaces (IDamagable.cs)
├── Settings/                   # Project settings
│   └── InputSystem_Actions.inputactions  # Input action definitions
└── Cainos/                     # Pixel art assets
```

### Core Systems

#### Input System
The project uses Unity's new Input System with a defined input action asset. Key actions include:
- Move: Vector2 value for character movement
- Attack: Button action for shooting
- Jump, Crouch, Sprint, etc.

#### Object Pooling
The `EnemyPool.cs` implements an object pooling pattern to efficiently manage enemy instances:
- Pre-creates a pool of enemies at startup
- Reuses existing enemy objects instead of instantiating new ones
- Uses a static singleton pattern for access

## Development Setup

### Building and Running
1. Open the project in Unity 2021.3 or later (based on the Input System version)
2. Ensure all packages are imported:
   - Input System package (version 1.20.0 or later)
   - Any required 2D packages for tilemap functionality
3. The main scene is located at `Assets/HellOfBullets/Scenes/Main.unity`

### Key Files to Understand

1. **Assets/HellOfBullets/Scripts/ObjectPooling/EnemyPool.cs** - Core object pooling logic
2. **Assets/HellOfBullets/Scripts/Enemies/Enemy.cs** - Base enemy class (currently empty)
3. **Assets/HellOfBullets/Scripts/Interfaces/IDamagable.cs** - Damage interface definition
4. **Assets/Settings/InputSystem_Actions.inputactions** - Input action definitions

### Common Development Tasks

1. **Adding new enemy types**: Create new scripts inheriting from Enemy class and modify the pool to use different prefabs
2. **Modifying input controls**: Edit `InputSystem_Actions.inputactions` file to change key bindings or add new actions
3. **Tuning object pooling**: Adjust `_poolSize` in `EnemyPool.cs` based on performance requirements
4. **Adding damage mechanics**: Implement the `IDamagable` interface on enemy classes

## Architecture Notes

- The project uses a singleton pattern for the enemy pool, which means there should only be one instance of EnemyPool in the game
- Input handling is done through Unity's new Input System with proper action mapping
- The scene uses tilemaps for level rendering
- The project appears to be using a modular architecture where different components (enemies, pooling, input) are loosely coupled

## Testing and Debugging

To debug input issues:
1. Check that the `InputSystem_Actions` asset is properly configured
2. Verify that input actions are enabled in the scene
3. Use Unity's Input Debugger window to monitor input events

To debug object pooling:
1. Monitor the pool size in EnemyPool.cs 
2. Check that enemies are being correctly activated and deactivated
3. Ensure no memory leaks from unmanaged objects

## Future Enhancements

Consider implementing:
- Enemy AI behaviors
- Different enemy types with varying behaviors
- Power-ups and special abilities
- Particle effects for shooting/impact
- Sound effects and music
- Level progression system