# GitHub Secrets Checklist

## For Kivy Build (build-android.yml)

You need these 4 secrets in your GitHub repository:
Settings → Secrets and variables → Actions → New repository secret

### 1. KEYSTORE_BASE64
```bash
# Copy the content of this file:
cat keystore_base64.txt
```
- Paste the ENTIRE content (it will be long)
- Make sure no extra spaces or line breaks

### 2. KEYSTORE_PASS
```
marble2024
```

### 3. KEY_ALIAS
```
marblecollector
```

### 4. KEY_ALIAS_PASS
```
marble2024
```

## Quick Fix Options:

### Option A: Use Simple Build (No Signing)
The `build-android-simple.yml` workflow doesn't need secrets. It will:
- Build unsigned debug APK
- You can test it locally
- Can't upload to Play Store

### Option B: Build Locally Instead
```bash
# If GitHub Actions keeps failing:
source venv/bin/activate
buildozer android debug  # Unsigned APK
buildozer android release  # Signed AAB
```

### Option C: Manual Workflow Trigger
1. Go to Actions tab
2. Click "Build Android Simple" 
3. Click "Run workflow"
4. Select branch: prototype
5. Run workflow

This will give us logs to debug further.