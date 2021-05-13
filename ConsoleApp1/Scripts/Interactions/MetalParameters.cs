namespace Lab3.Scripts.Interactions
{
    public class MetalParameters
    {
        public float Mass { get; private set; }

        public float SpecificHeat{ get; private set; }

        public MetalParameters(float density, float volume, float specificHeat)
        {
            Mass = volume * density;
            SpecificHeat = specificHeat;
        }
    }
}
