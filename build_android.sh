#!/bin/bash

# Activate virtual environment
source venv/bin/activate

# Export necessary paths for macOS
export PATH="/opt/homebrew/bin:$PATH"
export LDFLAGS="-L/opt/homebrew/opt/openssl@3/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl@3/include"
export PKG_CONFIG_PATH="/opt/homebrew/opt/openssl@3/lib/pkgconfig"

# Create symlink for openssl if needed
if [ ! -e "/opt/homebrew/opt/openssl" ]; then
    ln -sf /opt/homebrew/opt/openssl@3 /opt/homebrew/opt/openssl
fi

# Auto-accept prerequisites
export PYTHONUNBUFFERED=1

# Build with automatic yes responses
yes | buildozer android debug

echo "Build complete! Check bin/ directory for APK"