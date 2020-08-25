using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SFML.System;

namespace FlappyBird
{
    public sealed class Application
    {
        private readonly RenderWindow _window;
        private readonly Player _player;
        private readonly ObstacleManager _obstacleManager;
        private readonly SpriteSheet _sheet;
        private Sprite? _tutorial;
        private bool _isUpdating = false;

        public Application()
        {
            _window = new RenderWindow(new VideoMode(800, 600), "Flappy bird", Styles.Titlebar | Styles.Close);

            _sheet = new SpriteSheet("spriteSheet.png");
            _player = new Player(_window, _window.Size.X * 0.2f, _sheet);
            _sheet.SetTutorialSprite(ref _tutorial);
            _tutorial.Position = (Vector2f)_window.Size / 2f;
            _tutorial.Scale = new Vector2f(3, 3);
            _obstacleManager = new ObstacleManager(_window, _player, _sheet);

            _obstacleManager.OnPlayerHitObstacle += OnPlayerHitObstacle;
            _window.Closed += OnWindowClosed;
            _window.KeyPressed += OnKeyPressed;
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
                if (!_isUpdating && _tutorial != null)
                {
                    _window.Draw(_tutorial);
                }
                _window.Display();
            }
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }

        private void OnKeyPressed(object? sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                if (!_isUpdating && _tutorial != null)
                {
                    _isUpdating = true;
                    _tutorial = null;
                }
                else if (!_isUpdating && _tutorial == null)
                {
                    _isUpdating = true;
                    //Reset
                }
            }
        }
    }
}
