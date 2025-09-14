using Godot;
using System;

public partial class Main : Node2D
{
	private Vector2 screenSize;
	[Export] public PackedScene EnemyScene;
	[Export] public float spawnTimer = 1.0f;
	private float _spawnTimer;

	public override void _Ready()
	{
		_spawnTimer = spawnTimer;
		screenSize = GetViewportRect().Size;

		for (int i = 0; i < 50; i++)
		{
			var enemy = (Node2D)EnemyScene.Instantiate();
			var enemyLocation = new Vector2(
				(float)GD.RandRange(-1000, 1000), (float)GD.RandRange(-1000, 1000)
			// 40, 40
			);
			enemy.Position = enemyLocation;
			AddChild(enemy);
		}
	}

	public override void _Process(double delta)
	{
		_spawnTimer -= (float)delta;
		if (_spawnTimer <= 0.0f)
		{
			var enemy = (Node2D)EnemyScene.Instantiate();
			var enemyLocation = new Vector2(
				(float)GD.RandRange(-1000, 1000), (float)GD.RandRange(-1000, 1000)
			);
			enemy.Position = enemyLocation;
			AddChild(enemy);
			_spawnTimer = spawnTimer;
		}
	}
}
