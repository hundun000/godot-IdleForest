using Assets.Scripts.DemoGameCore.logic;
using Godot;
using Godot.Collections;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ResourceLineVM : Control
{
    [Export]
    Label descriptionLabel;
    [Export]
    Array<ResourcePairVM> pairVMs;

    public void AfterSetModel(String description)
    { 
        descriptionLabel.Text = description;
    }
    public void fillOneLine(List<ResourcePair> modifiedValues)
    {
        var resourceTypeList = ResourceType.VALUES_FOR_SHOW_ORDER;

        for (int i = 0; i < resourceTypeList.Count; i++)
        {
            ResourcePair pair = modifiedValues.Where(it => it.type.Equals(resourceTypeList[i])).FirstOrDefault();
            ResourcePairVM vm = pairVMs[i];
            if (pair != null)
            {
                vm.Visible = true;
                vm.Value.Text = pair.amount.ToString();
                vm.Icon.Texture = GameContainer.Instance.TextureLib.GetResourceIcon(resourceTypeList[i]);
            }
            else
            {
                vm.Visible = false;
            }
        }
    }
}
