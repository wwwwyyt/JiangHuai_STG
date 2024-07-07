using Godot;
using System;

/*
* 用于处理主菜单
* 界面显示（TitleCanvas）
* 按钮（StartButton）
*/
public partial class Menu : Node2D
{

    public void OnStartButtonPressed()
    {
        GetTree().ChangeSceneToFile("res://stage_1.tscn");
    }
}
