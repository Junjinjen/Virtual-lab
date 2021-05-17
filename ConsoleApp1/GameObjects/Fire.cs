using JUnity;
using JUnity.Utilities;
using SharpDX;

namespace Lab2.GameObjects
{
    public class Fire : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/fire.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Fire"));
            obj.Position = new Vector3(-1.5f, -1.5f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            obj.IsActive = false;
            return obj;
        }
    }
}
