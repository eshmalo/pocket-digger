#!/bin/bash

echo "Building Marble Collector with custom Docker image..."
echo ""

# Check if Docker is available
if ! command -v docker &> /dev/null; then
    echo "Docker not found. Please ensure Docker Desktop is running."
    exit 1
fi

# Build custom image
echo "Building custom buildozer image..."
docker build -t buildozer-custom -f Dockerfile.buildozer .

# Build debug APK
echo ""
echo "Building debug APK..."
docker run --rm -v "$PWD":/workspace:rw -w /workspace \
    buildozer-custom sh -c "buildozer android debug"

echo ""
echo "Build complete! Check bin/ directory:"
ls -la bin/