# 🚀 Google Play Store Setup - Quick Guide

## 1. Create Keystore in Unity (5 min)
1. Open Unity 2021.3.21f1
2. **File → Build Settings → Android → Player Settings**
3. **Publishing Settings → Keystore Manager → Create New**
   - Keystore name: `pocket-digger`
   - Password: `pocket123` (or choose your own)
   - Alias: `pocket-digger`
   - Validity: 50 years

## 2. Configure Build Settings (5 min)
| Setting | Value |
|---------|-------|
| **Company Name** | EshMalo |
| **Product Name** | Pocket Digger |
| **Package Name** | `com.eshmalo.pocketdigger` |
| **Version** | 0.1.0 |
| **Bundle Version Code** | 1 |
| **Minimum API** | 21 (Android 5.0) |
| **Target API** | 34 (Android 14) |
| **Build Type** | ✓ Build App Bundle (Google Play) |

## 3. Build AAB File (10 min)
1. **File → Build Settings → Build**
2. Name: `PocketDigger.aab`
3. Wait for build to complete

## 4. Google Play Console (15 min)
1. Go to: https://play.google.com/console
2. Pay $25 fee (one-time)
3. **Create app**:
   - App name: Pocket Digger
   - Default language: English
   - App type: Game
   - Category: Casual
   - Free/Paid: Free

## 5. Upload to Internal Testing
1. **Testing → Internal testing → Create new release**
2. Upload `PocketDigger.aab`
3. Release name: `0.1.0-alpha`
4. Release notes: "Initial test build"

## 6. Complete Required Info
- **Content rating**: IARC questionnaire
- **Data safety**: No data collected (for now)
- **App content**: No ads, no login
- **Target audience**: 13+

## 7. Add Testers
- Internal testing → Testers → Add email list
- Add: shmalo.elazar@gmail.com
- Share opt-in link

---

## 🎯 You'll be live in 2 hours!

The AAB file is what matters - everything else can be updated later!