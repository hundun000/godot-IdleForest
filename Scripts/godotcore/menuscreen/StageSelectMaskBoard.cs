using Godot;
using GodotIdleForest.Scripts.godotcore;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public partial class StageSelectMaskBoard : PopupPanel
{
    DemoMenuScreen parent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_Ready开始");

        parent = GodotUtils.FindParentOfType<DemoMenuScreen>(this);

        var texts = parent.game.idleGameplayExport.gameDictionary.getStageSelectMaskBoardTexts(parent.game.idleGameplayExport.language);
        List<TextureButton> stageButtons = GodotUtils.FindAllChildrenOfType<TextureButton>(this).Where(it => it.Name.Equals("stageButton")).ToList();
        List<TextureLabel> textureLabels = GodotUtils.FindAllChildrenOfType<TextureLabel>(this).Where(it => it.Name.Equals("stageName")).ToList();

        
        for (int i = 0; i < stageButtons.Count; i++)
        {
            textureLabels[i].TextLabel.Text = texts[i + 1];
            stageButtons[i].Pressed += () => {
                GD.Print($"stageButtons{i} Pressed");
                parent.game.saveHandler.gameplayLoadOrStarter(0);
                GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToPacked, GameContainer.SceneManager.DemoPlayScreen);
                //GetTree().ChangeSceneToPacked(GameContainer.SceneManager.DemoPlayScreen);
            };
        }
        
    }



}
