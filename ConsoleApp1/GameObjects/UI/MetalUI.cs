using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.UI;

namespace Lab3.GameObjects.UI
{
    public class MetalUI : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("MetalUI");
            obj.AddScript<MetalPanelUI>();
            return obj;
        }
    }
}
