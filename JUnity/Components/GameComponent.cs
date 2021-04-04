using System;

namespace JUnity.Components
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "Custom dispose logic")]
    public abstract class GameComponent : IDisposable
    {
        protected GameComponent(GameObject owner)
        {
            Owner = owner;
        }

        internal GameObject Owner { get; }

        internal virtual void Start() { }

        internal abstract void CallComponent(double deltaTime);

        public virtual void Dispose() { }
    }
}
