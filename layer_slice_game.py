from kivy.app import App
from kivy.uix.widget import Widget
from kivy.uix.floatlayout import FloatLayout
from kivy.uix.label import Label
from kivy.graphics import Color, Ellipse, Rectangle, Fbo, ClearColor, ClearBuffers
from kivy.graphics.texture import Texture
from kivy.clock import Clock
from kivy.core.window import Window
from kivy.properties import NumericProperty, ListProperty
from random import random, randint
import math

# Try to import plyer for haptic feedback
try:
    from plyer import vibrator
    HAS_VIBRATOR = True
except:
    HAS_VIBRATOR = False


class Layer(Widget):
    """Single colorful layer that can be sliced"""
    
    def __init__(self, color, layer_index, **kwargs):
        super().__init__(**kwargs)
        self.layer_color = color
        self.layer_index = layer_index
        self.texture_size = 256
        self.pixels_remaining = self.texture_size * self.texture_size
        
        # Create texture for this layer
        self.texture = Texture.create(
            size=(self.texture_size, self.texture_size),
            colorfmt='rgba'
        )
        self.texture.flip_vertical()
        
        # Fill with layer color
        buf = []
        for i in range(self.texture_size * self.texture_size):
            buf.extend([
                int(color[0] * 255),
                int(color[1] * 255),
                int(color[2] * 255),
                255  # Alpha
            ])
        self.texture.blit_buffer(bytes(buf), colorfmt='rgba', bufferfmt='ubyte')
        
        # Display the texture
        with self.canvas:
            Color(1, 1, 1, 1)
            self.rect = Rectangle(
                texture=self.texture,
                pos=self.pos,
                size=self.size
            )
        
        self.bind(pos=self.update_rect, size=self.update_rect)
    
    def update_rect(self, *args):
        self.rect.pos = self.pos
        self.rect.size = self.size
    
    def slice_at(self, touch_x, touch_y, radius):
        """Erase pixels at touch position"""
        # Convert touch to texture coordinates
        tx = int((touch_x - self.x) / self.width * self.texture_size)
        ty = int((touch_y - self.y) / self.height * self.texture_size)
        tr = int(radius / self.width * self.texture_size)
        
        # Get current texture data
        pixels = bytearray(self.texture.pixels)
        
        # Erase circle
        pixels_erased = 0
        for y in range(max(0, ty - tr), min(self.texture_size, ty + tr)):
            for x in range(max(0, tx - tr), min(self.texture_size, tx + tr)):
                if math.sqrt((x - tx)**2 + (y - ty)**2) <= tr:
                    idx = (y * self.texture_size + x) * 4
                    if idx + 3 < len(pixels) and pixels[idx + 3] > 0:
                        pixels[idx + 3] = 0  # Set alpha to 0
                        pixels_erased += 1
        
        # Update texture
        self.texture.blit_buffer(pixels, colorfmt='rgba', bufferfmt='ubyte')
        self.pixels_remaining -= pixels_erased
        
        return pixels_erased > 0


class GemCore(Widget):
    """Shimmering gem at the bottom"""
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        self.rotation = 0
        
        with self.canvas:
            # Gem gradient effect
            self.color1 = Color(1, 0.8, 0.2, 1)
            self.gem = Ellipse(pos=self.pos, size=(60, 60))
            self.color2 = Color(1, 1, 0.4, 0.5)
            self.glow = Ellipse(pos=(self.x - 10, self.y - 10), size=(80, 80))
        
        Clock.schedule_interval(self.update_gem, 1/30.0)
        self.bind(pos=self.update_graphics)
    
    def update_graphics(self, *args):
        self.gem.pos = self.pos
        self.glow.pos = (self.x - 10, self.y - 10)
    
    def update_gem(self, dt):
        # Pulsing effect
        self.rotation += dt * 100
        scale = 1 + math.sin(self.rotation * 0.05) * 0.1
        self.gem.size = (60 * scale, 60 * scale)
        self.glow.size = (80 * scale, 80 * scale)


class SliceParticle(Widget):
    """Particle effect when slicing"""
    
    def __init__(self, x, y, color, **kwargs):
        super().__init__(**kwargs)
        self.velocity = [randint(-100, 100), randint(50, 200)]
        self.lifetime = 1.0
        self.start_color = color
        
        with self.canvas:
            self.color = Color(*color, 1)
            self.particle = Ellipse(pos=(x, y), size=(8, 8))
    
    def update(self, dt):
        self.lifetime -= dt * 2
        if self.lifetime <= 0:
            return False
        
        # Update position
        self.particle.pos = (
            self.particle.pos[0] + self.velocity[0] * dt,
            self.particle.pos[1] + self.velocity[1] * dt
        )
        self.velocity[1] -= 300 * dt  # Gravity
        
        # Fade out
        self.color.a = self.lifetime
        return True


class LayerSliceGame(FloatLayout):
    score = NumericProperty(0)
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        
        # Layer colors (pastel rainbow)
        self.layer_colors = [
            (1.0, 0.7, 0.7),    # Pastel red
            (1.0, 0.9, 0.7),    # Pastel orange  
            (1.0, 1.0, 0.7),    # Pastel yellow
            (0.7, 1.0, 0.7),    # Pastel green
            (0.7, 0.7, 1.0),    # Pastel blue
        ]
        
        # Create layers
        self.layers = []
        for i, color in enumerate(self.layer_colors):
            layer = Layer(color, i)
            layer.size_hint = (0.8, 0.6)
            layer.pos_hint = {'center_x': 0.5, 'center_y': 0.4 + i * 0.02}
            self.layers.append(layer)
            self.add_widget(layer)
        
        # Create gem
        self.gem = GemCore()
        self.gem.size_hint = (None, None)
        self.gem.size = (60, 60)
        self.gem.pos_hint = {'center_x': 0.5, 'center_y': 0.1}
        self.add_widget(self.gem)
        
        # UI
        self.score_label = Label(
            text='Slice to reveal the gem!',
            font_size='24sp',
            size_hint=(1, 0.1),
            pos_hint={'x': 0, 'top': 1}
        )
        self.add_widget(self.score_label)
        
        # Progress bar
        with self.canvas:
            Color(0.2, 0.2, 0.2, 1)
            self.progress_bg = Rectangle(pos=(20, 20), size=(Window.width - 40, 30))
            Color(0.3, 0.8, 0.3, 1)
            self.progress_bar = Rectangle(pos=(20, 20), size=(0, 30))
        
        # Particle system
        self.particles = []
        
        # Update loop
        Clock.schedule_interval(self.update, 1/60.0)
        
        # Touch tracking
        self.last_touch = None
    
    def on_touch_down(self, touch):
        self.last_touch = (touch.x, touch.y)
        return True
    
    def on_touch_move(self, touch):
        if self.last_touch:
            # Find top visible layer
            for layer in reversed(self.layers):
                if layer.pixels_remaining > 0:
                    if layer.slice_at(touch.x, touch.y, 30):
                        # Create particles
                        for _ in range(3):
                            particle = SliceParticle(touch.x, touch.y, layer.layer_color)
                            self.particles.append(particle)
                            self.add_widget(particle)
                        
                        # Update score
                        self.score += 10
                        
                        # Haptic feedback
                        if HAS_VIBRATOR:
                            vibrator.vibrate(0.01)  # Quick pulse
                        
                        # Check if layer completed
                        progress = 1 - (layer.pixels_remaining / (layer.texture_size ** 2))
                        if progress > 0.85:  # 85% cleared
                            layer.pixels_remaining = 0
                            self.score += 500
                            self.score_label.text = f'Layer {layer.layer_index + 1} complete! Score: {self.score}'
                            
                            # Stronger vibration for layer complete
                            if HAS_VIBRATOR:
                                vibrator.vibrate(0.05)
                    break
        
        self.last_touch = (touch.x, touch.y)
        return True
    
    def update(self, dt):
        # Update particles
        for particle in self.particles[:]:
            if not particle.update(dt):
                self.remove_widget(particle)
                self.particles.remove(particle)
        
        # Update progress bar
        total_pixels = len(self.layers) * (self.layers[0].texture_size ** 2)
        cleared_pixels = sum(
            (layer.texture_size ** 2) - layer.pixels_remaining 
            for layer in self.layers
        )
        progress = cleared_pixels / total_pixels
        self.progress_bar.size = ((Window.width - 40) * progress, 30)
        
        # Check win condition
        if all(layer.pixels_remaining == 0 for layer in self.layers):
            self.score_label.text = f'GEM REVEALED! Final Score: {self.score}'


class LayerSliceApp(App):
    def build(self):
        Window.clearcolor = (0.15, 0.15, 0.15, 1)
        return LayerSliceGame()


if __name__ == '__main__':
    LayerSliceApp().run()