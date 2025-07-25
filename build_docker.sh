#!/bin/bash

echo "Building Marble Collector with Docker..."
echo "NOTE: Requires Docker Desktop to be installed first"
echo ""

# Check if Docker is available
if ! command -v docker &> /dev/null; then
    echo "Docker not found. Please install Docker Desktop from:"
    echo "https://www.docker.com/products/docker-desktop/"
    echo ""
    echo "Or install via Homebrew:"
    echo "brew install --cask docker"
    exit 1
fi

# Pull the image first
echo "Pulling Kivy buildozer image..."
docker pull kivy/buildozer:latest

# Build debug APK
echo "Building debug APK..."
docker run --rm -v "$PWD":/workspace:rw -w /workspace \
    --platform linux/amd64 \
    kivy/buildozer:latest sh -c "pip install buildozer==1.5 && \
    buildozer android debug"

# Build release AAB
echo ""
echo "Building release AAB..."
docker run --rm -v "$PWD":/workspace:rw -w /workspace \
    --platform linux/amd64 \
    kivy/buildozer:latest sh -c "pip install buildozer==1.5 && \
    buildozer android release"

echo ""
echo "Build complete! Check bin/ directory:"
ls -la bin/