﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using PlatformerSpeedRunner.Enum;
using PlatformerSpeedRunner.Objects.Base;
using PlatformerSpeedRunner.Input.Base;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner.Objects;
using PlatformerSpeedRunner.Camera;

namespace PlatformerSpeedRunner.States.Base
{
    public abstract class BaseGameState
    {
        public GameState currentState;
        private protected bool debug = false;

        public BasicObject backgroundImage;

        public CameraHelper camera;

        private ContentManager baseContentManager;

        private SpriteFont font;

        protected int baseViewportHeight;
        protected int baseViewportWidth;

        private readonly List<RenderAbleObject> gameObjects = new List<RenderAbleObject>();
        private readonly List<Text> textObjects = new List<Text>();

        protected InputManager InputManager { get; set; }

        protected abstract void SetInputManager();

        private const string fallBackTexture = "ErrorSprite";

        public void Initialize(ContentManager contentManager, int viewportWidth, int viewportHeight, CameraHelper inputCamera)
        {
            camera = inputCamera;
            baseViewportHeight = viewportHeight;
            baseViewportWidth = viewportWidth;

            baseContentManager = contentManager;

            font = baseContentManager.Load<SpriteFont>("Fonts\\GuiFont");

            SetInputManager();
        }

        public abstract void LoadContent();
        public void UnloadContent()
        {
            baseContentManager.Unload();
        }

        public void Update(GameTime gameTime)
        {
            UpdateGameState(gameTime);
        }

        public abstract void HandleInput();

        public abstract void UpdateGameState(GameTime gameTime);

        public event EventHandler<BaseGameState> OnStateSwitched;


        protected Texture2D LoadTexture(string textureName)
        {
            try
            {
                var texture = baseContentManager.Load<Texture2D>(textureName);
                return texture;
            }
            catch (Microsoft.Xna.Framework.Content.ContentLoadException)
            {
                var texture = baseContentManager.Load<Texture2D>(fallBackTexture);
                return texture;
            }
        }


        protected void SwitchState(BaseGameState gameState)
        {
            OnStateSwitched?.Invoke(this, gameState);
        }

        protected void AddGameObject(RenderAbleObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        protected void RemoveGameObject(RenderAbleObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }

        protected void AddTextObject(Text textObject)
        {
            textObjects.Add(textObject);
        }

        protected void RemoveTextObject(Text textObject)
        {
            textObjects.Remove(textObject);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
            {
                if (debug)
                {
                    //gameObject.RenderBoundingBoxes(spriteBatch);
                }
                gameObject.RenderSprite(spriteBatch);
            }
        }

        public void RenderText(SpriteBatch spriteBatch)
        {
            foreach (var textObject in textObjects)
            {
                textObject.Render.RenderText(spriteBatch, font, textObject);
            }
        }

        public virtual void RenderBoundingBoxes(SpriteBatch spriteBatch)
        { }
    }
}