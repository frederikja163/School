#nullable enable
using SFML.Graphics;
using SFML.System;

namespace FlappyBird
{
    public sealed class SpriteSheet
    {
        private readonly Texture _texture;
        private readonly IntRect[,] _birds;
        private readonly IntRect[,] _pipes;
        private readonly IntRect _background;
        private readonly IntRect _tutorial;
        
        public SpriteSheet(string filePath)
        {
            _texture = new Texture(filePath);
            _birds = new IntRect[,]
            {
                {GetLoc(3, 491, 17, 12), GetLoc(31, 491, 17, 12), GetLoc(59, 491, 17, 12)}, //Yellow bird
                {GetLoc(87, 491, 17, 12), GetLoc(115, 329, 17, 12), GetLoc(115, 355, 17, 12)}, //Blue bird
                {GetLoc(381, 381, 17, 12), GetLoc(115, 407, 17, 12), GetLoc(115, 433, 17, 12)} //Red bird
            };
            _pipes = new IntRect[,]
            {
                {GetLoc(56, 323, 26, 160), GetLoc(84, 323, 26, 160)}, //Green pipe
                {GetLoc(0, 323, 26, 160), GetLoc(28, 323, 26, 160)} //Red pipe
            };
            _background = GetLoc(0, 0, 144, 256);
            _tutorial = GetLoc(292, 91, 58, 49);
        }

        public void SetBirdSprite(ref Sprite? sprite, int birdType, int animFrame)
        {
            if (sprite == null)
            {
                sprite = new Sprite();
            }
            if (sprite.Texture != _texture)
            {
                sprite.Texture = _texture;
                sprite.Origin = new Vector2f(17, 12) / 2;
            }
            sprite.TextureRect = _birds[birdType, animFrame];
        }
        
        public void SetPipeSprite(ref Sprite? sprite, int pipeSkin, bool topPipe)
        {
            if (sprite == null)
            {
                sprite = new Sprite();
            }
            if (sprite.Texture != _texture)
            {
                sprite.Texture = _texture;
                sprite.TextureRect = _pipes[pipeSkin, topPipe ? 0 : 1];
                sprite.Origin = new Vector2f(26 / 2f,  topPipe ? 160 : 0);
                return;
            }
            sprite.TextureRect = _pipes[pipeSkin, topPipe ? 0 : 1];
        }

        public void SetBackground(ref Sprite? sprite)
        {
            if (sprite == null)
            {
                sprite = new Sprite();
            }
            if (sprite.Texture != _texture)
            {
                sprite.Texture = _texture;
            }
            sprite.TextureRect = _background;
        }

        public void SetTutorialSprite(ref Sprite? sprite)
        {
            if (sprite == null)
            {
                sprite = new Sprite();
            }
            if (sprite.Texture != _texture)
            {
                sprite.Texture = _texture;
                sprite.Origin = new Vector2f(58, 49) / 2f;
            }
            sprite.TextureRect = _tutorial;
        }

        private IntRect GetLoc(int x, int y, int w, int h)
        {
            return new IntRect(x, y, w, h);
        }
    }
}