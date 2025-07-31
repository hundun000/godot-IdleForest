using Godot;
using GodotIdleForest.Scripts.godotcore;
using System;

public partial class StageSelectMaskBoard : PopupPanel
{
    DemoMenuScreen parent;

    [Export]
    TextureButton backTextButton;
    [Export]
    TextureButton stage1TextButton;
    [Export]
    TextureButton stage2TextButton;
    [Export]
    TextureButton stage3TextButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        parent = GodotUtils.FindParentOfType<DemoMenuScreen>(this);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        var texts = parent.game.idleGameplayExport.gameDictionary.getStageSelectMaskBoardTexts(parent.game.idleGameplayExport.language);


        
    }
}
