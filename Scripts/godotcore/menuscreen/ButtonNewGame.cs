using Godot;
using GodotIdleForest.Scripts.godotcore;

public partial class ButtonNewGame : TextureButton
{
    DemoMenuScreen parent;
    public override void _Ready()
	{
		Pressed += OnButtonPressed;

        parent = GodotUtils.FindParentOfType<DemoMenuScreen>(this);
    }

	private void OnButtonPressed()
	{
        parent.StageSelectMaskBoard.Show();
		GD.Print("按钮被点击了！StageSelectMaskBoard.Show...");
	}
	public override void _Process(double delta)
	{
	}
}
