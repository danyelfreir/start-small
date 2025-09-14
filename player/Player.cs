using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	[Export]
	public Strength strengthComponent;
	[Export]
	public Camera2D playerCamera;
	private Vector2 screenSize;
	private bool updateCamera = false;

	[Signal]
	public delegate void InVicinityEventHandler();

	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		strengthComponent.LevelUp(1);
	}

	public override void _Process(double delta)
	{
		if (strengthComponent != null)
		{
			Scale = new Vector2(strengthComponent.GetValue(), strengthComponent.GetValue());
			if (playerCamera != null)
			{
				playerCamera.Zoom = new Vector2(2.0f / strengthComponent.GetValue(), 2.0f / strengthComponent.GetValue());
			}
		}
		MoveAndSlide();
	}
}
