using Godot;
using GodotIdleForest.Scripts.godotcore;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards.subs;
using System;
using static Godot.VisualShaderNode;

public partial class FactoryController : BaseDetailBoardController
{
    SubWorkInfoBoard subWorkInfoBoard;
    SubProficiencyBoard subProficiencyBoard;
    SubDestoryBoard subDestoryBoard;
    ConstructionIconVM constructionIconVM;

    public override void _Ready()
    {
        base._Ready();
        this.subWorkInfoBoard = GodotUtils.FindFirstChildOfType<SubWorkInfoBoard>(this);
        this.subProficiencyBoard = GodotUtils.FindFirstChildOfType<SubProficiencyBoard>(this);
        this.subDestoryBoard = GodotUtils.FindFirstChildOfType<SubDestoryBoard>(this);
        this.constructionIconVM = GodotUtils.FindFirstChildOfType<ConstructionIconVM>(this);
    }

    public override void BoardUpdate()
    {
        onLogicFrame();
    }

    public override void AfterSetModel()
    {
        base.AfterSetModel();
        subWorkInfoBoard.AfterSetModel(this);
        subProficiencyBoard.AfterSetModel(this);
        subDestoryBoard.AfterSetModel(this);
        constructionIconVM.AfterSetModel(
            parent.game,
            parent.game.idleGameplayExport.gameplayContext.constructionFactory.getPrototype(model.prototypeId)
            );
    }

    public override void onLogicFrame()
    {
        subWorkInfoBoard.onLogicFrame();
        subProficiencyBoard.onLogicFrame();
        subDestoryBoard.onLogicFrame();
    }
}
