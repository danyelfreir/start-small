using Godot;

public partial class WanderState : State
{
	[Export] public float movementSpeed = 50f;
	private double wanderTimer;
	private Vector2 direction;

	public override void Update(double delta)
	{
		if (enemy.Target != null)
		{
			Strength myStrength = enemy.GetNode<Strength>("Strength");
			Strength targetStrength = enemy.Target.GetNode<Strength>("Strength");
			if (myStrength.Compare(targetStrength) < 0)
			{
				EmitSignal(SignalName.EnemyStateChanged, "flee");
				return;
			}
			else
			{
				EmitSignal(SignalName.EnemyStateChanged, "follow");
				return;
			}
		}

		if (wanderTimer > 0.0f)
		{
			wanderTimer -= delta;
		}
		else
		{
			RandomizeWander();
		}
	}

	public override void PhysicsUpdate(double delta)
	{
		enemy.Velocity = direction * movementSpeed;
	}

	public override void Enter()
	{
		RandomizeWander();
	}

	public override void Exit()
	{
		// No cleanup needed
	}

	private void RandomizeWander()
	{
		direction = new Vector2(
			GD.RandRange(-1, 1),
			GD.RandRange(-1, 1)
		).Normalized();
		wanderTimer = GD.RandRange(0, 3);
	}
}
