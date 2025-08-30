using Godot;
using GodotIdleForest.Scripts.godotcore;

public partial class ButtonNewGame : TextureButton
{
    DemoMenuScreen parent;
    public override void _EnterTree()
	{
        parent = GodotUtils.FindParentOfType<DemoMenuScreen>(this);

        Pressed += OnButtonPressed;
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
