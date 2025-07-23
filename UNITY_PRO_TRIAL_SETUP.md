# Unity Pro Trial Setup for GitHub Actions

Since Unity Personal licenses can no longer be manually activated, we're using your Unity Pro Trial license with the serial number.

## Current Setup

The workflow is configured to use:
- Unity 2022.3.24f1 LTS (stable version)
- Your Unity Pro Trial license via serial activation
- game-ci/unity-activate@v2 for license activation

## Required Secrets (Already Set ✅)

| Secret Name | Value |
|-------------|-------|
| `UNITY_EMAIL` | `shmalo.elazar@gmail.com` |
| `UNITY_PASSWORD` | `2026.Hell` |
| `UNITY_SERIAL` | `2475866362554-unity_a85230c974b535551851-UnityProTXXXX` |

## How It Works

1. The workflow activates Unity using your serial number
2. Builds the Android APK
3. Returns the license seat (important for Pro/Trial licenses)
4. Uploads the APK as an artifact

## Troubleshooting

If the build fails with license errors:
1. Check that your Unity Pro Trial is still active in Unity Hub
2. Verify the serial number hasn't changed
3. Make sure the Unity version matches what you have locally

## Note About Unity Versions

We're using Unity 2022.3.24f1 LTS for better stability with Game-CI. Your local Unity 6 project will need to be opened with Unity 2022.3.24f1 to match the CI environment.