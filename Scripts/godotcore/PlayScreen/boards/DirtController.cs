using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DirtController : BaseDetailBoardController
{
    [Export]
    SubBuyBoard subBuyForestBoard;
    [Export]
    SubBuyBoard subBuyFactoryBoard;

    public override void BoardUpdate()
    {
        subBuyForestBoard.SubBoardUpdate();
        subBuyFactoryBoard.SubBoardUpdate();
    }

    public override void AfterSetModel()
    {
        var constructionPrototypes = parent.game.idleGameplayExport.gameplayContext.constructionManager.getAreaShownConstructionPrototypesOrEmpty(GameArea.AREA_SINGLE);
        subBuyForestBoard.AfterSetModel(this, constructionPrototypes.Where(it => it.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE)).First());
        subBuyFactoryBoard.AfterSetModel(this, constructionPrototypes.Where(it => it.prototypeId.Equals(ConstructionPrototypeId.SMALL_FACTORY)).First());
    }
}
