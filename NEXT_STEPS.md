# Next Steps - Layer Slice Launch

## 1. GitHub Setup ✅
- Add secrets to your GitHub repo (see GITHUB_SETUP.md)
- Base64 keystore is in your clipboard!

## 2. Push & Build 🚀
```bash
git push origin prototype
```

## 3. Monitor CI Build 👀
- Go to GitHub → Actions tab
- Wait ~20-25 minutes for first build
- Download artifacts:
  - `MarbleCollector-APK` → For device testing
  - `MarbleCollector-AAB` → For Play Store

## 4. Test on Device 📱
```bash
adb install -r MarbleCollector-*-debug.apk
```

## 5. Report Results 📊
```
Layer Slice (Kivy) live – FPS = __, APK = __ MB, issues = __
```

## Game Features 🎮
- 5 pastel layers to slice
- Particle effects
- Haptic feedback (vibration)
- Progress tracking
- Shimmering gem reveal
- Score system

## Polish Ideas 💡
- Combo multiplier for long slices
- Dynamic color palettes
- Better particle effects
- Sound effects
- Leaderboard