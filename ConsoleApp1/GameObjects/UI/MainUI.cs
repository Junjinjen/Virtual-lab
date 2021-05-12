using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.UI;

namespace Lab3.GameObjects.UI
{
    public class MainUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("MainUI");
            obj.AddScript<MainPanelUI>();
            return obj;
        }
    }
}
