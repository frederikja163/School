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
        private readonly RenderWindow _window;
        private readonly Player _player;
        private readonly ObstacleManager _obstacleManager;
        private bool _isUpdating = true;

        public Application()
        {
            _window = new RenderWindow(new VideoMode(800, 600), "Flappy bird", Styles.Titlebar | Styles.Close);

            var spriteSheet = new Texture("spriteSheet.png");
            _player = new Player(_window, _window.Size.X * 0.2f, spriteSheet,
                new IntRect(3, 491, 17, 12),
                new IntRect(31, 491, 17, 12),
                new IntRect(59, 491, 17, 12));
            
            _obstacleManager = new ObstacleManager(_window, _player, spriteSheet, new IntRect[1, 2]{{new IntRect(56, 323, 26,160), new IntRect(84, 323, 26, 160)}});

            _obstacleManager.OnPlayerHitObstacle += OnPlayerHitObstacle;
            _window.Closed += OnWindowClosed;
        }

        private void OnPlayerHitObstacle()
        {
            _isUpdating = false;
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
                
                if (_isUpdating)
                {
                    _player.Update(deltaT);
                    _obstacleManager.Update(deltaT);
                }

                _window.Clear();
                _player.Draw();
                _obstacleManager.Draw();
                _window.Display();
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
    }
}
