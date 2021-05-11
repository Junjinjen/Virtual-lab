using JUnity;
using JUnity.Components.Rendering;
using JUnity.Utilities;
using SharpDX;


namespace App.Objects
{
    public class Wall : IGameObjectCreator
    {
        public GameObject Create()
        {
            GameObject gameObject = GameObjectFactory.Create(new FbxObjectCreator("Meshes\\wall.fbx"));
            gameObject.Scale = new Vector3(0.04f, 0.042f, 0.04f);
            gameObject.Rotation = Quaternion.RotationYawPitchRoll(0, 0, 0);
            gameObject.Position = new Vector3(0, 1.3f, 10);
            gameObject.GetComponent<MeshRenderer>().Material.EmissivityCoefficient = Color4.White;
            return gameObject;
        }
    }
}
