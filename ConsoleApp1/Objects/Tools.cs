using App.Scripts;
using JUnity;
using JUnity.Utilities;
using SharpDX;


namespace App.Objects
{
    public class Tools : IGameObjectCreator
    {
        public GameObject Create()
        {
            GameObject gameObject = GameObjectFactory.Create(new FbxObjectCreator("Meshes\\scene.fbx", "Tools"));
            gameObject.Scale = new Vector3(0.01f);
            gameObject.Position = new Vector3(-6, -5, -8);
            gameObject.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.PiOverTwo, 0, 0);
            gameObject.AddScript<ToolsSript>();
            gameObject.Children[0].Children.Remove(gameObject.Children[0].Children[0]); //Assimp bug fix
            return gameObject;
        }
    }
}
