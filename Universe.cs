using Godot;
using System;

public partial class Universe : Node2D
{
	float G = 0.1f;
	[Export]
	float M = 10000000; //Sun
	[Export]
	float m = 1f; //apollo 11 lunar module
	float[] masses = {10000000, 4000000, 3000000, 1000000};
	int index = 0;

	[Export]
	AnimatedSprite2D Body;
	[Export]
	Sprite2D Rocket;
	[Export]
	AnimatedSprite2D VelArrowEnd;

	[Export]
	public ItemList IL;
	[Export]
	SpinBox SBM;
	[Export]
	SpinBox SBm;
	[Export]
	TextEdit TE;
	[Export]
	Label L;

	[Export]
	GraphPoint GraphCenter;

	public Color red = new Color(1, 0, 0);

	float mu;
	float h;

	float r;
	float a;
	float ex;
	float ey;

	float fx;
	float fy;
	float f;
	float angle;

	public Vector2[,] points = new Vector2[2561, 2];
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		ResetBoxes();
		GraphCenter.QueueRedraw();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Draw"))
		{
			DrawConic();
            GraphCenter.QueueRedraw();
        }
	}
    
    private void DrawConic()
	{
		mu = G * (M + m);
		h = Rocket.Position.X * VelArrowEnd.Position.Y - Rocket.Position.Y * VelArrowEnd.Position.X;

		r = Rocket.Position.Length(); //rocket is a child of planet -- position will be relative
		a = mu * r / (2 * mu - r * (Mathf.Pow(VelArrowEnd.Position.X, 2) + Mathf.Pow(VelArrowEnd.Position.Y, 2)));
		ex = (Rocket.GlobalPosition.X / r) - (h * VelArrowEnd.Position.Y / mu);
		ey = (Rocket.GlobalPosition.Y / r) + (h * VelArrowEnd.Position.X / mu);
		fx = 2 * a * ex;
		fy = 2 * a * ey;
		
		//check for escape velocity here
		f = (new Vector2(fx, fy)).Length();
		angle = Mathf.Atan2(fy, fx);
		
		GraphCenter.GlobalRotation = angle;

		float tempY;
		bool graphed = false;

		for (int i = -1280; i <= 1280; i++)
		{
			tempY = LongFunction(i);
			
			if (tempY != -1)
			{
				points[i + 1280, 0] = new Vector2(i, tempY);
                points[i + 1280, 1] = new Vector2(i, -tempY);

                graphed = true;
            }
			
		}
		L.Text = "STATUS: ";
		if (graphed)
		{
			L.Text += "Orbiting";
		} else
		{
			L.Text += "Escaped!";
		}

		L.Text += "\nFOCAL DISTANCE (planet to empty): " + f;
		L.Text += "\nANGLE: " + angle;
		L.Text += "\nCURRENT VELOCITY: " + VelArrowEnd.Position.Length();
	}
	

	private float LongFunction(int x)
	{
		float v = Mathf.Sqrt((-8*Mathf.Pow(a, 2) * Mathf.Pow(f, 2)) + 
						  (-4*x*Mathf.Pow(f, 3)) + 
						  (Mathf.Pow(f, 4)) + 
						  (4*Mathf.Pow(x, 2)*Mathf.Pow(f, 2)) +
						  (16*Mathf.Pow(a, 2)*x*f) +
						  (16*Mathf.Pow(a, 4)) +
						  (-16*Mathf.Pow(a, 2)*Mathf.Pow(x, 2))
						  ) / (4*a);
		if (!float.IsNaN(v))
		{
			return v;
		} else
		{
			return -1;
		}
	}

	private void _on_item_list_item_clicked(long index, Vector2 at_position, long mouse_button_index)
	{
		this.index = (int)index;
		Body.Frame = this.index;
		ResetBoxes(0);
		DrawConic();
	}
	private void _on_spin_box_value_changed(double value)
	{
		M = (float)value;
		ResetBoxes(1);
        DrawConic();
    }
	private void _on_spin_box_2_value_changed(double value)
	{
		m = (float)value;
		ResetBoxes(2);
        DrawConic();
    }
	private void _on_text_edit_text_changed()
	{
		G = float.Parse(TE.Text);
		ResetBoxes(3);
        DrawConic();
    }


	private void ResetBoxes(int source = -1)
	{
		if (source != 0)
		{
			IL.Select(index);
		} 
		if (source != 1)
		{
			SBM.SetValueNoSignal(masses[index]);
		}
		if (source != 2)
		{
			SBm.SetValueNoSignal(m);
		}
		if (source != 3)
		{
			TE.Text = "" + G;
		}
        
    }
}


