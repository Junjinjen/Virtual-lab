using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Stand : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/stand.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Stand"));
            obj.Position = new Vector3(0.0f, -4.0f, 0.0f);
            obj.Scale *= 1.5f;

            return obj;
        }
    }
}
