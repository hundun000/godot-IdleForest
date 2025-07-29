using Godot;

public partial class ButtonContinueGame : TextureButton
{
	public override void _Ready()
	{
		Pressed += OnButtonPressed;
	}

	private void OnButtonPressed()
	{
		GD.Print("按钮被点击了！退出游戏...");
		GetTree().Quit();
	}
	public override void _Process(double delta)
	{
	}
}
