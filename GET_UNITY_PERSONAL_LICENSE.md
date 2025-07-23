# Get Unity Personal License (.ulf) - Quick Guide

## Option 1: Direct from Unity Hub (Easiest)

1. Open Unity Hub
2. Click gear icon → License Management
3. Click "Add" → "Get a free personal license"
4. Sign in with your Unity account
5. Once activated, go to "Manual Activation"
6. Save the .ulf file

## Option 2: Manual Activation via Web

1. In Unity Hub → License Management → Manual Activation
2. Click "Save License Request" (saves a .alf file)
3. Go to: https://license.unity3d.com/manual
4. Upload the .alf file
5. Sign in with: shmalo.elazar@gmail.com
6. Select "Unity Personal" (free)
7. Download the .ulf file

## Add to GitHub Secret

1. Open the .ulf file in any text editor
2. Copy ALL content (starts with <?xml version...)
3. Go to: https://github.com/eshmalo/pocket-digger/settings/secrets/actions
4. Update/Create secret named `UNITY_LICENSE`
5. Paste the entire .ulf content

## Remove the Serial Secret

Since we're using Personal license, remove the UNITY_SERIAL secret if it exists.