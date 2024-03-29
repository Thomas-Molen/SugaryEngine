﻿using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects;

namespace PlatformerSpeedRunner.Camera
{
    public class CameraHelper
    {
        public Matrix transform { get; private set; }
        private Matrix position;
        private Matrix offset;
        public CameraMode cameraState { get; private set; }
        public void Follow(Player playerSprite)
        {
            if (playerSprite != null)
            {
                if (playerSprite.Position.position.X < Program.width / 2)
                {
                    position = Matrix.CreateTranslation(
                        -playerSprite.Position.position.X - (playerSprite.Texture.width / 2),
                        -playerSprite.Position.position.Y - (playerSprite.Texture.height / 2),
                        0);

                    offset = Matrix.CreateTranslation(
                        playerSprite.Position.position.X,
                        playerSprite.Position.position.Y,
                        0);
                }
                else
                {
                    switch (cameraState)
                    {
                        case CameraMode.Free:
                            position = Matrix.CreateTranslation(
                        -playerSprite.Position.position.X - (playerSprite.Texture.width / 2),
                        -playerSprite.Position.position.Y - (playerSprite.Texture.height / 2),
                        0);

                            offset = Matrix.CreateTranslation(
                                Program.width / 2,
                                Program.height / 2,
                                0);
                            break;

                        case CameraMode.Horizontal:
                            position = Matrix.CreateTranslation(
                       -playerSprite.Position.position.X - (playerSprite.Texture.width / 2),
                       -playerSprite.Position.position.Y - (playerSprite.Texture.height / 2),
                       0);

                            offset = Matrix.CreateTranslation(
                                Program.width / 2,
                                playerSprite.Position.position.Y,
                                0);
                            break;
                        case CameraMode.Vertical:
                            position = Matrix.CreateTranslation(
                        -playerSprite.Position.position.X - (playerSprite.Texture.width / 2),
                        -playerSprite.Position.position.Y - (playerSprite.Texture.height / 2),
                        0);

                            offset = Matrix.CreateTranslation(
                                Program.width / 2,
                                playerSprite.Position.position.Y,
                                0);
                            break;

                        default:
                            break;
                    }
                }
            }
            transform = position * offset;
        }

        public void Follow(Vector2 newPosition)
        {

            position = Matrix.CreateTranslation(
                -newPosition.X,
                -newPosition.Y,
                0);

            offset = Matrix.CreateTranslation(
                Program.width / 2,
                Program.height / 2,
                0);
                           
            transform = position * offset;
        }

        public void Follow()
        {
            transform = Matrix.CreateTranslation(0, 0, 0);
        }

        public Vector2 GetCameraBasedPosition(Vector2 BasePosition)
        {
            return new Vector2(-transform.Translation.X + BasePosition.X, -transform.Translation.Y + BasePosition.Y);
        }

        public void SetCameraMode(CameraMode mode)
        {
            cameraState = mode;
        }
    }
}
