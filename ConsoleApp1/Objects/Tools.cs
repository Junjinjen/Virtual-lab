using JUnity;
using JUnity.Utilities;
using SharpDX;
using System;


namespace App.Objects
{
    public class Tools : IGameObjectCreator
    {
        public GameObject Create()
        {
            GameObject gameObject = GameObjectFactory.Create(new FbxObjectCreator("Meshes\\scene.fbx"));
            gameObject.Scale = new Vector3(0.01f);
            gameObject.Position = new Vector3(-4, -4, -10);
            gameObject.Rotation = Quaternion.RotationYawPitchRoll(-MathUtil.PiOverTwo, 0, 0);
            return gameObject;
        }
    }
}
