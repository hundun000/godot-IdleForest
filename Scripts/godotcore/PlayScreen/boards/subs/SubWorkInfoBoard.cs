using Assets.Scripts.DemoGameCore.logic;
using Godot;
using Godot.Collections;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Godot.VisualShaderNode;

namespace GodotIdleForest.Scripts.godotcore.PlayScreen.boards.subs
{
    public partial class SubWorkInfoBoard : Control, ILogicFrameListener
    {

        BaseConstruction model;

        [Export]
        ResourceLineVM costLineVM;
        [Export]
        ResourceLineVM gainLineVM;

        bool hasCost;
        bool hasGain;


        internal void AfterSetModel(BaseDetailBoardController baseDetailBoard)
        {
            this.model = baseDetailBoard.model;

            hasCost = model.outputComponent.outputCostPack != null;
            hasGain = model.outputComponent.outputGainPack != null;
            costLineVM.Visible = hasCost;
            gainLineVM.Visible = hasGain;
            if (hasCost)
            {
                costLineVM.AfterSetModel(model.outputComponent.outputCostPack.descriptionStart);
            }
            if (hasGain)
            {
                gainLineVM.AfterSetModel(model.outputComponent.outputGainPack.descriptionStart);
            }
        }

        public void onLogicFrame()
        {
            if (hasCost)
            {
                costLineVM.fillOneLine(model.outputComponent.outputCostPack.modifiedValues);
            }
            if (hasGain)
            {
                gainLineVM.fillOneLine(model.outputComponent.outputGainPack.modifiedValues);
            }
        }

    }
}
