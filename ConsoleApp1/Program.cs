using JUnity;
using JUnity.Services.UI.Elements;
using JUnity.Utilities;

namespace ConsoleApp1
{
    class Init : ISceneInitializer
    {
        public void Seed(GameObjectCollection scene)
        {
            var btn = new TestBtn();
            btn.Width = 0.5f;
            btn.Height = 0.5f;
            Engine.Instance.UIController.RegisterElement(btn);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var engine = new Engine(new Init()))
            {
                engine.Run();
            }
        }
    }
}
