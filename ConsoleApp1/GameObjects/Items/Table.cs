using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Table : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/table.fbx";
            var obj = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Table"));
            obj.Position = new Vector3(0.0f, -4.5f, 0.0f);
            obj.Scale *= 2f;

            return obj;
        }
    }
}
