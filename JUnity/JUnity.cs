using Engine.Services;
using Engine.Utilities;
using SharpDX.Windows;
using System;
using System.Diagnostics;

namespace Engine
{
    public class JUnity : IDisposable
    {
        private const double FixedUpdatePeriod = 0.01;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private long _lastTickCount;
        private double _elapsedTime;
        private bool _isDisposed;

        public JUnity(ISceneInitializer initializer)
            : this()
        {
            initializer.Seed(Scene);
        }

        public JUnity()
        {
            Instance = this;
            Scene = new GameObjectCollection(null);
            InputManager = new InputManager();
            GraphicsRenderer = new GraphicsRenderer();
            UIController = new UIController();
        }

        public static JUnity Instance { get; private set; }

        internal GraphicsRenderer GraphicsRenderer { get; }

        internal UIController UIController { get; }

        public GameObjectCollection Scene { get; }

        public InputManager InputManager { get; }

        public void Run()
        {
            GraphicsRenderer.Initialize();
            InputManager.Initialize(GraphicsRenderer.RenderForm);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    InputManager.Dispose();
                    GraphicsRenderer.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~JUnity()
        {
            Dispose(false);
        }
    }
}
