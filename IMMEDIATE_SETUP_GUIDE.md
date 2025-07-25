# Immediate Setup Guide for Pocket Digger

## ✅ Status Update

### 1. Scene Configuration
- **Current state**: SampleScene.unity is already the build scene (#0)
- **Action needed**: None - this is correct

### 2. Missing Critical Assets
The game code expects these prefabs that don't exist:
- terrainPrefab
- marblePrefab  
- bucketPrefab

## 🚀 Quick Unity Setup Instructions

### Step 1: Create Basic Prefabs (5 minutes)
1. Open Unity and load SampleScene.unity
2. Create these GameObjects in the hierarchy:

#### Marble Prefab:
```
GameObject > 2D Object > Sprite/Circle
- Name: MarblePrefab
- Add Component: Rigidbody2D (Dynamic, Gravity Scale: 1)
- Add Component: CircleCollider2D
- Add Component: Marble (script)
- Scale: (0.3, 0.3, 1)
- Layer: Default
- Save as Prefab in Assets/Prefabs/
```

#### Bucket Prefab:
```
GameObject > 2D Object > Sprite/Square
- Name: BucketPrefab
- Add Component: BoxCollider2D (Is Trigger: true)
- Tag: Goal
- Scale: (2, 0.5, 1)
- Save as Prefab in Assets/Prefabs/
```

#### Terrain Prefab:
```
GameObject > 2D Object > Sprite/Square
- Name: TerrainPrefab
- Add Component: D2dDestructible (from Destructible2D)
- Add Component: D2dCollider
- Tag: Terrain
- Layer: Default
- Color: Brown (#8B4513)
- Save as Prefab in Assets/Prefabs/
```

### Step 2: Connect Prefabs to LevelGenerator
1. Find LevelGenerator in the scene
2. Drag the created prefabs to:
   - Terrain Prefab slot
   - Marble Prefab slot
   - Bucket Prefab slot

### Step 3: Quick Visual Polish
1. **Camera Background**: 
   - Main Camera > Background Color: #C8E7FF (sky blue)

2. **Score Text**:
   - Find ScoreText in hierarchy
   - Anchor: Top Center
   - Font: LiberationSans SDF
   - Color: White
   - Outline: 0.2

### Step 4: Build AAB
```bash
# After Unity setup:
1. File > Build Settings
2. Android platform
3. ✓ Build App Bundle (Google Play)
4. Player Settings verify:
   - Package: com.eshmalo.pocketdigger
   - Min API: 21
   - Target API: 34
5. Build > Save as: PocketDigger-0.1-int.aab
```

## 📝 Commit After Unity Changes
```bash
git add -A
git commit -m "Add basic prefabs and visual setup for Internal Testing"
git push
```

## 🎯 Next Steps
1. Upload AAB to Google Play Console Internal Testing
2. Add tester emails
3. Fill privacy policy (use GitHub Gist: "We collect no data")
4. Roll out and wait 30 minutes

---

**Note**: Since I can't create Unity prefabs via CLI, you'll need to do these steps in the Unity Editor. This should take about 15-20 minutes total.