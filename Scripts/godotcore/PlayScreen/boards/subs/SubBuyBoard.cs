using Assets.Scripts.DemoGameCore.logic;
using Godot;
using Godot.Collections;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SubBuyBoard : PanelContainer
{
    BaseDetailBoardController baseDetailBoard;
    AbstractConstructionPrototype prototype;
    [Export]
    Array<ResourcePairVM> costPairVMs;
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


        var resourceTypeList = new List<string>() { ResourceType.COIN, ResourceType.WOOD };

        for (int i = 0; i < resourceTypeList.Count; i++)
        {
            ResourcePair cost = prototype.buyInstanceCostPack.modifiedValues.Where(it => it.type.Equals(resourceTypeList[i])).FirstOrDefault();
            ResourcePairVM vm = costPairVMs[i];
            if (cost != null)
            {
                vm.Visible = true;
                vm.Value.Text = cost.amount.ToString();
            }
            else
            {
                vm.Visible = false;
            }
        }

    }

    private void OnButtonPressed()
    {
        var position = baseDetailBoard.model.saveData.position;
        baseDetailBoard.parent.game.idleGameplayExport.gameplayContext.constructionManager
            .buyInstanceOfPrototype(prototype.prototypeId, position);
    }
}
