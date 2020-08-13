using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird
{
    public static class Utility
    {
        public static float Lerp(float a, float b, float t)
        {
            return a * (1 - t) + b * t;
        }

        public static float Clamp(float a, float min, float max)
        {
            return MathF.Min(max, MathF.Max(min, a));
        }
    }
}
