using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Components.Rendering;
using JUnity.Utilities;
using SharpDX;

namespace Lab2.GameObjects
{
    public class Calorimeter : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/Calorimeters.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Calorimeter"));
            obj.Position = new Vector3(-4.5f, -3f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(MathUtil.Pi - 0.5f, 0, 0);
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "WaterCalorimeter")));
            obj.Children[3].Position = new Vector3(0f, 0f, 0f);
            obj.Children[3].Scale = Vector3.One;
            obj.Children[3].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            var mat = obj.Children[3].GetComponent<MeshRenderer>().Material;
            mat.AmbientCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            mat.DiffusionCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            mat.SpecularCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            var rb = obj.Children[1].AddComponent<Rigidbody>();
            rb.UseGravity = false;
            rb.AddCollider(new BoxCollider(-Vector3.One * 0.3f, Vector3.One * 0.3f, "CalorimeterCollider"));
            GameObject tmp4 = obj.Children[0];
            GameObject tmp5 = obj.Children[2];
            GameObject tmp6 = obj.Children[3];
            obj.Children[0] = tmp5;
            obj.Children[2] = tmp6;
            obj.Children[3] = tmp4;

            return obj;
        }
    }
}
