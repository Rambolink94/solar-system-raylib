using System.Numerics;
using Raylib_cs;

namespace SolarSystem;

class Program
{
    private const int Width = 1920;
    private const int Height = 1080;
    
    [STAThread]
    public static void Main()
    {
        Raylib.InitWindow(Width, Height, "Solar System");

        var camera = new Camera2D
        {
            Target = new Vector2(0, 0),
            Offset = new Vector2(Width / 2f, Height / 2f),
            Zoom = 1f,
        };

        var sun = new CelestialBody(40f, 0f, 0f, Color.Yellow);
        sun.SpawnChildren(5, 2);
        
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            
            Raylib.BeginMode2D(camera);
            
            sun.Draw();
            sun.Orbit(deltaTime);
            
            Raylib.EndDrawing();
        }
        
        Raylib.CloseWindow();
    }
}