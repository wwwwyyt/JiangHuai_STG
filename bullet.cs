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
    //速度和方向在一开始就设定好了
    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity * (float)delta;
    }

    //使超出屏幕的子弹销毁
    private void OnVisibleOnScreenNotifier2dScreenExited()
    {
        QueueFree();
    }

    //处理子弹击中敌人
    private void OnEnemyAreaEntered(Area2D area)
    {
        GD.Print("OnEnemyAreaEntered");
        QueueFree();//销毁自身
    }
}
