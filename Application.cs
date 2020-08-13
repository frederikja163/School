using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FlappyBird
{
    public sealed class Application
    {
        private RenderWindow _window;
        private Player _player;

        public Application()
        {
            _window = new RenderWindow(new VideoMode(800, 600), "Flappy bird", Styles.Titlebar | Styles.Close);

            var spriteSheet = new Texture("spriteSheet.png");
            _player = new Player(_window, _window.Size.X * 0.2f, spriteSheet,
                new IntRect(3, 491, 17, 12),
                new IntRect(31, 491, 17, 12),
                new IntRect(59, 491, 17, 12));

            _window.Closed += OnWindowClosed;
        }

        public void Run()
        {
            var watch = new Stopwatch();
            watch.Start();
            while (_window.IsOpen)
            {
                _window.DispatchEvents();

                var deltaT = watch.ElapsedTicks / (float)Stopwatch.Frequency;
                watch.Restart();
                
                _player.Update(deltaT);

                _window.Clear();
                _player.Draw();
                _window.Display();
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
    }
}
