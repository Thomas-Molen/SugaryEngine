﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Enum;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Camera;

namespace PlatformerSpeedRunner.Objects
{
    public class Player : RenderAbleObject
    {
        public PlayerMovementHelper Movement = new PlayerMovementHelper();
        private AnimationHelper Animation = new AnimationHelper();
        public CameraMode cameraState = CameraMode.Horizontal;
        private Animations AnimationState;

        private const int BBWidth = 45;
        private const int BBHeight = 55;

        private readonly AnimationObject IdleAnimation;
        private readonly AnimationObject RunningRightAnimation;
        private readonly AnimationObject RunningLeftAnimation;
        private readonly AnimationObject FallingRightAnimation;
        private readonly AnimationObject FallingLeftAnimation;
        private readonly AnimationObject JumpingRightAnimation;
        private readonly AnimationObject JumpingLeftAnimation;
        private readonly AnimationObject DeathAnimation;

        public Player(Texture2D Texture = null)
        {
            base.Texture.SetTexture(Texture);
            BoundingBox.AddBoundingBox(new BoundingBoxObject(new Vector2(1, 0), BBWidth, BBHeight));
            AnimationState = Animations.Idle;

            RunningRightAnimation = Animation.CreateAnimation("Player\\Running\\PlayerRunningRight", 12, 24);
            RunningLeftAnimation = Animation.CreateAnimation("Player\\Running\\PlayerRunningLeft", 12, 24);
            IdleAnimation = Animation.CreateAnimation("Player\\Idle\\PlayerIdle", 11, 55);
            FallingRightAnimation = Animation.CreateAnimation("Player\\FallingRight");
            FallingLeftAnimation = Animation.CreateAnimation("Player\\FallingLeft");
            JumpingRightAnimation = Animation.CreateAnimation("Player\\JumpingRight");
            JumpingLeftAnimation = Animation.CreateAnimation("Player\\JumpingLeft");
            DeathAnimation = Animation.CreateAnimation("Player\\Death\\Death", 6, 12);
        }

        public void PlayerUpdate()
        {
            SetCorrectAnimation();
            Movement.PlayerPhysics(this);
        }

        public void Grapple(int timeCharged, CameraHelper camera)
        {
            if (timeCharged < 10)
            {
                return;
            }
            else if (timeCharged >= 30)
            {
                CalculateGrapple(30, camera);
            }
            else
            {
                CalculateGrapple(timeCharged, camera);
            }
        }

        public AnimationObject GetAnimationState()
        {
            return AnimationState switch
            {
                Animations.Death => DeathAnimation,
                Animations.Idle => IdleAnimation,
                Animations.RunningRight => RunningRightAnimation,
                Animations.RunningLeft => RunningLeftAnimation,
                Animations.FallingRight => FallingRightAnimation,
                Animations.FallingLeft => FallingLeftAnimation,
                Animations.JumpingRight => JumpingRightAnimation,
                Animations.JumpingLeft => JumpingLeftAnimation,
                _ => IdleAnimation,
            };
        }

        private void CalculateGrapple(int timeCharged, CameraHelper camera)
        {
            MouseState mouseState = Mouse.GetState();
            float yGrappleDistance = mouseState.Y - Position.position.Y;

            if (yGrappleDistance < -700)
            {
                Movement.yVelocity += -700 / 135 * timeCharged / 10;
            }
            else
            {
                Movement.yVelocity += yGrappleDistance / 135 * timeCharged / 10;
            }
            Movement.xVelocity += (-(camera.transform.Translation.X + 23) + mouseState.X - (Position.position.X + (Texture.Width / 2))) / 150 * timeCharged / 10;
        }

        private void SetCorrectAnimation()
        {
            if (Movement.xVelocity == 0 && Movement.yVelocity == 0 && AnimationState != Animations.Idle)
            {
                AnimationState = Animations.Idle;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity >= 0 &&AnimationState != Animations.JumpingRight)
            {
                AnimationState = Animations.JumpingRight;
            }
            else if (Movement.yVelocity < 0 && Movement.xVelocity <= 0 && AnimationState != Animations.JumpingRight)
            {
                AnimationState = Animations.JumpingLeft;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity >= 0 && AnimationState != Animations.FallingRight)
            {
                AnimationState = Animations.FallingRight;
            }
            else if (Movement.yVelocity > 0 && Movement.xVelocity <= 0 && AnimationState != Animations.FallingRight)
            {
                AnimationState = Animations.FallingLeft;
            }
            else if (Movement.xVelocity > 0 && Movement.yVelocity == 0 && AnimationState != Animations.RunningRight)
            {
                AnimationState = Animations.RunningRight;
            }
            else if (Movement.xVelocity < 0 && Movement.yVelocity == 0 && AnimationState != Animations.RunningLeft)
            {
                AnimationState = Animations.RunningLeft;
            }
        }
    }
}