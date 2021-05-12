using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Background : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/background.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Background"));
            obj.Position = new Vector3(0.0f, 0.0f, 7.5f);
            obj.Scale *= 6f;

            return obj;
        }
    }
}
