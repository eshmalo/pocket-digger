# Getting Your Unity License for CI/CD

## Steps to Get Your Unity License File:

1. **Open Unity Hub**
   - Launch Unity Hub on your computer

2. **Navigate to License Management**
   - Click on the gear icon (⚙️) in the top-right
   - Select "License Management" from the menu

3. **Manual Activation**
   - Click "Manual Activation"
   - Click "Save License Request"
   - This saves a file like `Unity_lic.alf`

4. **Activate Online**
   - Go to: https://license.unity3d.com/manual
   - Upload the `.alf` file
   - Choose "Unity Personal" (free)
   - Download the `.ulf` license file

5. **Get License Text**
   - Open the downloaded `.ulf` file in a text editor
   - Copy ALL the text (starts with `<?xml version="1.0"...`)
   - This is your UNITY_LICENSE value

## Your GitHub Secrets:

| Secret | Value |
|--------|-------|
| UNITY_EMAIL | shmalo.elazar@gmail.com |
| UNITY_PASSWORD | [Your Unity account password] |
| UNITY_LICENSE | [Entire contents of .ulf file] |

## Add Secrets Here:
https://github.com/eshmalo/pocket-digger/settings/secrets/actions/new

## Alternative: Using Unity Personal License Action

If manual activation is too complex, we can modify the workflow to use automated personal license activation:

```yaml
- uses: game-ci/unity-request-activation-file@v2
  id: getManualLicenseFile
- uses: game-ci/unity-activate@v2
  with:
    unityVersion: 2022.3.24f1
```