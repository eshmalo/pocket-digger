#!/bin/bash

echo "Quick Android Studio Emulator Setup"
echo "==================================="

# Set Android SDK path (default for Android Studio on Mac)
export ANDROID_HOME="$HOME/Library/Android/sdk"
export PATH="$ANDROID_HOME/emulator:$ANDROID_HOME/tools:$ANDROID_HOME/tools/bin:$ANDROID_HOME/platform-tools:$PATH"

# Check if adb is available
if ! command -v adb &> /dev/null; then
    echo "❌ ADB not found in PATH"
    echo "Open Android Studio → Preferences → Appearance & Behavior → System Settings → Android SDK"
    echo "Install 'Android SDK Platform-Tools'"
    exit 1
fi

echo "✅ ADB found at: $(which adb)"

# List available emulators
echo ""
echo "Available emulators:"
$ANDROID_HOME/emulator/emulator -list-avds

echo ""
echo "To start an emulator:"
echo "  $ANDROID_HOME/emulator/emulator -avd <AVD_NAME>"
echo ""
echo "To install APK:"
echo "  adb install path/to/PocketDigger.apk"
echo ""
echo "Or just open Android Studio and use 'Profile or debug APK' from the welcome screen!"