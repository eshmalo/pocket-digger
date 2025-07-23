#!/bin/bash

# Replace <your-username> with your GitHub username
GITHUB_USERNAME="<your-username>"

echo "Setting up remote and pushing to GitHub..."
git remote add origin "https://github.com/$GITHUB_USERNAME/pocket-digger.git"
git push -u origin prototype

echo "Repository pushed to: https://github.com/$GITHUB_USERNAME/pocket-digger"