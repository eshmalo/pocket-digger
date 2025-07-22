# Pocket Digger

![Build‑Android](https://github.com/<owner>/pocket-digger/actions/workflows/unity-build.yml/badge.svg)

A Unity-based mobile game where players dig through terrain to guide marbles into goal buckets.

## Secrets Configuration

Add these secrets to your GitHub repository (Settings → Secrets and variables → Actions):

| Secret Key | Description |
|------------|-------------|
| `UNITY_LICENSE` | Text of manual activation file from Unity Hub |
| `UNITY_EMAIL` | Unity ID email for activation |
| `UNITY_PASSWORD` | Unity ID password for activation |
| `GAMEANALYTICS_GAME_KEY` | From GameAnalytics → Create Game → Android |
| `GAMEANALYTICS_SECRET_KEY` | From GameAnalytics → Create Game → Android |
| `DEPLOYGATE_API_TOKEN` | From DeployGate → API Tokens → Generate |

## DeployGate Upload Command

```bash
curl -X POST \
  -F "file=@build/Android/PocketDigger.apk" \
  -F "message=CI build $GITHUB_SHA" \
  -H "Authorization: token $DEPLOYGATE_API_TOKEN" \
  https://deploygate.com/api/users/<your-username>/apps
```