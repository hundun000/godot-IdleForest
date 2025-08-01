using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts;
using GodotIdleForest.Scripts.godotcore;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using System;

public partial class DemoMenuScreen : GodotBaseHundunScreen
{
    [Export]
    public BaseButton ContinueGameButton { get; set; }

    [Export]
    public StageSelectMaskBoard StageSelectMaskBoard { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_Ready开始");
        base._Ready();

        if (!GameContainer.Game.saveHandler.hasContinuedGameplaySave())
        {
            ContinueGameButton.Visible = false;
        }
        else
        {
            ContinueGameButton.Visible = true;
        }



    }

}
