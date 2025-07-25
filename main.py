from kivy.app import App
from kivy.uix.widget import Widget
from kivy.uix.label import Label
from kivy.uix.floatlayout import FloatLayout
from kivy.graphics import Ellipse, Rectangle, Color
from kivy.clock import Clock
from kivy.core.window import Window
from kivy.properties import NumericProperty, ObjectProperty
from random import randint, random
import math


class Marble(Widget):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        self.size = (40, 40)
        self.pos = (randint(50, Window.width - 50), Window.height)
        self.velocity_y = -randint(100, 200)  # pixels per second
        self.collected = False
        
        # Random color
        with self.canvas:
            Color(random(), random(), random(), 1)
            self.circle = Ellipse(pos=self.pos, size=self.size)
    
    def update(self, dt):
        if not self.collected:
            self.y += self.velocity_y * dt
            self.circle.pos = self.pos
            
            # Remove if off screen
            if self.y < -self.height:
                return False
        return True
    
    def check_collision(self, bucket_x, bucket_y, bucket_width, bucket_height):
        # Check if marble center is within bucket bounds
        marble_center_x = self.x + self.width / 2
        marble_center_y = self.y + self.height / 2
        
        if (bucket_x < marble_center_x < bucket_x + bucket_width and
            bucket_y < marble_center_y < bucket_y + bucket_height):
            self.collected = True
            return True
        return False


class Bucket(Widget):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        self.size = (120, 80)
        self.pos = (Window.width / 2 - 60, 50)
        
        with self.canvas:
            Color(0.3, 0.6, 0.9, 1)  # Blue bucket
            self.rect = Rectangle(pos=self.pos, size=self.size)
    
    def move_to(self, x):
        # Keep bucket within screen bounds
        self.x = max(0, min(x - self.width / 2, Window.width - self.width))
        self.rect.pos = self.pos
    
    def on_touch_move(self, touch):
        self.move_to(touch.x)
        return True


class GameWidget(FloatLayout):
    score = NumericProperty(0)
    
    def __init__(self, **kwargs):
        super().__init__(**kwargs)
        
        # Create bucket
        self.bucket = Bucket()
        self.add_widget(self.bucket)
        
        # Create score label
        self.score_label = Label(
            text=f'Score: {self.score}',
            font_size='30sp',
            pos_hint={'x': 0, 'top': 1},
            size_hint=(0.5, 0.1)
        )
        self.add_widget(self.score_label)
        
        # Create FPS label
        self.fps_label = Label(
            text=f'FPS: 0',
            font_size='20sp',
            pos_hint={'x': 0.5, 'top': 1},
            size_hint=(0.5, 0.1)
        )
        self.add_widget(self.fps_label)
        
        # List to track marbles
        self.marbles = []
        
        # Schedule updates
        Clock.schedule_interval(self.update, 1/60.0)
        Clock.schedule_interval(self.spawn_marble, 2.0)  # Slower spawn for mobile performance
    
    def spawn_marble(self, dt):
        marble = Marble()
        self.marbles.append(marble)
        self.add_widget(marble)
    
    def update(self, dt):
        # Update marbles
        marbles_to_remove = []
        for marble in self.marbles[:]:
            if not marble.update(dt):
                marbles_to_remove.append(marble)
            elif marble.check_collision(
                self.bucket.x, self.bucket.y,
                self.bucket.width, self.bucket.height
            ):
                self.score += 10
                marbles_to_remove.append(marble)
        
        # Remove collected or off-screen marbles
        for marble in marbles_to_remove:
            if marble in self.marbles:
                self.marbles.remove(marble)
                self.remove_widget(marble)
        
        # Update score display
        self.score_label.text = f'Score: {self.score}'
        
        # Update FPS display
        self.fps_label.text = f'FPS: {int(Clock.get_fps())}'
    
    def on_touch_move(self, touch):
        self.bucket.on_touch_move(touch)
        return True


class MarbleCollectorApp(App):
    def build(self):
        # Set window background color
        Window.clearcolor = (0.1, 0.1, 0.1, 1)
        
        # Optimize for mobile
        from kivy.config import Config
        Config.set('graphics', 'multisamples', '0')  # Disable anti-aliasing for performance
        
        # Create and return the game widget
        game = GameWidget()
        return game


if __name__ == '__main__':
    MarbleCollectorApp().run()