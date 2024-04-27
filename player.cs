using Godot;
using System;
using System.Diagnostics;

public partial class player : Area2D
{
    //移动速度（初始值为零向量）
    private Vector2 Velocity = Vector2.Zero;
    
    //移动速率（可以在Godot检查器界面修改）
    [Export]
    public int Speed { get; set; }

    //攻击模式（普通，散射，激光，跟踪，弹幕，集中，锁定，强化，炸药包）
    public enum AttackMode { NORMAL, SPREAD, LASER, HOMING, BARRAGE, FOCUSED, LOCKDOWN, POWERUP, SATCHEL };
    public Vector2 ScreenSize;
    //初始化////////////////////////////////////////////////////////////////////////////////
    public override void _Ready()
    {
        //获取屏幕大小
        ScreenSize = GetViewportRect().Size;

        //设置控制子弹频率的计时器
        GetNode<Timer>("ShootTimer").Start();

        //设置攻击模式
    }
    
    public void GetInput()//处理输入的函数
    {
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }
    public override void _PhysicsProcess(double delta)
    {
    //处理输入和运动////////////////////////////////////////////////////////////////////////////
        GetInput();
        Position += Velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );

    //处理角色动画/////////////////////////////////////////////////////////////////////////////
        var animatedSprite2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        
        if (Velocity.Length() > 0)
        {
            animatedSprite2d.Play();
        }
        else
        {
            animatedSprite2d.Stop();
        }
        if (Velocity.X != 0)
        {
            animatedSprite2d.Animation = "fly";
            animatedSprite2d.FlipH = Velocity.X > 0;
        }
        if (Input.IsActionJustReleased("ui_left") || Input.IsActionJustReleased("ui_right"))
        {
            animatedSprite2d.Animation = "default";
        }
    
    //处理攻击信号/////////////////////////////////////////////////////////////////////////////
        if (Input.IsActionPressed("attack") && shootTimerTimeout == true)
        {
            Vector2 position = Position;
            float rotation = Rotation;
            int positionOffsetY, positionOffsetX, rotationOffset;
            int attackmode = (int)AttackMode.NORMAL;
            
            switch (attackmode)
            {
                case (int)AttackMode.NORMAL:
                    positionOffsetY = -25;
                    position.Y += positionOffsetY;
                    break;
                case (int)AttackMode.SPREAD:
                    break;
            }
            EmitSignal(SignalName.Shoot, _bullet, Rotation, Position);///////////////////////////////////////////////////
            shootTimerTimeout = false;
        }
    }
    
    //处理射击信号的发射//////////////////////////////////////////////////////////////////////////
    [Signal]
    public delegate void ShootEventHandler(PackedScene bullet, float direction, Vector2 location);//自定义信号：shoot()
    private PackedScene _bullet = GD.Load<PackedScene>("res://bullet.tscn");

    /*
    public override void _Input(InputEvent @event)
    {
        //if (@event.IsActionPressed("attack"))
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left&& mouseButton.Pressed)
            {
                EmitSignal(SignalName.Shoot, _bullet, Rotation, Position);
                GD.Print("Shoot");
            }
        }
    }
    */

    //控制射击频率
    private bool shootTimerTimeout = false;//射击时间到的标志
    public void OnShootTimerTimeout()//使用了一个计时器来控制射击频率
    {
        shootTimerTimeout = true;
    }
    public override void _Process(double delta)
    {

    }
}
