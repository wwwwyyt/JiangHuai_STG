using Godot;
using System;

public partial class bullet : Area2D
{
    //处理子弹飞行的实例
    [Export]
    public int Speed { get; set; }
    public Vector2 Velocity { get; set; }

    public override void _Ready()
    {
        Velocity = Vector2.Right * Speed;
    }
    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity * (float)delta;
    }
}
