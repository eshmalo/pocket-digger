# Unity License Activation Steps

You have the Unity License Request file (`.alf`). Now complete these steps:

## 1. Go to Unity License Portal
Open this URL in your browser:
**https://license.unity3d.com/manual**

## 2. Upload Your .alf File
- Click "Browse" and select: `/Users/elazarshmalo/PycharmProjects/PythonProject4/Unity_v6000.1.13f1.alf`
- OR drag and drop the file onto the page

## 3. Sign In
- Use your Unity account: `shmalo.elazar@gmail.com`
- Password: `2026.Hell`

## 4. Choose License Type
- Select **"Unity Personal"** (free license)
- Confirm you meet the revenue requirements for Personal license

## 5. Download License File
- Click "Download license file"
- This will download a `.ulf` file (Unity License File)

## 6. Open the .ulf File
- Open the downloaded `.ulf` file in any text editor (TextEdit, VS Code, etc.)
- Select ALL text (Cmd+A) and copy it (Cmd+C)

## 7. Add to GitHub Secrets
Go to: https://github.com/eshmalo/pocket-digger/settings/secrets/actions/new

Add these three secrets:

| Name | Value |
|------|-------|
| `UNITY_EMAIL` | `shmalo.elazar@gmail.com` |
| `UNITY_PASSWORD` | `2026.Hell` |
| `UNITY_LICENSE` | [Paste the ENTIRE .ulf file contents here] |

## 8. Trigger the Build
Once all three secrets are added, the workflow will automatically run on your next push, or you can manually trigger it from the Actions tab.

---

**Note**: The .ulf file will contain XML that starts with something like:
```xml
<?xml version="1.0" encoding="utf-8"?>
<root>
<License id="Unity">
...
</root>
```

Copy EVERYTHING including the XML declaration!