using Godot;
using System;
using System.Diagnostics;

public partial class player : Area2D
{
    //初始化////////////////////////////////////////////////////////////////////////////////
    //移动速度（初始值为零向量）
    private Vector2 Velocity = Vector2.Zero;
    //移动速率（可以在Godot检查器界面修改）
    [Export]
    public int Speed { get; set; }
    //攻击模式（普通，散射，激光，跟踪，弹幕，集中，锁定，强化，炸药包）
    private enum AttackModeList { NORMAL, SPREAD, LASER, HOMING, BARRAGE, FOCUSED, LOCKDOWN, POWERUP, SATCHEL };
    [Export]
    public int AttackMode { get; set; }
    //屏幕大小
    public Vector2 ScreenSize;
    public override void _Ready()
    {
        //获取屏幕大小
        ScreenSize = GetViewportRect().Size;
        Start();
    }
    //处理开始新游戏（玩家的初始状态）
    public void Start()
    {
        //设置玩家起始位置
        Vector2 initPosition; initPosition.X = 420; initPosition.Y = 540;
        Position = initPosition;

        //设置玩家起始朝向
        Rotation = 0.0f;

        //设置控制子弹频率的计时器
        GetNode<Timer>("ShootTimer").Start();

        //设置攻击模式...等等，攻击模式交给main处理了？！我觉得还是让player控制比较好
        AttackMode = (int)AttackModeList.NORMAL;
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
            EmitSignal(SignalName.Shoot, _bullet, Rotation, Position, AttackMode);//发送玩家角色朝向和位置信息
            shootTimerTimeout = false;
        }
    }
    
    //处理射击信号的发射//////////////////////////////////////////////////////////////////////////
    [Signal]
    public delegate void ShootEventHandler(PackedScene bullet, float direction, Vector2 location, int attackmode);//自定义信号：shoot()
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

    //处理角色死亡
    //public void 
}
