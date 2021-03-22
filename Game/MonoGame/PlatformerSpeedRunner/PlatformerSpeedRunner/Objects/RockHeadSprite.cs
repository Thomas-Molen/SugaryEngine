﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class RockHeadSprite : BaseGameObject
    {
        private const int BBWidth = 129;
        private const int BBHeight = 129;
        private int minPos;
        private int maxPos;

        private int movementSpeed = 2;
        public int velocity;

        Texture2D idleTexture;

        public RockHeadSprite(Texture2D texture, int inputMinPos, int inputMaxPos)
        {
            baseTexture = texture;
            idleTexture = texture;
            minPos = inputMinPos;
            maxPos = inputMaxPos;

            velocity = movementSpeed;

            AddBoundingBox(new BoundingBox(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void Movement()
        {
            if (baseTexture != idleTexture)
            {
                baseTexture = idleTexture;
            }

            if (Position.X >= maxPos)
            {
                velocity = -movementSpeed;
            }
            else if (Position.X <= minPos)
            {
                velocity = movementSpeed;
            }
            Position = new Vector2(Position.X + velocity, Position.Y);
        }

        public void ChangeTexture(Texture2D texture)
        {
            baseTexture = texture;
        }
    }
}
