using Godot;
using System;

public partial class main : Node2D
{
    //用于实例化子弹
    private void _spawnBullet(PackedScene bullet, float direction, Vector2 location, Vector2 velocity)
    {
        var spawnedBullet = bullet.Instantiate<bullet>();
        AddChild(spawnedBullet);
        spawnedBullet.Rotation = direction; 
        spawnedBullet.Position = location;
        spawnedBullet.Velocity = velocity;       
    }
    
    //攻击模式（普通，散射，激光，跟踪，弹幕，集中，锁定，强化，炸药包）
    public enum AttackMode { NORMAL, SPREAD, LASER, HOMING, BARRAGE, FOCUSED, LOCKDOWN, POWERUP, SATCHEL };
    
    //处理玩家发出的Shoot信号的接收
    private void OnPlayerShoot(PackedScene bullet, float direction, Vector2 location)
    {        
        //处理子弹速度
        int bulletSpeed = 800;
        var velocity = bulletSpeed * Vector2.Up;
        
        //处理子弹偏移
        Vector2 position = Position;
        float rotation = Rotation;
        int positionOffsetY/*, positionOffsetX, rotationOffset*/;

        //设置攻击模式
        int attackmode = (int)AttackMode.NORMAL;
        
        switch (attackmode)
        {
            case (int)AttackMode.NORMAL:
                positionOffsetY = -25;
                position.Y += positionOffsetY;
                _spawnBullet(bullet, direction, location, velocity);
                break;
            case (int)AttackMode.SPREAD:
                break;
        }
    }
}
