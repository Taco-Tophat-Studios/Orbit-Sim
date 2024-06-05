using Godot;
using System;

public partial class GraphPoint : Node2D
{
	[Export]
	Universe u;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override void _Draw()
    {
        for (int i = 0; i < u.points.GetLength(0); i++)
        {
			DrawCircle(u.points[i, 0], 2, u.red);
            DrawCircle(u.points[i, 1], 2, u.red);
        }
    }
}
