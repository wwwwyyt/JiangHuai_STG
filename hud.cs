using Godot;
using System;

public partial class hud : CanvasLayer
{
    [Signal]
    public delegate void StartGameEventHandler();

    public override void _Ready()
    {
        GetNode<Label>("ScoreLabel").Hide();
    }
    public void ShowMessage(string text)
    {
        var message = GetNode<Label>("Message");
        message.Text = text;
        message.Show();

        GetNode<Timer>("MessageTimer").Start();
    }

    public void UpdateScore(int score)
    {
        GetNode<Label>("ScoreLabel").Text = score.ToString();
    }

    private void OnStartButtonPressed()
    {
        GetNode<Label>("Message").Hide();
        GetNode<Button>("StartButton").Hide();
        GetNode<Label>("ScoreLabel").Show();
        EmitSignal(SignalName.StartGame);
    }
}
