﻿using JUnity.Services.Graphics;
using JUnity.Services.Graphics.UI;
using JUnity.Services.Graphics.Utilities;
using JUnity.Services.Input;
using JUnity.Services.UI;
using JUnity.Utilities;
using SharpDX.Windows;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConsoleApp1")]
namespace JUnity
{
    public sealed class Engine : IDisposable
    {
        private const double FixedUpdatePeriod = 0.01;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ISceneInitializer _sceneInitializer;
        private long _lastTickCount;
        private double _elapsedTime;

        public Engine(ISceneInitializer initializer)
        {
            _sceneInitializer = initializer;
            Instance = this;

            Scene = new GameObjectCollection(null);
            InputManager = new InputManager();
            GraphicsRenderer = new GraphicsRenderer();
            UIRenderer = new UIRenderer();
            GraphicsSettings = GraphicsSettings.Default;
            UIController = new UIController();
        }

        public static Engine Instance { get; private set; }

        internal GraphicsRenderer GraphicsRenderer { get; }

        internal UIRenderer UIRenderer { get; private set; }

        internal UIController UIController { get; }

        internal GameObjectCollection Scene { get; }

        public GraphicsSettings GraphicsSettings { get; set; }

        public InputManager InputManager { get; }

        public Size WindowSize { get => GraphicsRenderer.RenderForm.Size; set => GraphicsRenderer.RenderForm.Size = value; }

        public void Run()
        {
            GraphicsRenderer.Initialize(GraphicsSettings);
            UIRenderer.Initialize(GraphicsRenderer.RenderForm);
            InputManager.Initialize(GraphicsRenderer.RenderForm);

            _sceneInitializer.Seed(Scene);

            foreach (var gameObject in Scene)
            {
                gameObject.OnStartup();
            }

            _stopwatch.Start();

            RenderLoop.Run(GraphicsRenderer.RenderForm, RenderCallback);
        }

        public void Stop()
        {
            _stopwatch.Stop();
            GraphicsRenderer.RenderForm.Close();
        }

        private void RenderCallback()
        {
            Scene.CommitRemove();

            var currentTickCount = _stopwatch.ElapsedTicks;
            var deltaTime = (currentTickCount - _lastTickCount) / (double)TimeSpan.TicksPerSecond;
            _lastTickCount = currentTickCount;

            _elapsedTime += deltaTime;
            if (_elapsedTime >= FixedUpdatePeriod)
            {
                InputManager.Update();
                foreach (var gameObject in Scene)
                {
                    if (gameObject.IsActive)
                    {
                        gameObject.OnFixedUpdate(_elapsedTime);
                    }
                }

                _elapsedTime = 0.0;
            }

            foreach (var gameObject in Scene)
            {
                if (gameObject.IsActive)
                {
                    gameObject.OnUpdate(deltaTime);
                }
            }

            GraphicsRenderer.RenderScene();
            UIRenderer.RenderUI();
        }

        public void Dispose()
        {
            InputManager.Dispose();
            GraphicsRenderer.Dispose();
        }
    }
}
