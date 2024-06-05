using Godot;
using System;

public partial class Camera : Camera2D
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
        if (Input.IsActionJustReleased("MouseWUP") && !u.IL.HasFocus())
		{
			Zoom = new Vector2(Zoom.X * 1.25f, Zoom.Y * 1.25f);

		} else if (Input.IsActionJustReleased("MouseWDN") && !u.IL.HasFocus())
		{
            Zoom = new Vector2(Zoom.X * 0.8f, Zoom.Y * 0.8f);
        }

		if (Input.IsActionPressed("ui_left"))
		{
			Position = new Vector2(Position.X - 50 * (float)GetProcessDeltaTime(), Position.Y);
		}
        if (Input.IsActionPressed("ui_right"))
        {
            Position = new Vector2(Position.X + 50 * (float)GetProcessDeltaTime(), Position.Y);
        }
        if (Input.IsActionPressed("ui_up"))
        {
            Position = new Vector2(Position.X, Position.Y - 50 * (float)GetProcessDeltaTime());
        }
        if (Input.IsActionPressed("ui_down"))
        {
            Position = new Vector2(Position.X, Position.Y + 50 * (float)GetProcessDeltaTime());
        }
    }
}
