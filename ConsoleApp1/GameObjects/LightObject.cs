using JUnity;
using JUnity.Components.Lighning;
using JUnity.Utilities;
using SharpDX;

namespace Lab3.GameObjects
{
    public class LightObject : IGameObjectCreator
    {
        public GameObject Create()
        {
            var obj = new GameObject();
            obj.AddComponent<DirectionLight>();
            obj.GetComponent<DirectionLight>().Direction = new Vector3(0f, -1f, 0.5f);
            obj.GetComponent<DirectionLight>().Direction.Normalize();
            obj.GetComponent<DirectionLight>().Color = Color3.White;

            return obj;
        }
    }
}
