using Godot;
using System;

public partial class main : Node2D
{
    //处理玩家发出的Shoot信号的接收
    private void OnPlayerShoot(PackedScene bullet, float direction, Vector2 location)
    {
        var spawnedBullet = bullet.Instantiate<bullet>();
        AddChild(spawnedBullet);
        spawnedBullet.Rotation = direction; 
        spawnedBullet.Position = location; 
        spawnedBullet.Velocity = spawnedBullet.Velocity.Rotated(direction);
    }
}
