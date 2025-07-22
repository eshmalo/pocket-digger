#!/bin/bash

# Deploy to DeployGate script
# Usage: ./deploy-to-deploygate.sh <your-username>

if [ -z "$1" ]; then
    echo "Usage: $0 <deploygate-username>"
    exit 1
fi

if [ -z "$DEPLOYGATE_API_TOKEN" ]; then
    echo "Error: DEPLOYGATE_API_TOKEN environment variable not set"
    exit 1
fi

APK_PATH="build/Android/PocketDigger.apk"

if [ ! -f "$APK_PATH" ]; then
    echo "Error: APK not found at $APK_PATH"
    exit 1
fi

echo "Uploading to DeployGate..."
curl -X POST \
  -F "file=@$APK_PATH" \
  -F "message=CI build $GITHUB_SHA" \
  -H "Authorization: token $DEPLOYGATE_API_TOKEN" \
  "https://deploygate.com/api/users/$1/apps"

echo -e "\nDone!"