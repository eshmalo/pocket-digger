# GitHub Secrets Setup for Unity 6 Pro Trial

## Your Unity Account Info:
- **Email**: `shmalo.elazar@gmail.com`
- **Password**: `2026.Hell`
- **Serial**: `2475866362554-unity_a85230c974b535551851-UnityProTXXXX`

## Add These Secrets to GitHub:

Go to: https://github.com/eshmalo/pocket-digger/settings/secrets/actions/new

Add these THREE secrets:

| Secret Name | Secret Value |
|-------------|--------------|
| `UNITY_EMAIL` | `shmalo.elazar@gmail.com` |
| `UNITY_PASSWORD` | `2026.Hell` |
| `UNITY_SERIAL` | `2475866362554-unity_a85230c974b535551851-UnityProTXXXX` |

## Important Notes:
1. We're now using Unity 6000.1.13f1 (Unity 6 LTS) in the workflow
2. The workflow will use your Pro Trial license via serial number
3. No need for the .ulf file anymore!

## After Adding Secrets:
Push the updated workflow to trigger a build:

```bash
git add .github/workflows/unity-build.yml
git commit -m "Update workflow for Unity 6 with Pro Trial"
git push
```

The build should now work with your Unity Pro Trial!