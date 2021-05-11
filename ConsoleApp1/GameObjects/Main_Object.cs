using JUnity;
using JUnity.Utilities;
using Lab2.Scripts;

namespace Lab2.GameObjects
{
    public class Main_Object : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObject = new GameObject("Main");
            gameObject.AddScript<Main_Script>();
            return gameObject;
        }
    }
}
