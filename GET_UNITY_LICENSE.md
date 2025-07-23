# Getting Your Unity Personal License File

## Steps to get your .ulf file:

1. **Open Unity Hub** (you already have it installed)

2. **Log in** with your Unity account:
   - Email: shmalo.elazar@gmail.com
   - Password: 2026.Hell

3. **Navigate to licenses**:
   - Unity Hub → Preferences → Licenses
   - Click the **Add** button
   - Select **"Get a free personal license"**
   - Complete the activation

4. **Find the .ulf file** on your Mac:
   ```bash
   # The file is located at:
   /Library/Application Support/Unity/Unity_lic.ulf
   
   # Copy it to your desktop for easy access:
   cp "/Library/Application Support/Unity/Unity_lic.ulf" ~/Desktop/Unity_lic.ulf
   ```

5. **Open the file** and copy ALL contents:
   ```bash
   cat ~/Desktop/Unity_lic.ulf
   ```

6. **Add to GitHub Secrets**:
   - Go to: https://github.com/eshmalo/pocket-digger/settings/secrets/actions
   - Update these secrets:
     - `UNITY_LICENSE`: Paste the entire .ulf file contents
     - `UNITY_EMAIL`: shmalo.elazar@gmail.com  
     - `UNITY_PASSWORD`: 2026.Hell

## Important Notes:
- The .ulf file contains XML that starts with `<?xml version="1.0" encoding="utf-8"?>`
- Copy EVERYTHING including the XML declaration
- This file is tied to your Unity account, not your machine
- Once added to GitHub, the build will work!