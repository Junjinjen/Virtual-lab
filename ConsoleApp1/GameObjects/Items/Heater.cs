using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Heater : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/heater.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Heater"));
            obj.Position = new Vector3(0.0f, -1.0f, 0.4f);
            obj.Scale *= 1.5f;

            return obj;
        }
    }
}
