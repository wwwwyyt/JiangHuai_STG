using Godot;
using System;
using System.Diagnostics;

public partial class main : Node2D
{
    //用于实例化子弹
    private void _spawnBullet(PackedScene bullet, float direction, float locationX, float locationY, Vector2 velocity)
    {
        var spawnedBullet = bullet.Instantiate<bullet>();
        Vector2 location; location.X = locationX; location.Y = locationY;
        AddChild(spawnedBullet);
        spawnedBullet.Position = location;//设置子弹生成位置
        // spawnedBullet.Velocity = velocity;//设置子弹速度
        spawnedBullet.Velocity = spawnedBullet.Velocity.Rotated(direction) * 800;//设置子弹速度////////////////////////////////////
    }
    
    //攻击模式（普通，散射，激光，跟踪，弹幕，集中，锁定，强化，炸药包）
    public enum AttackMode { NORMAL, SPREAD, LASER, HOMING, BARRAGE, FOCUSED, LOCKDOWN, POWERUP, SATCHEL };
    
    //处理玩家发出的Shoot信号的接收
    private void OnPlayerShoot(PackedScene bullet, float direction, Vector2 location)
    {        
        //处理子弹速度
        int bulletSpeed = 800;
        var velocity = bulletSpeed * Vector2.Up;///////////////////////////////////////////////////
        
        //处理子弹偏移
        Vector2 position = location;
        float rotation = direction, rotationOffset;
        int positionOffsetY, positionOffsetX;

        //设置攻击模式
        int attackmode = (int)AttackMode.SPREAD;
        
        switch (attackmode)
        {
            //普通模式
            case (int)AttackMode.NORMAL:
                positionOffsetY = -25;
                _spawnBullet(bullet, rotation, position.X, position.Y+positionOffsetY, velocity);
                break;
            //散射模式
            case (int)AttackMode.SPREAD:
                positionOffsetY = -25;
                positionOffsetX = 30;
                rotationOffset = Mathf.Pi / 6;

                _spawnBullet(bullet, rotation, position.X, position.Y+positionOffsetY, velocity);
                _spawnBullet(bullet, rotation + rotationOffset, position.X+positionOffsetX, position.Y-10, velocity);
                _spawnBullet(bullet, rotation - rotationOffset, position.X-positionOffsetX, position.Y-10, velocity);
                break;
            //强化模式
            case (int)AttackMode.POWERUP:
                positionOffsetY = -25;
                positionOffsetX = 30;

                _spawnBullet(bullet, rotation, position.X, position.Y+positionOffsetY, velocity);
                _spawnBullet(bullet, rotation, position.X+positionOffsetX, position.Y, velocity);
                _spawnBullet(bullet, rotation, position.X-positionOffsetX, position.Y, velocity);
                break;
        }
    }
}
