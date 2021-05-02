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

        public static GameObject CreateAndRegister<TCreator>()
            where TCreator : IGameObjectCreator
        {
            var gameObject = Create<TCreator>();
            Engine.Instance.Scene.Add(gameObject);
            return gameObject;
        }

        public static GameObject CreateAndRegister(IGameObjectCreator creator)
        {
            var gameObject = Create(creator);
            Engine.Instance.Scene.Add(gameObject);
            return gameObject;
        }
    }
}
