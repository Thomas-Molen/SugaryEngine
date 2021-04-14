﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects.Base;

namespace PlatformerSpeedRunner.Objects
{
    public class MovingRockHead : RenderAbleObject
    {
        public BoundingBoxHelper BoundingBox = new BoundingBoxHelper();
        private const int BBWidth = 129;
        private const int BBHeight = 129;
        private readonly int minPos;
        private readonly int maxPos;

        private readonly int movementSpeed = 2;
        public int velocity;
        private readonly Texture2D idleTexture;

        public MovingRockHead(Texture2D Texture, Vector2 Position, int inputMinPos, int inputMaxPos)
        {
            base.Position.SetPosition(Position);
            base.Texture.SetTexture(Texture);
            idleTexture = Texture;
            minPos = inputMinPos;
            maxPos = inputMaxPos;

            velocity = movementSpeed;

            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(0, 0), BBWidth, BBHeight));
        }

        public void Movement()
        {
            BoundingBox.UpdateBoundingBoxes(Position.position);
            if (Texture.texture != idleTexture)
            {
                Texture.SetTexture(idleTexture);
            }

            if (Position.position.X >= maxPos)
            {
                velocity = -movementSpeed;
            }
            else if (Position.position.X <= minPos)
            {
                velocity = movementSpeed;
            }
            Position.SetPosition(new Vector2(Position.position.X + velocity, Position.position.Y));
        }
    }
}