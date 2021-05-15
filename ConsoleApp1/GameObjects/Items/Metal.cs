using JUnity;
using JUnity.Components.Audio;
using JUnity.Components.Physics;
using JUnity.Components.Physics.Colliders;
using JUnity.Utilities;
using Lab3.Scripts.Interactions;
using SharpDX;

namespace Lab3.GameObjects.Items
{
    public class Metal : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject("Metal");
            obj.Position = new Vector3(6.5f, -3.8f, 0.0f);
            obj.Scale *= 0.75f;

            var file = @"Meshes/aluminum.fbx";
            var metal = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Aluminum"));
            obj.Children.Add(metal);
            file = @"Meshes/brass.fbx";
            metal = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Brass"));
            obj.Children.Add(metal);
            file = @"Meshes/steel.fbx";
            metal = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "Steel"));
            obj.Children.Add(metal);
            file = @"Meshes/castIron.fbx";
            metal = GameObjectFactory.CreateAndRegister(new FbxObjectCreator(file, "CastIron"));
            obj.Children.Add(metal);

            obj.AddScript<MetalScript>();
            var rb = obj.AddComponent<Rigidbody>();
            rb.AddCollider(new BoxCollider(-Vector3.One * 0.5f, Vector3.One * 0.5f));
            file = @"Audio/choice.wav";
            var audio = obj.AddComponent<AudioPlayer>();
            audio.SetAudio(file);
            audio.Repeat = false;

            return obj;
        }
    }
}
