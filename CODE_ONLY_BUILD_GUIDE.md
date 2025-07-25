# Code-Only Unity Build Guide

## 🚀 Zero Editor Work Required!

This project now runs entirely from code - no Unity Editor setup needed.

### How It Works
- `GameBootstrap.cs` uses `[RuntimeInitializeOnLoadMethod]` to auto-run when Unity starts
- All prefabs, sprites, and UI are generated procedurally in code
- No inspector fields or scene editing required

### To Build:
1. Open Unity 2021.3.21f1
2. Press Play to test (everything auto-generates)
3. File > Build Settings > Android > ✓ Build App Bundle
4. Build as: PocketDigger-0.1-int.aab

### What Gets Created Automatically:
- Camera with sky blue background
- Score UI with TextMeshPro
- Marble prefab (white circle with physics)
- Terrain (brown square with Destructible2D)
- Goal bucket (dark brown rectangle)
- Dig controller (mouse/touch input)
- Marble spawner (every 0.4 seconds)
- Win UI ("Level Complete!" message)

### No Manual Steps Required:
- ❌ No prefab creation
- ❌ No scene editing
- ❌ No inspector setup
- ✅ Just open, play, and build!

The entire game bootstraps from code when SampleScene loads.