using JUnity.Components.Rendering;

namespace JUnity.Utilities
{
    public class FbxObjectCreator : IGameObjectCreator
    {
        private readonly string _filename;
        private readonly string _objectName;

        public FbxObjectCreator(string filename, string objectName = null)
        {
            _filename = filename;
            _objectName = objectName;
        }

        public GameObject Create()
        {
            var collection = MeshLoader.LoadScene(_filename);
            GameObject root;

            if (collection.Count == 1)
            {
                root = CreateFromNodeDescription(collection[0], _objectName);
            }
            else if (collection.Count > 0)
            {
                root = new GameObject(_objectName);
                foreach (var item in collection)
                {
                    root.Children.Add(CreateFromNodeDescription(item));
                }
            }
            else
            {
                return null;
            }

            return root;
        }

        private GameObject CreateFromNodeDescription(NodeDescription description, string objectName = null)
        {
            var obj = new GameObject(objectName ?? description.Name);
            if (description.NodeMeshes.Count == 0)
            {
                AppendTranslation(obj, description);
            }
            else if (description.NodeMeshes.Count == 1)
            {
                obj.AddComponent<MeshRenderer>().Initialize(description.NodeMeshes[0], Engine.Instance.Settings.DefaultVertexShader, Engine.Instance.Settings.DefaultPixelShader);
                AppendTranslation(obj, description);
            }
            else
            {
                for (int i = 0; i < description.NodeMeshes.Count; i++)
                {
                    var child = new GameObject($"{description.Name}_{i}");

                    child.AddComponent<MeshRenderer>().Initialize(description.NodeMeshes[i], Engine.Instance.Settings.DefaultVertexShader, Engine.Instance.Settings.DefaultPixelShader);
                    AppendTranslation(child, description);

                    obj.Children.Add(child);
                }
            }

            AppendChildren(obj, description);
            return obj;
        }

        private void AppendChildren(GameObject obj, NodeDescription description)
        {
            foreach (var item in description.Children)
            {
                obj.Children.Add(CreateFromNodeDescription(item));
            }
        }

        private void AppendTranslation(GameObject obj, NodeDescription description)
        {
            obj.Position = description.Position;
            obj.Rotation = description.Rotation;
            obj.Scale = description.Scale;
        }
    }
}
