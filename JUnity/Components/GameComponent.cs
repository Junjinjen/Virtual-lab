namespace JUnity.Components
{
    public abstract class GameComponent
    {
        protected GameComponent(GameObject owner)
        {
            Owner = owner;
        }

        internal GameObject Owner { get; }

        internal virtual void Start() { }

        internal abstract void CallComponent(double deltaTime);
    }
}
