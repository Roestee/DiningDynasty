namespace Utilities.Helpers
{
    public static class FloatExtensions
    {
        public static float ReMap(this float input, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            return outputMin + (input - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
        }
    }
}