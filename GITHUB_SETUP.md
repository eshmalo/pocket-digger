# GitHub Secrets Setup

## Add these secrets to your repository:

1. Go to your GitHub repository
2. Settings → Secrets and variables → Actions
3. Click "New repository secret" for each:

| Secret Name | Value |
|-------------|-------|
| `KEYSTORE_BASE64` | [Already in your clipboard - just paste!] |
| `KEYSTORE_PASS` | `marble2024` |
| `KEY_ALIAS` | `marblecollector` |
| `KEY_ALIAS_PASS` | `marble2024` |

## After adding secrets:

1. Push any commit to trigger the build:
   ```bash
   git push origin prototype
   ```

2. Go to Actions tab on GitHub
3. Wait ~15-20 minutes for build
4. Download artifacts:
   - MarbleCollector-APK (for testing)
   - MarbleCollector-AAB (for Play Store)

## Test the APK:
```bash
adb install -r MarbleCollector-*.apk
```

The APK will be the current marble game. Once it works, we'll add Layer Slice mechanics!