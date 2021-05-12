using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Weigher : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/weigher.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Weigher"));
            obj.Position = new Vector3(3.5f, -4.0f, 0.0f);
            obj.Scale *= 0.5f;

            return obj;
        }
    }
}
