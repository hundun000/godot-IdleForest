using Godot;

public partial class ButtonNewGame : TextureButton
{
    public override void _Ready()
    {
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        GameContainer.Instance.saveHandler.gameSaveCurrent();
        GD.Print("按钮被点击了！gameSaveCurrent...");
    }
    public override void _Process(double delta)
    {
    }
}
