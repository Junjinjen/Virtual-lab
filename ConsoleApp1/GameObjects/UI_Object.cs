using JUnity;
using JUnity.Utilities;
using Lab2.Scripts;

namespace Lab2.GameObjects
{
    public class UI_Object : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObject = new GameObject("UI");
            gameObject.AddScript<UI_Script>();
            return gameObject;
        }
    }
}
