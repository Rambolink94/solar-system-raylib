namespace SolarSystem.Utils;

public static class Random
{
    public static float Range(float min, float max)
    {
        return System.Random.Shared.NextSingle() * (max - min) + min;
    }

    public static int Range(int min, int max)
    {
        return System.Random.Shared.Next(min, max);
    }
}