using Godot;

public partial class FleeState : State
{
	[Export] public float movementSpeed = 120f;
	private Vector2 direction;

	public override void Update(double delta)
	{
		if (enemy.Target == null)
		{
			EmitSignal(SignalName.EnemyStateChanged, "wander");
			return;
		}
		direction = (enemy.Position - enemy.Target.Position).Normalized();
	}

    public override void PhysicsUpdate(double delta)
    {
        enemy.Velocity = direction * movementSpeed;
	}

	public override void Enter()
	{
		// Optional: Add enter logic here
	}

	public override void Exit()
	{
		// Optional: Add exit logic here
	}
}
