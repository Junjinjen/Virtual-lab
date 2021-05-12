using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Metal : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/metal.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Metal"));
            obj.Position = new Vector3(6.5f, -4.0f, 0.0f);
            obj.Scale *= 0.75f;

            return obj;
        }
    }
}
