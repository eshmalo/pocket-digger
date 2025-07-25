# Build Checklist for Pocket Digger

## 1. Open Unity
- Unity Hub → Open → Select this project folder
- Unity version: 2021.3.21f1

## 2. Build Settings
- File → Build Settings
- Platform: Android (click "Switch Platform" if needed)
- Scenes in Build: ✓ SampleScene (should be checked)

## 3. Player Settings (IMPORTANT!)
File → Build Settings → Player Settings:

### Company & Product
- Company Name: EshMalo ✓
- Product Name: Pocket Digger ✓
- Package Name: com.eshmalo.pocketdigger ✓

### Other Settings
- Minimum API Level: Android 5.0 'Lollipop' (API 21) ✓
- Target API Level: API Level 34 ✓

### Publishing Settings
- ✓ Custom Main Keystore
- Browse: Select `pocket-digger.keystore` from project folder
- Keystore password: pocket123
- Alias: pocket-digger
- Alias password: pocket123

## 4. First Build (TEST APK)
- Build Settings → Build
- Name: PocketDigger-test.apk
- Location: Desktop

## 5. Test the APK
- Email it to yourself
- Install on any Android phone
- OR: Open in Android Studio → Profile or debug APK

## 6. Build for Google Play (AAB)
- Build Settings → ✓ Build App Bundle (Google Play)
- Build → Name: PocketDigger.aab
- This is what you upload to Play Console!