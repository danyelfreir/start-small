using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public float FollowSpeed = 300;
	[Export]
	public Strength strengthComponent;
	private List<Area2D> InRange = new List<Area2D>();
	public Node2D Target;
	private int[] LevelSize = new int[4];

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (InRange.Count > 0)
		{
			float closestLen = float.PositiveInfinity;
			foreach (var node in InRange)
			{
				Vector2 direction = node.Position - Position;
				float length = direction.Length();
				Node2D parentBody = node.GetParent() as Node2D;
				if (length < closestLen || parentBody is Player)
				{
					closestLen = length;
					Target = parentBody;
					if (parentBody is Player)
					{

						break;
					}
				}
			}
		}
		else
		{
			Target = null;
		}
		if (strengthComponent != null)
		{
			Scale = new Vector2(strengthComponent.GetValue(), strengthComponent.GetValue());
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}

	public void _OnAreaEntered(Area2D area)
	{
		// Check if body is self
		if (area.GetParent() == this)
			return;
		// Check if body has name "AggroArea"
		if (area.Name != "AggroArea")
			return;
		InRange.Add(area);
	}

	public void _OnAreaExited(Area2D area)
	{
		// Check if body is self
		if (area.GetParent() == this)
			return;
		// Check if body has name "AggroArea"
		if (area.Name != "AggroArea")
			return;
		InRange.Remove(area);
	}

	public void _OnFightAreaEntered(Area2D body)
	{
		// Check if body is self
		if (body.GetParent() == this)
			return;
		// Check if body has name "FightArea"
		if (body.Name != "FightArea")
			return;
		// Get strength component of other
		Strength other = body.GetParent().GetNode<Strength>("Strength");
		GD.Print("Enemy strength: " + strengthComponent.GetValue() + " Other strength: " + other.GetValue());
		if (other != null && strengthComponent != null)
		{
			if (this.strengthComponent.Compare(other) < 0) // this is weaker
			{
				other.LevelUp(this.strengthComponent.GetValue());
				QueueFree();
			}
			else if (this.strengthComponent.Compare(other) > 0) // this is stronger
			{
				strengthComponent.LevelUp(other.GetValue());
				body.GetParent().QueueFree();
			}
			else
			{
				// equal strength, kill randomly
				if (GD.Randf() < 0.5)
				{
					other.LevelUp(this.strengthComponent.GetValue());
					QueueFree();
				}
				else
				{
					strengthComponent.LevelUp(other.GetValue());
					body.GetParent().QueueFree();
				}
			}
		}
	}
}
