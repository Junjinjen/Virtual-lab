using Engine.Services;
using JUnity.UI;

namespace JUnity
{
    public class TESTBUTTON : Button
    {
        public TESTBUTTON()
        {
            Click += TESTBUTTON_Click;
        }

        private void TESTBUTTON_Click(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("Click");
        }

        protected override void OnMouseDown()
        {
            System.Console.WriteLine("Down");
        }

        protected override void OnMouseUp()
        {
            System.Console.WriteLine("Up");
        }

        internal override void Render(GraphicsRenderer renderer)
        {
        }
    }
}
