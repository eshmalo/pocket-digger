# 📱 Internal Testing Launch Checklist

## Pre-Build Verification
- [x] Code-only bootstrap working (GameBootstrap.cs)
- [x] All scripts updated for runtime generation
- [x] Keystore configured (pocket-digger.keystore)
- [x] Package name: com.eshmalo.pocketdigger
- [x] Min API: 21, Target API: 34

## Build Steps (10 min)
1. [ ] Open Unity 2021.3.21f1
2. [ ] Press Play once to verify game runs
3. [ ] File > Build Settings > Android
4. [ ] ✓ Build App Bundle (Google Play)
5. [ ] Build as: `PocketDigger-0.1-int.aab`
   
   OR use: `./build-aab.sh` (automated)

## Google Play Console Setup (20 min)
1. [ ] Go to: https://play.google.com/console
2. [ ] Select "Pocket Digger" app
3. [ ] Testing > Internal testing > Create new release

### Upload AAB
4. [ ] Upload `PocketDigger-0.1-int.aab`
5. [ ] Version code: 1
6. [ ] Release name: "0.1.0 Alpha"
7. [ ] Release notes: "Initial internal test build"

### Complete Forms
8. [ ] Privacy Policy URL: Upload PRIVACY_POLICY.md to GitHub Gist
9. [ ] Data Safety: "No data collected"
10. [ ] Content Rating: Complete questionnaire (Casual game, 13+)
11. [ ] App Category: Game > Casual

### Add Testers
12. [ ] Manage testers > Add email addresses
13. [ ] Include:
    - Your email
    - Team members
    - 5-10 trusted testers
14. [ ] Save changes

### Roll Out
15. [ ] Review release
16. [ ] Start rollout to Internal testing
17. [ ] Copy opt-in link for testers

## Post-Launch (30 min after rollout)
- [ ] Verify AAB processes successfully
- [ ] Test install on personal device
- [ ] Check Play Console > Android Vitals
- [ ] Send opt-in link to testers

## Day 0 Metrics to Track
| Metric | Target | Actual |
|--------|--------|--------|
| Install Success | ≥95% | ___ |
| Crash-free Sessions | ≥99% | ___ |
| Median FPS | ≥55 | ___ |
| ANR Rate | <0.1% | ___ |

## Quick Survey for Testers
Create Google Form with:
1. How satisfying is the digging? (1-5)
2. Did marbles collect smoothly? (Y/N)
3. Any crashes or freezes? (Y/N + details)
4. Device model and Android version
5. Overall fun factor (1-5)

## Report Back Format
```
Internal build live!
- Store link: [URL]
- FPS = ___
- Crash count = ___
- Initial feedback: [summary]
```

---

💡 **Pro tip**: Keep Play Console > Android Vitals open in a tab. Crash data appears within 1-2 hours of first installs.