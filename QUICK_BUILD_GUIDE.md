# Quick Build Guide - 3 Options

## Option 1: Build Locally (Fastest - 10 min)
1. Install Unity Hub
2. Add Unity 2021.3.21f1 LTS
3. Open this project
4. File → Build Settings → Android → Build
5. Test APK in Android Studio

## Option 2: Unity Cloud Build (Easiest - 15 min)
1. Go to: https://dashboard.unity3d.com/
2. Create Project → Import from GitHub
3. Select: eshmalo/pocket-digger
4. Configure: Android build
5. Unity handles all licensing automatically!

## Option 3: Fix GitHub Actions (Complex - 30+ min)
Since Unity deprecated their activation method, you need:
1. Install Unity locally first
2. Activate it with your Unity ID
3. Extract the license file from your local Unity
4. Add it as UNITY_LICENSE secret

## Recommended: Use Option 2
Unity Cloud Build is free for Personal users and handles all the licensing complexity. It's the official Unity solution and works reliably.