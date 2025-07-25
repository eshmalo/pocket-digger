from PIL import Image, ImageDraw, ImageFont
import os

# Create store_assets directory
os.makedirs('store_assets', exist_ok=True)

# Colors from the game
layer_colors = [
    (255, 179, 179),  # Pastel red
    (255, 230, 179),  # Pastel orange  
    (255, 255, 179),  # Pastel yellow
    (179, 255, 179),  # Pastel green
    (179, 179, 255),  # Pastel blue
]

# 1. Create App Icon (512x512)
icon = Image.new('RGBA', (512, 512), (38, 38, 38, 255))
draw = ImageDraw.Draw(icon)

# Draw stacked layers
for i, color in enumerate(layer_colors):
    y = 100 + i * 60
    # Main layer
    draw.rectangle([100, y, 412, y + 50], fill=color)
    # Slice effect
    if i < 3:
        draw.rectangle([250 + i*20, y, 412, y + 50], fill=(38, 38, 38, 255))

# Add gem at bottom
draw.ellipse([226, 380, 286, 440], fill=(255, 215, 0))
draw.ellipse([236, 390, 276, 430], fill=(255, 255, 100))

icon.save('store_assets/icon.png')
print("✓ Created icon.png")

# 2. Create Feature Graphic (1024x500)
feature = Image.new('RGBA', (1024, 500), (38, 38, 38, 255))
draw = ImageDraw.Draw(feature)

# Draw larger layers
for i, color in enumerate(layer_colors):
    y = 50 + i * 70
    draw.rectangle([200, y, 824, y + 60], fill=color)
    # Slice effect
    if i < 2:
        draw.rectangle([500 + i*50, y, 824, y + 60], fill=(38, 38, 38, 255))

# Title text (using default font)
try:
    font = ImageFont.truetype("/System/Library/Fonts/Helvetica.ttc", 72)
except:
    font = ImageFont.load_default()

# Add title
draw.text((512, 400), "LAYER SLICE", font=font, anchor="mm", fill=(255, 255, 255))

feature.save('store_assets/feature_graphic.png')
print("✓ Created feature_graphic.png")

# 3. Create Screenshots (1080x1920)
for screen_num in range(3):
    screenshot = Image.new('RGBA', (1080, 1920), (38, 38, 38, 255))
    draw = ImageDraw.Draw(screenshot)
    
    # Game area
    game_y = 400
    for i, color in enumerate(layer_colors):
        y = game_y + i * 120
        if screen_num == 0:
            # Full layers
            draw.rectangle([140, y, 940, y + 100], fill=color)
        elif screen_num == 1:
            # Partially sliced
            if i < 2:
                draw.rectangle([140, y, 600 - i*100, y + 100], fill=color)
            else:
                draw.rectangle([140, y, 940, y + 100], fill=color)
        else:
            # Almost complete
            if i < 4:
                draw.rectangle([140, y, 300 - i*50, y + 100], fill=color)
            else:
                draw.rectangle([140, y, 940, y + 100], fill=color)
    
    # UI elements
    draw.text((540, 200), "SCORE: " + str(screen_num * 1250), 
              font=font, anchor="mm", fill=(255, 255, 255))
    
    # Progress bar
    draw.rectangle([140, 1600, 940, 1640], fill=(64, 64, 64))
    progress = [0.1, 0.5, 0.85][screen_num]
    draw.rectangle([140, 1600, 140 + int(800 * progress), 1640], fill=(76, 204, 76))
    
    # Gem (visible in last screenshot)
    if screen_num == 2:
        draw.ellipse([490, 1200, 590, 1300], fill=(255, 215, 0))
    
    screenshot.save(f'store_assets/screenshot_{screen_num + 1}.png')
    print(f"✓ Created screenshot_{screen_num + 1}.png")

print("\n✅ All store assets created in store_assets/ directory!")
print("\nNext steps:")
print("1. Check your GitHub Actions for the AAB file")
print("2. Follow PLAY_STORE_GUIDE.md to upload")
print("3. Use the generated assets for store listing")