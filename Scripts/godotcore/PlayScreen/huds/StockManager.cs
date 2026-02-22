using Assets.Scripts.DemoGameCore;
using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts;
using GodotIdleForest.Scripts.godotcore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Color = Godot.Color;

public partial class StockManager : Node, IOneFrameResourceChangeListener
{
    [Export]
    public Label moneyText;
    [Export]
    public Label woodText;
    [Export]
    public Label moneyChangeText;
    [Export]
    public Label woodChangeText;

    private List<Label> moneyTexts;
    private List<Label> woodTexts;

    private Color addColor = Colors.Red;
    private Color minusColor = Colors.Green;

    Dictionary<string, List<Label>> nodeMap;

    DemoPlayScreen parent;

    public override void _EnterTree()
    {
        this.parent = GodotUtils.FindParentOfType<DemoPlayScreen>(this); ;
        moneyTexts = new List<Label>(2);
        woodTexts = new List<Label>(2);
        moneyTexts.Add(moneyText);
        moneyTexts.Add(moneyChangeText);
        woodTexts.Add(woodText);
        woodTexts.Add(woodChangeText);
        nodeMap = new() {
            { ResourceType.COIN, moneyTexts },
            { ResourceType.WOOD, woodTexts }
        };
    }

    public void updateStock(Dictionary<string, long> changeMap, Dictionary<string, List<long>> deltaHistoryMap)
    {
        foreach (var node in nodeMap)
        {
            long historySum;
            if (deltaHistoryMap.ContainsKey(node.Key))
            {
                historySum = deltaHistoryMap.get(node.Key).TakeLast(DemoIdleGame.LOGIC_FRAME_PER_SECOND).Sum();
            }
            else
            {
                historySum = 0;
            }

            nodeUpdate(node.Value[0], node.Value[1], historySum, parent.game.idleGameplayExport.gameplayContext.storageManager.getResourceNumOrZero(node.Key));
        }
    }

    private void nodeUpdate(Label thiz, Label sub, long delta, long amout)
    {

        var amountLabelText = (
                amout + ""
                );
        var deltaLabelText = "";
        bool isAdding = true;
        if (delta > 0)
        {
            deltaLabelText = "(+" + delta + ")";
            isAdding = true;
        }
        else if (delta == 0)
        {
            deltaLabelText = "";
        }
        else
        {
            deltaLabelText = "(-" + Math.Abs(delta) + ")";
            isAdding = false;
        }

        thiz.Text = amountLabelText;
        sub.AddThemeColorOverride("font_color", isAdding ? addColor : minusColor);

        sub.Text = deltaLabelText;
    }

    void IOneFrameResourceChangeListener.onResourceChange(Dictionary<string, long> changeMap, Dictionary<string, List<long>> deltaHistoryMap)
    {
        updateStock(changeMap, deltaHistoryMap);
    }
}
