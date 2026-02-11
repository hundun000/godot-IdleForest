using Godot;
using GodotIdleForest.Scripts.godotcore;

public partial class ButtonContinueGame : TextureButton
{
    DemoMenuScreen parent;
    public override void _Ready()
	{
		Pressed += OnButtonPressed;

        parent = GodotUtils.FindParentOfType<DemoMenuScreen>(this);
    }

	private void OnButtonPressed()
	{
		GD.Print("按钮被点击了！读取游戏...");
        parent.game.saveHandler.gameplayLoadOrStarter(-1);
        GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToPacked, GameContainer.SceneManager.DemoPlayScreen);

    }
    public override void _Process(double delta)
	{
	}
}
