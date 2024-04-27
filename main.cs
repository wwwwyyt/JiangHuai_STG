using Godot;
using System;

public partial class main : Node2D
{
    //处理玩家发出的Shoot信号的接收
    private void OnPlayerShoot(PackedScene bullet, float direction, Vector2 location)
    {
        var spawnedBullet = bullet.Instantiate<bullet>();
        
        //处理子弹速度
        int bulletSpeed = 800;
        var velocity = bulletSpeed * Vector2.Up;

        //处理子弹偏移
        Vector2 position = Position;
        float rotation = Rotation;
        int positionOffsetY, positionOffsetX, rotationOffset;

        positionOffsetY = -25;
        position.Y += positionOffsetY;

        AddChild(spawnedBullet);
        spawnedBullet.Rotation = direction; 
        spawnedBullet.Position = location;
        spawnedBullet.Velocity = velocity;
    }
}
