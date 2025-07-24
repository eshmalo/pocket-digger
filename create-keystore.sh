#!/bin/bash

echo "Creating Android Keystore for Pocket Digger"
echo "==========================================="

# Create keystore
keytool -genkeypair \
  -alias pocket-digger \
  -keyalg RSA \
  -keysize 2048 \
  -validity 10000 \
  -keystore pocket-digger.keystore \
  -storepass pocket123 \
  -keypass pocket123 \
  -dname "CN=Pocket Digger, OU=Mobile Games, O=EshMalo, L=New York, ST=NY, C=US"

echo ""
echo "✅ Keystore created: pocket-digger.keystore"
echo "   Alias: pocket-digger"
echo "   Password: pocket123"
echo ""
echo "⚠️  IMPORTANT: Back up this keystore file!"
echo "   You'll need it for all future updates to Google Play"