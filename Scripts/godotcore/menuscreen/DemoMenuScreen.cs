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

    public override void _EnterTree()
    {
        GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_EnterTree 开始");
        base._EnterTree();

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
