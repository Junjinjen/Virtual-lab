using JUnity.Services.Graphics;
using JUnity.Services.UI.Elements;

namespace ConsoleApp1
{
    class TestBtn : Button
    {
        public TestBtn()
        {
            Click += (o, x) => System.Console.WriteLine("Click");
        }

        protected override void Render(GraphicsRenderer renderer)
        {
        }
    }
}
