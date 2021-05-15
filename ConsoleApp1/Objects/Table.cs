using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace App.Objects
{
    public class Table : IGameObjectCreator
    {

        public GameObject Create()
        {
            GameObject gameObject = GameObjectFactory.Create(new FbxObjectCreator("Meshes\\stol.fbx", "Table"));
            gameObject.Scale = new Vector3(0.02f);
            gameObject.Position = new Vector3(-3, -5, -13);
            return gameObject;
        }
    }
}
