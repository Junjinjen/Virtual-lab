using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Device : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/device.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Device"));
            obj.Position = new Vector3(-3.5f, -4.0f, 0.0f);
            obj.Scale *= 1.2f;
            obj.AddScript<HeatingProcessScript>();

            return obj;
        }
    }
}
