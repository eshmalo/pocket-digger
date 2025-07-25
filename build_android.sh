#!/bin/bash

# Activate virtual environment
source venv/bin/activate

# Export necessary paths for macOS
export PATH="/opt/homebrew/bin:$PATH"
export LDFLAGS="-L/opt/homebrew/opt/openssl@3/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl@3/include"

# Auto-accept prerequisites
export PYTHONUNBUFFERED=1

# Build with automatic yes responses
yes | buildozer android debug

echo "Build complete! Check bin/ directory for APK"