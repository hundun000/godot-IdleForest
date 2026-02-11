using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using hundun.idleshare.gamelib;
using System;
using System.Linq;

public partial class SubBuyBoard : PanelContainer
{
    BaseDetailBoardController baseDetailBoard;
    AbstractConstructionPrototype prototype;
    [Export]
    Label _costCoinValue;
    [Export]
    Label _costWoodValue;
    [Export]
    Label _buyName;
    [Export]
    Button _buyButton;

    public override void _Ready()
    {
        _buyButton.Pressed += OnButtonPressed;
    }

    public void SubBoardUpdate()
    {
        var position = baseDetailBoard.model.saveData.position;
        _buyButton.Disabled = !baseDetailBoard.parent.game.idleGameplayExport.gameplayContext.constructionManager.canBuyInstanceOfPrototype(prototype.prototypeId, position);
    }

    internal void AfterSetModel(BaseDetailBoardController baseDetailBoard, AbstractConstructionPrototype prototype)
    {
        this.baseDetailBoard = baseDetailBoard;
        this.prototype = prototype;

        _buyName.Text = baseDetailBoard.parent.game.idleGameplayExport.gameplayContext.gameDictionary.constructionPrototypeIdToShowName(prototype.language, prototype.prototypeId);

        ResourcePair cost;
        cost = prototype.buyInstanceCostPack.modifiedValues.Where(it => it.type.Equals(ResourceType.COIN)).FirstOrDefault();
        if (cost != null)
        {
            _costCoinValue.Text = cost.amount.ToString();
        }
        cost = prototype.buyInstanceCostPack.modifiedValues.Where(it => it.type.Equals(ResourceType.WOOD)).FirstOrDefault();
        if (cost != null)
        {
            _costWoodValue.Text = cost.amount.ToString();
        }


    }

    private void OnButtonPressed()
    {
        var position = baseDetailBoard.model.saveData.position;
        baseDetailBoard.parent.game.idleGameplayExport.gameplayContext.constructionManager
            .buyInstanceOfPrototype(prototype.prototypeId, position);
    }
}
