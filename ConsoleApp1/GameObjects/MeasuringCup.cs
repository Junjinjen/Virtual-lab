using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Components.Rendering;
using JUnity.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class MeasuringCup : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/MeasuringCups.fbx";
            GameObject obj = GameObjectFactory.Create(new FbxObjectCreator(file, "MeasuringCup"));
            obj.Position = new Vector3(-1.5f, -3f, 0.0f);
            obj.Scale = Vector3.One * 1.5f;
            file = @"Meshes/water.fbx";
            obj.Children.Add(GameObjectFactory.Create(new FbxObjectCreator(file, "Water")));
            obj.Children[4].Position = new Vector3(0f, 1.8f, 0f);
            obj.Children[4].Scale = Vector3.One * 0.9f + Vector3.UnitZ * 5;
            obj.Children[4].Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            var mat = obj.Children[4].GetComponent<MeshRenderer>().Material;
            mat.AmbientCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            mat.DiffusionCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            mat.SpecularCoefficient = new Color4(1f, 1f, 1f, 0.4f);
            var rb = obj.Children[3].AddComponent<Rigidbody>();
            rb.UseGravity = false;
            rb.AddCollider(new BoxCollider(-Vector3.One * 0.2f, Vector3.One * 0.2f, "MeasuringCupCollider"));
            GameObject tmp = obj.Children[1];
            GameObject tmp2 = obj.Children[2];
            GameObject tmp3 = obj.Children[4];
            obj.Children[1] = tmp2;
            obj.Children[2] = tmp3;
            obj.Children[4] = tmp;
            return obj;
        }
    }
}
