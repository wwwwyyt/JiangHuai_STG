using Godot;
using System;

public partial class enemy : Area2D
{
    //处理敌机的移动路径
    // [Signal]
    // public delegate void MoveEventHandler();
    // private PackedScene _bullet = GD.Load<PackedScene>("res://bullet.tscn");
    
    //设置敌机的速度（使用父节点给出信号的方式？）

    //控制敌机的运动：
    //1.可以从stage场景那里接收移动的指令和参数
    //2.将自身的移动参数进行更新

    //3.执行移动
    
    public Vector2 Velocity = Vector2.Down;
    public int Speed = 0;
    public override void _Process(double delta)
    {
        Position += Velocity * (float)delta * Speed;/////////////////////////////////////测试匀速直线运动（这种移动方式似乎会有卡顿）
    }

    [Signal]
    public delegate void UpdateEventHandler(int deltaX, int deltaY, int speed);

    // private void OnUpdate(int deltaX, int deltaY)
    // {
    //     Velocity.X += deltaX;
    //     Velocity.Y += deltaY;
    // }

    //处理敌机被子弹击中的情况
    private void OnAreaEntered(Area2D area)
    {
        // GD.Print("OnPalyer_BulletAreaEntered");
        Hide();
    }

    //飞出屏幕范围时清除对象
    private void OnVisibleOnScreenNotifier2dScreenExited()
    {
        QueueFree();
    }
}
