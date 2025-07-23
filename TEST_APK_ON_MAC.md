# Testing Android APK on MacBook

## Option 1: Android Studio Emulator (Recommended)
**Time: 5-10 min setup**

1. **Install Android Studio** (if not already installed):
   ```bash
   brew install --cask android-studio
   ```

2. **Create AVD (Android Virtual Device)**:
   - Open Android Studio
   - Tools → AVD Manager → Create Virtual Device
   - Choose: Pixel 6 (or any phone)
   - System Image: Android 13 (API 33)
   - Finish

3. **Install APK**:
   ```bash
   # Start emulator
   emulator -avd Pixel_6_API_33
   
   # Install APK (in new terminal)
   adb install PocketDigger.apk
   ```

## Option 2: Android Studio Built-in Emulator
1. Open Android Studio
2. Click "Profile or debug APK" from welcome screen
3. Select your downloaded APK
4. Android Studio will launch it in emulator

## Option 3: BlueStacks (Easiest)
**Time: 3 min**

1. Download BlueStacks: https://www.bluestacks.com
2. Install and launch
3. Drag and drop the APK onto BlueStacks window
4. App installs and runs automatically

## Option 4: Unity Editor Play Mode (Fastest)
**For quick testing without APK:**

1. Open project in Unity 2022.3.24f1
2. File → Build Settings → Switch Platform to Android
3. Hit Play button in Unity Editor
4. Test with mouse (simulates touch)

## Option 5: Chrome DevTools (Web-based)
**If you build a WebGL version:**
- Can test touch simulation
- Better for UI/UX testing

## Performance Notes
- Emulators won't give accurate FPS readings
- For real performance metrics, use physical device
- Unity Editor gives closest-to-device performance on Mac

## Quick Commands Cheat Sheet
```bash
# Check if emulator is running
adb devices

# Install APK
adb install path/to/PocketDigger.apk

# Uninstall old version
adb uninstall com.DefaultCompany.PocketDigger

# View logs
adb logcat | grep Unity
```