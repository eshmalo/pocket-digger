# Google Play Store Upload Guide

## Step 1: Get Your AAB File

### From GitHub Actions:
1. Go to your GitHub repo → Actions tab
2. Click on the latest build workflow
3. Download `MarbleCollector-AAB` artifact
4. Unzip to get `layerslice-0.1-arm64-v8a_armeabi-v7a-release.aab`

### Or build locally (if CI fails):
```bash
./build_android.sh  # or ./build_docker.sh
# AAB will be in bin/ directory
```

## Step 2: Google Play Console

### Create New App:
1. Go to [Google Play Console](https://play.google.com/console)
2. Click "Create app"
3. Fill in:
   - App name: **Layer Slice**
   - Default language: English (US)
   - App or game: Game
   - Free or paid: Free
   - Accept declarations

### Complete Setup Tasks:

#### 1. App Access
- Select: "All functionality available without special access"

#### 2. Content Rating
- Start questionnaire
- Category: Game
- Violence: None
- Other content: None
- Submit → Get "Everyone" rating

#### 3. Target Audience
- Age groups: 13-15, 16-17, 18+
- Appeal to children: No

#### 4. News Apps
- Is this a news app?: No

#### 5. App Category
- Category: Casual
- Tags: Puzzle, Satisfying, ASMR

#### 6. Store Listing
Required fields:
- **App name**: Layer Slice
- **Short description** (80 chars):
  "Slice through colorful layers to reveal the hidden gem!"
  
- **Full description** (4000 chars):
  ```
  Layer Slice is a satisfying puzzle game where you slice through vibrant colored layers to uncover a shimmering gem hidden beneath.

  Features:
  • Simple one-touch gameplay
  • 5 beautiful pastel layers to slice through
  • Particle effects and haptic feedback
  • Progress tracking
  • Score system with combo bonuses
  • Relaxing ASMR-style gameplay

  Perfect for quick sessions or extended play. No ads, no in-app purchases - just pure slicing satisfaction!
  ```

- **App icon**: Create 512x512 PNG
- **Feature graphic**: 1024x500 PNG
- **Screenshots**: At least 2 (phone), up to 8
  - Recommended: 1080x1920 or 1440x2560

## Step 3: Upload AAB

### Testing → Internal testing:
1. Create new release
2. Upload your AAB file
3. Release name: "Version 1.0"
4. Release notes:
   ```
   Initial release
   - Layer slicing gameplay
   - Haptic feedback
   - Particle effects
   ```
5. Save → Review → Start rollout

## Step 4: Privacy Policy

Add URL: `https://raw.githubusercontent.com/YOUR_USERNAME/YOUR_REPO/main/PRIVACY_POLICY.md`

Create PRIVACY_POLICY.md:
```markdown
# Privacy Policy for Layer Slice

Layer Slice does not collect any personal data.

## Information Collection
- No personal information is collected
- No analytics or tracking
- No internet permissions required

## Contact
If you have questions: [your-email]

Last updated: [date]
```

## Step 5: Testing & Production

### Internal Testing (Immediate):
- Add tester emails
- They get link in ~15 minutes
- Gather feedback for 24-48 hours

### Closed Testing (Optional):
- Larger group (up to 10,000)
- Can run alongside internal

### Production Release:
1. Testing → Production
2. Create new release
3. Use same AAB from testing
4. Add release notes
5. Review → Start rollout

## Timelines

| Stage | Time |
|-------|------|
| Internal testing available | 15-30 minutes |
| Closed testing | 2-24 hours |
| Production review | 2-24 hours |
| Live on Play Store | Within 3 hours of approval |

## Quick App Icon

Create a simple icon (save as icon.png):
```python
from PIL import Image, ImageDraw

# Create icon
img = Image.new('RGBA', (512, 512), (30, 30, 30, 255))
draw = ImageDraw.Draw(img)

# Draw layers
colors = [(255, 179, 179), (255, 230, 179), (255, 255, 179), 
          (179, 255, 179), (179, 179, 255)]
for i, color in enumerate(colors):
    y = 100 + i * 60
    draw.rectangle([100, y, 412, y + 50], fill=color)

# Save
img.save('icon.png')
```

## Common Issues

**"Version code 1 has already been used"**
- Update version in buildozer.spec:
  ```ini
  version = 0.2
  ```

**"App bundle is signed with wrong key"**
- Make sure using same keystore as initial upload

**"Target API level requirement"**
- buildozer.spec already set to API 33 ✓

Ready to upload! The AAB from GitHub Actions is signed and ready for Play Store.