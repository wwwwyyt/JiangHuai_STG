using Godot;
using System;

public partial class bullet : Area2D
{
    //处理单个子弹飞行的实例
    // [Export]
    // public int Speed { get; set; }
    // public override void _Ready()
    // {
    //     Velocity = Vector2.Up;
    // }
    public Vector2 Velocity { get; set; } = Vector2.Up;

    //处理子弹的运动（匀速子弹）
    //这里的Position, Velocity都可以在外部修改
    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity * (float)delta;
    }

    //使超出屏幕的子弹销毁
    private void OnVisibleOnScreenNotifier2dScreenExited()
    {
        QueueFree();
    }
}
