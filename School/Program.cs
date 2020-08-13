using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace School
{
    class Program
    {
        private static RenderWindow _window;

        static void Main(string[] args)
        {
            _window = new RenderWindow(new VideoMode(800, 600), "Test");
            var rect = new RectangleShape(new Vector2f(50, 50));
            rect.FillColor = new Color(150, 150, 0);
            rect.Position = new Vector2f(_window.Size.X / 2, _window.Size.Y / 2);
            _window.Closed += WindowClosed;

            while(_window.IsOpen)
            {
                _window.DispatchEvents();
                _window.Clear();


                _window.Draw(rect);

                _window.Display();
            }
        }

        private static void WindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
    }
}
