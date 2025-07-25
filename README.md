# Marble Collector - Python Kivy Game

A simple marble collection game built with Python and Kivy framework.

## Setup

1. **Activate virtual environment:**
   ```bash
   source venv/bin/activate
   ```

2. **Run the game locally:**
   ```bash
   python main.py
   ```

## Building for Android

### Option A: Docker Build (Recommended - 20-25 min)
```bash
# Install Docker Desktop first
brew install --cask docker

# Run build script
./build_docker.sh
```

### Option B: Native macOS Build (30-60 min)
```bash
# Install build tools
brew install autoconf automake libtool pkg-config cmake openssl@3

# Use build script
./build_android.sh
```

### Build Outputs
- **Debug APK**: `bin/marblecollector-0.1-arm64-v8a_armeabi-v7a-debug.apk`
- **Release AAB**: `bin/marblecollector-0.1-arm64-v8a_armeabi-v7a-release.aab`

### First Build Notes
- First build downloads Android SDK/NDK (~2GB)
- Subsequent builds take only 2-3 minutes
- Docker approach avoids macOS-specific issues

## Game Controls

- Move your finger/mouse left and right to control the bucket
- Catch falling marbles to increase your score
- Each marble caught adds 10 points

## Requirements

- Python 3.7+
- Kivy 2.3.1
- Buildozer (for Android builds)

## Notes

- First build will download Android SDK/NDK automatically
- Building requires Java JDK installed
- On macOS/Linux, you may need to install additional dependencies for Buildozer