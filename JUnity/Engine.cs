using JUnity.Services.Graphics;
using JUnity.Services.Input;
using JUnity.Services.UI;
using JUnity.Utilities;
using SharpDX.Windows;
using System;
using System.Diagnostics;

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
            GraphicsSettings = GraphicsSettings.Default;
            UIController = new UIController();
        }

        public static Engine Instance { get; private set; }

        internal GraphicsRenderer GraphicsRenderer { get; }

        internal UIController UIController { get; }

        internal GameObjectCollection Scene { get; }

        public GraphicsSettings GraphicsSettings { get; set; }

        public InputManager InputManager { get; }

        public void Run()
        {
            GraphicsRenderer.Initialize(GraphicsSettings);
            InputManager.Initialize(GraphicsRenderer.RenderForm);

            _sceneInitializer.Seed(Scene);

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
        }

        public void Dispose()
        {
            InputManager.Dispose();
            GraphicsRenderer.Dispose();
        }
    }
}
