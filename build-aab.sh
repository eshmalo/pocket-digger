#!/bin/bash

# Pocket Digger AAB Build Script
# Builds Android App Bundle for Google Play Store

echo "🚀 Building Pocket Digger AAB for Internal Testing..."

# Unity path - adjust if your Unity is installed elsewhere
UNITY_PATH="/Applications/Unity/Hub/Editor/2021.3.21f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH=$(pwd)
BUILD_PATH="$PROJECT_PATH/Builds"
AAB_NAME="PocketDigger-0.1-int.aab"

# Create build directory if it doesn't exist
mkdir -p "$BUILD_PATH"

# Build the AAB
echo "📦 Building Android App Bundle..."
"$UNITY_PATH" \
  -batchmode \
  -nographics \
  -silent-crashes \
  -quit \
  -projectPath "$PROJECT_PATH" \
  -buildTarget Android \
  -executeMethod UnityEditor.BuildPlayerWindow.DefaultBuildMethods.BuildPlayer \
  -buildPath "$BUILD_PATH/$AAB_NAME" \
  -androidBuildType AppBundle \
  -androidBuildSystem Gradle \
  -androidPackageName com.eshmalo.pocketdigger \
  -androidMinSdkVersion AndroidApiLevel21 \
  -androidTargetSdkVersion AndroidApiLevel34 \
  -keystorePath "$PROJECT_PATH/pocket-digger.keystore" \
  -keystorePassword "pocket123" \
  -keyaliasName "pocket-digger" \
  -keyaliasPassword "pocket123"

# Check if build succeeded
if [ -f "$BUILD_PATH/$AAB_NAME" ]; then
    echo "✅ Build successful!"
    echo "📍 AAB location: $BUILD_PATH/$AAB_NAME"
    echo ""
    echo "📱 Next steps:"
    echo "1. Go to https://play.google.com/console"
    echo "2. Select 'Pocket Digger' app"
    echo "3. Navigate to Testing > Internal testing"
    echo "4. Create new release and upload: $AAB_NAME"
    echo "5. Add tester emails and roll out"
    
    # Show file size
    ls -lh "$BUILD_PATH/$AAB_NAME"
else
    echo "❌ Build failed! Check Unity logs for errors."
    exit 1
fi