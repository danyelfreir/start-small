using Godot;
using System;

public partial class Strength : Node
{
    private float value;

    public override void _Ready()
    {
        this.value = 1.0f;
    }

    public float GetValue()
    {
        return this.value;
    }

    public void LevelUp(float amount)
    {
        this.value += amount / 10.0f;
    }

    public int Compare(Strength other)
    {
        if (other == null) return 1;
        if (this.value > other.value) return 1;
        if (this.value < other.value) return -1;
        return 0;
    }
}
