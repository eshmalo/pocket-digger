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

1. **Build AAB for Google Play:**
   ```bash
   buildozer android release
   ```

2. **Build APK for testing:**
   ```bash
   buildozer android debug
   ```

The built files will be in the `bin/` directory.

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