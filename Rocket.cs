using Godot;
using System;

public partial class Rocket : Sprite2D
{
	[Export]
	AnimatedSprite2D VelArrowEnd;
	

	bool draggingRocket;
	bool draggingVAE;
	Vector2 previousVAEPos;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        previousVAEPos = VelArrowEnd.GlobalPosition;
		GD.Print(VelArrowEnd.GlobalPosition.X+ " | " +  VelArrowEnd.GlobalPosition.Y);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("leftclick"))
		{
			if (this.GetRect().HasPoint(ToLocal(GetGlobalMousePosition())))
			{
				draggingRocket = true;
			}
			if ((GetGlobalMousePosition() - previousVAEPos).Length() <= 16*Mathf.Sqrt2)
			{
				GD.Print("Debug A");
				draggingVAE = true;
			}
		}
		else if (Input.IsActionJustReleased("leftclick"))
		{
			draggingRocket = false;
			draggingVAE = false;
            previousVAEPos = VelArrowEnd.GlobalPosition;
            Rotation = Mathf.Atan2(previousVAEPos.Y - GlobalPosition.Y, previousVAEPos.X - GlobalPosition.X);
			VelArrowEnd.GlobalPosition = previousVAEPos;
			VelArrowEnd.GlobalRotation = 0;
		}

		if (draggingRocket)
		{
			GlobalPosition = GetGlobalMousePosition();
		}
		if (draggingVAE)
		{
			VelArrowEnd.GlobalPosition = GetGlobalMousePosition();
		}
	}
}
