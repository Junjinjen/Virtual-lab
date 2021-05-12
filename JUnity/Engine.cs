using JUnity.Components.Audio;
using JUnity.Services.Graphics;
using JUnity.Services.Input;
using JUnity.Services.UI;
using JUnity.Utilities;
using SharpDX.Windows;
using System;
using System.Diagnostics;
using System.Drawing;

namespace JUnity
{
    public sealed class Engine : IDisposable
    {
        private const double FixedUpdatePeriod = 0.01;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly ISceneInitializer _sceneInitializer;
        private Settings _settings;
        private long _lastTickCount;
        private double _elapsedTime;
        private bool _isStarted;

        public Engine(ISceneInitializer initializer)
        {
            _sceneInitializer = initializer;
            Instance = this;

            Scene = new GameObjectCollection(null);
            InputManager = new InputManager();
            GraphicsRenderer = new GraphicsRenderer();
            Settings = Settings.Default;
            UIController = new UIController();
        }

        public static Engine Instance { get; private set; }

        internal GraphicsRenderer GraphicsRenderer { get; }

        internal UIController UIController { get; }

        internal GameObjectCollection Scene { get; }

        public Settings Settings
        {
            get => _settings;
            set
            {
                if (_isStarted)
                {
                    throw new InvalidOperationException("Unable to change the settings after Engine start");
                }

                _settings = value;
            }
        }

        public InputManager InputManager { get; }

        public Size WindowSize { get => GraphicsRenderer.RenderForm.Size; set => GraphicsRenderer.RenderForm.Size = value; }

        public void Run()
        {
            _isStarted = true;
            GraphicsRenderer.Initialize(Settings);
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
            UIController.CreateDrawRequest();
        }

        public void Dispose()
        {
            InputManager.Dispose();
            GraphicsRenderer.Dispose();
        }
    }
}
