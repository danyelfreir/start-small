using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export]
	public State InitialState;

	private State _currentState;
	private Dictionary<string, State> _states = new();

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			if (child is State state)
			{
                state.EnemyStateChanged += (string newStateName) => OnChildStateChanged(state, newStateName);
				_states[child.Name.ToString().ToLower()] = state;
			}
		}

		if (InitialState != null)
		{
			InitialState.Enter();
			_currentState = InitialState;
		}
	}

	public override void _Process(double delta)
	{
		_currentState?.Update(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState?.PhysicsUpdate(delta);
	}

	private void OnChildStateChanged(State state, string newStateName)
	{
		if (state != _currentState)
            return;

		if (_states.TryGetValue(newStateName.ToLower(), out State newState))
		{
			_currentState?.Exit();
			newState.Enter();
			_currentState = newState;
		}
	}
}
