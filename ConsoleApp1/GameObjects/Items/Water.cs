using JUnity;
using JUnity.Utilities;
using Lab3.Scripts.MutableObjects;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Water : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/water.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Water"));
            obj.Position = new Vector3(0.0f, -3.45f, 0.0f);
            obj.Scale *= 1.5f;
            obj.AddScript<WaterScript>();

            return obj;
        }
    }
}
