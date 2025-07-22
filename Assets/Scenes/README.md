# Scene Setup Instructions

Create `Level01.unity` in Unity Editor with:

1. **Terrain Sprite**
   - Add Destructible 2D package
   - Create sprite with D2D Destructible component
   - Add D2D Collider component

2. **Marble Spawner**
   - Create empty GameObject "MarbleSpawner"
   - Position at top of scene
   - Add spawning script (25 marbles per 10 seconds)

3. **Goal Bucket**
   - Create sprite with "Goal" tag
   - Add Collider2D with IsTrigger = true
   - Position at bottom of scene

4. **Camera Setup**
   - Orthographic projection
   - Size adjusted to fit mobile aspect ratios