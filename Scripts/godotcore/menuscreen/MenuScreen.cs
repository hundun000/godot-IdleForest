using Godot;
using System;

public partial class MenuScreen : Node
{
    [Export] // 可以在编辑器中拖拽赋值
    public BaseButton ContinueGameButton { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        if (!GameContainer.Instance.saveHandler.hasContinuedGameplaySave())
        {
            ContinueGameButton.Visible = false;
        }
        else
        {
            ContinueGameButton.Visible = true;
        }

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
