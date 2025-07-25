#!/bin/bash

echo "🚀 Marble Collector Build Pre-flight Check"
echo "=========================================="
echo ""

# Check Python game
echo "✓ Checking game file..."
if [ -f "main.py" ]; then
    echo "  main.py found"
else
    echo "  ❌ main.py missing!"
fi

# Check buildozer.spec
echo ""
echo "✓ Checking build config..."
if [ -f "buildozer.spec" ]; then
    echo "  buildozer.spec found"
    grep -q "title = Marble Collector" buildozer.spec && echo "  Title: Marble Collector ✓"
    grep -q "android.release_artifact = aab" buildozer.spec && echo "  AAB output: ✓"
else
    echo "  ❌ buildozer.spec missing!"
fi

# Check keystore
echo ""
echo "✓ Checking signing keystore..."
if [ -f "marblecollector.keystore" ]; then
    echo "  Keystore found (protected by .gitignore)"
else
    echo "  ❌ marblecollector.keystore missing!"
fi

# Check Docker
echo ""
echo "✓ Checking Docker..."
if command -v docker &> /dev/null; then
    echo "  Docker command available"
    if docker info &> /dev/null; then
        echo "  Docker daemon running ✓"
    else
        echo "  ⚠️  Docker installed but not running"
        echo "  Run: open -a Docker"
    fi
else
    echo "  ❌ Docker not installed"
    echo "  Run: brew install --cask docker"
fi

echo ""
echo "=========================================="
echo "Ready to build? Run: ./build_docker.sh"
echo ""