using Godot;
using System;

/*
* 用来处理一个关卡（stage）的类
* 设计了所有敌人的行动脚本
*/
public partial class stage_1 : Node2D
{
    /*************************************************************************************/
    //DEBUG
    //
    //测试生成敌机的脚本
    //不考虑生成后的敌机状态，只考虑生成敌机的位置
    /*************************************************************************************/

    public override void _Ready()
    {
        //初始化生成计时器
        GetNode<Timer>("SpawnTimer").Start();
    }
    
    //每隔一个时刻执行一次生成行为
    private void OnSpawnTimerTimeout()
    {
        _spawnEnemy(_enemy, 400.0f, 0.0f, 100);
    }

    //用于实例化敌机
    private PackedScene _enemy = GD.Load<PackedScene>("res://enemy.tscn");
    private void _spawnEnemy(PackedScene enemy, float locationX, float locationY, int speed)
    {
        var spawnedEnemy = enemy.Instantiate<enemy>();
        Vector2 location; location.X = locationX; location.Y = locationY;
        AddChild(spawnedEnemy);
        spawnedEnemy.Position = location;//设置敌机生成位置
        spawnedEnemy.Speed = speed;//设置敌机生成速率
    }


}