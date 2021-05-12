using JUnity.Components;
using JUnity.Services.Input;
using System;

namespace Lab3.Scripts.Interactions
{
    public class UserInteractionScript : Script
    {
        public override void Start()
        {
            MouseGrip.OnClickObject += (o, e) =>
            {
                Console.WriteLine(e.Object.Name);
            };
        }

        public override void Update(double deltaTime)
        {
            
        }
    }
}
