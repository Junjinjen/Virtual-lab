using JUnity;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Services.Input;
using JUnity.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.GameObjects
{
    public class SolidBody : IGameObjectCreator
    {
        public GameObject Create()
        {
            var file = @"Meshes/object.fbx";
            var obj = GameObjectFactory.Create(new FbxObjectCreator(file, "Object"));
            obj.Position = new Vector3(1f, -2f, 0f);
            obj.Scale = Vector3.One * 1.5f;
            obj.Rotation = Quaternion.RotationYawPitchRoll(0, MathUtil.Pi / 2f, 0);
            var rb = obj.AddComponent<Rigidbody>();
            rb.UseGravity = false;
            rb.AddCollider(new BoxCollider(-Vector3.One * 0.5f, Vector3.One * 0.5f, "Sol"));
            return obj;
        }
    }
}
