using Godot;
using System;

public partial class WASDMovement : Node
{
    [Export] public float Speed = 1000;
    [Export] public float Acceleration = 30;
    [Export] public float Deceleration = 40;
    [Export] public float MaxVelocity = 350;
    [Export] public CharacterBody2D Player;

    public override void _Process(double delta)
    {
        Vector2 velocity = MoveSmoothly();
        Player.Velocity = velocity * Speed * (float)delta;
    }

    private Vector2 MoveSmoothly()
	{
		Vector2 _velocity = Vector2.Zero;
		// Left
		if (Input.IsActionPressed("move_left") && _velocity.X > -MaxVelocity)
		{
			_velocity.X -= Acceleration;
		}
		else
		{
			if (_velocity.X < 0)
			{
				if (_velocity.X + Deceleration > 0)
					_velocity.X = 0;
				else
					_velocity.X += Deceleration;
			}
		}

		// Right
		if (Input.IsActionPressed("move_right") && _velocity.X < MaxVelocity)
		{
			_velocity.X += Acceleration;
		}
		else
		{
			if (_velocity.X > 0)
			{
				if (_velocity.X - Deceleration < 0)
					_velocity.X = 0;
				else
					_velocity.X -= Deceleration;
			}
		}

		// Up
		if (Input.IsActionPressed("move_up") && _velocity.Y > -MaxVelocity)
		{
			_velocity.Y -= Acceleration;
		}
		else
		{
			if (_velocity.Y < 0)
			{
				if (_velocity.Y + Deceleration > 0)
					_velocity.Y = 0;
				else
					_velocity.Y += Deceleration;
			}
		}

		// Down
		if (Input.IsActionPressed("move_down") && _velocity.Y < MaxVelocity)
		{
			_velocity.Y += Acceleration;
		}
		else
		{
			if (_velocity.Y > 0)
			{
				if (_velocity.Y - Deceleration < 0)
					_velocity.Y = 0;
				else
					_velocity.Y -= Deceleration;
			}
		}
		return _velocity;
	}
}
