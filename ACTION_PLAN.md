# Action Plan - Unity First, Kivy CI

## 🎯 Today's Priority: Unity Layer Slice

### Step 1: Unity Build (< 30 min)
```bash
# In Unity Editor:
# Menu → Pocket Digger → Build Internal Test AAB
```

### Step 2: Play Console Upload
1. Go to Internal testing track
2. Upload AAB from Unity build
3. Add release notes: "Layer Slice ASMR prototype v1"
4. Invite 5-10 testers

### Step 3: Create Survey
Google Form questions:
- Overall satisfaction (1-5)
- Would play again tomorrow? (Yes/No)
- Most satisfying aspect
- Biggest frustration
- Device model & Android version

## 🔧 Parallel: Kivy GitHub Actions Setup

### Update workflow for signing:
```yaml
name: Build Android APK/AAB

on:
  push:
    branches: [ main, prototype ]
  workflow_dispatch:

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    - name: Set up Python
      uses: actions/setup-python@v4
      with:
        python-version: '3.9'
    
    - name: Install dependencies
      run: |
        pip install buildozer cython kivy
        sudo apt update
        sudo apt install -y git zip unzip openjdk-17-jdk autoconf libtool pkg-config zlib1g-dev libncurses5-dev libncursesw5-dev libtinfo5 cmake libffi-dev libssl-dev
    
    - name: Decode Keystore
      env:
        KEYSTORE_BASE64: ${{ secrets.KEYSTORE_BASE64 }}
      run: |
        echo $KEYSTORE_BASE64 | base64 -d > marblecollector.keystore
    
    - name: Build APK
      run: |
        buildozer android debug
    
    - name: Build AAB
      env:
        P4A_RELEASE_KEYSTORE: marblecollector.keystore
        P4A_RELEASE_KEYSTORE_PASSWD: ${{ secrets.KEYSTORE_PASS }}
        P4A_RELEASE_KEYALIAS_PASSWD: ${{ secrets.KEY_ALIAS_PASS }}
        P4A_RELEASE_KEYALIAS: ${{ secrets.KEY_ALIAS }}
      run: |
        buildozer android release
    
    - name: Upload APK
      uses: actions/upload-artifact@v3
      with:
        name: MarbleCollector-APK
        path: bin/*.apk
        
    - name: Upload AAB
      uses: actions/upload-artifact@v3
      with:
        name: MarbleCollector-AAB
        path: bin/*.aab
```

### GitHub Secrets to Add:
```bash
# Generate base64 keystore:
base64 < marblecollector.keystore

# Then add to repo settings:
KEYSTORE_BASE64 = [output from above]
KEYSTORE_PASS = marble2024
KEY_ALIAS = marblecollector
KEY_ALIAS_PASS = marble2024
```

## 📊 Success Metrics

### Unity Target:
- Satisfaction: ≥ 4.0/5
- "Play again": ≥ 70% yes
- FPS: ≥ 55 on mid-tier

### Kivy Target:
- APK size: < 30MB
- FPS: ≥ 50
- Touch response: < 100ms

## 🚀 Next 24 Hours
1. Unity AAB → Play Store → Testers (TODAY)
2. Push Kivy to GitHub with Actions (TONIGHT)
3. Compare metrics (TOMORROW)

Engine with higher retention wins!