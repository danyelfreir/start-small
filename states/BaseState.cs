using Godot;
using System;

public partial class State : Node
{
	[Signal]
	public delegate void EnemyStateChangedEventHandler(string newStateName);
	[Export]
	protected Enemy enemy;

	public virtual void Update(double delta)
	{
	}

	public virtual void PhysicsUpdate(double delta)
	{
	}

	public virtual void Enter()
	{
	}

	public virtual void Exit()
	{
	}
}
