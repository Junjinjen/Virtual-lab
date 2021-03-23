using System;

namespace JUnity.Utilities
{
    public static class GameObjectFactory
    {
        public static GameObject Create(IGameObjectCreator creator)
        {
            return creator.Create();
        }

        public static GameObject Create<TCreator>()
            where TCreator : IGameObjectCreator
        {
            var creator = Activator.CreateInstance<TCreator>();
            return Create(creator);
        }
    }
}
