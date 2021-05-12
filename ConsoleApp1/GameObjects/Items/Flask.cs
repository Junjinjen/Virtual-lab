using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Flask : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/flask.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Flask"));
            obj.Position = new Vector3(0.0f, -4.0f, 0.0f);
            obj.Scale *= 1.5f;

            return obj;
        }
    }
}
