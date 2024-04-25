using Godot;
using System;
using System.Diagnostics;

public partial class player : Area2D
{
    private Vector2 Velocity = Vector2.Zero;
    [Export]
    public int Speed { get; set; }
    public Vector2 ScreenSize;
    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }
    public void GetInput()
    {
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }
    public override void _PhysicsProcess(double delta)
    {
    //处理输入和运动
        GetInput();
        Position += Velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
            y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
        );
    
    
    //处理角色动画
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
    
    }
    
    //处理射击
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
                EmitSignal(SignalName.Shoot, _bullet, Rotation, Position);///////////////////////////////////////////////////
                GD.Print("Shoot");
            }
        }
    }
    */

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("attack"))
        {
            EmitSignal(SignalName.Shoot, _bullet, Rotation, Position);///////////////////////////////////////////////////
        }
    }
}


