using System.Numerics;
using Raylib_cs;

namespace SolarSystem;

public class CelestialBody
{
    public float Radius { get; set; }
    public float Angle { get; set; }
    public float Distance { get; set; }
    
    public Vector2 Position { get; set; }
    
    public CelestialBody? Parent { get; set; }

    private CelestialBody[] _children;
    private readonly Color _color;
    private readonly float _orbitSpeed;

    public CelestialBody(float radius, float distance, float orbitSpeed, Color color)
    {
        Radius = radius;
        Distance = distance;
        Angle = Random.Shared.NextSingle() * MathF.Tau;
        _children = Array.Empty<CelestialBody>();
        _color = color;
        _orbitSpeed = orbitSpeed;
    }

    public void Orbit(float deltaTime)
    {
        Angle += _orbitSpeed * deltaTime;
        var x = MathF.Cos(Angle) * Distance;
        var y = MathF.Sin(Angle) * Distance;
        Position = new Vector2(x, y);
        
        foreach (CelestialBody child in _children)
        {
            child.Orbit(deltaTime);
        }
    }

    public void SpawnChildren(int count, int maxLevel)
    {
        SpawnChildren(count, maxLevel, 0);
    }

    private void SpawnChildren(int count, int maxLevel, int currentLevel)
    {
        _children = new CelestialBody[count];
        float previousDistance = 0;
        for (int i = 0; i < _children.Length; i++)
        {
            var radius = Radius * Utils.Random.Range(0.2f, 0.5f);
            var combinedRadius = Radius + radius;

            var distance = Distance + previousDistance + 20f + combinedRadius;
            // var distance = Distance + (float)(Random.Shared.NextDouble() * (combinedRadius * 2 - combinedRadius)) + combinedRadius + previousDistance;
            previousDistance = distance;

            var orbitSpeed = Utils.Random.Range(0.1f, 0.3f);
            
            var body = new CelestialBody(radius, distance, orbitSpeed, RandomColor());
            body.Parent = this;

            if (currentLevel < maxLevel)
            {
                body.SpawnChildren(Utils.Random.Range(0, count) + currentLevel, maxLevel, currentLevel + 1);
            }

            _children[i] = body;
        }
    }

    public void Draw()
    {
        var offsetX = 0f;
        var offsetY = 0f;

        if (Parent is not null)
        {
            offsetX = Parent.Position.X;
            offsetY = Parent.Position.Y;
        }

        var x = Position.X + offsetX;
        var y = Position.Y + offsetY;
        
        Raylib.DrawEllipse((int)x, (int)y, Radius, Radius, _color);
        
        foreach (CelestialBody child in _children)
        {
            child.Draw();
        }
    }

    private Color RandomColor()
    {
        var r = (int)(Random.Shared.NextSingle() * 255);
        var g = (int)(Random.Shared.NextSingle() * 255);
        var b = (int)(Random.Shared.NextSingle() * 255);
        
        Console.WriteLine($"{r},{g},{b}");

        return new Color(r, g, b, 255);
    }
}