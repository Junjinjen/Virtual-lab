using App.Scripts;
using JUnity;
using JUnity.Utilities;

namespace App.Objects
{
    public class ObjectUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var gameObjectUI = new GameObject("UI");
            gameObjectUI.AddScript<UIScript>();
           return gameObjectUI;
        }
    }
}
