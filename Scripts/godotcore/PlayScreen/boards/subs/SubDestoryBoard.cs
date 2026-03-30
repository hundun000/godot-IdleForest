using Assets.Scripts.DemoGameCore.logic;
using Godot;
using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotIdleForest.Scripts.godotcore.PlayScreen.boards.subs
{
    public partial class SubDestoryBoard : Control, ILogicFrameListener
    {
        BaseDetailBoardController baseDetailBoard;
        BaseConstruction model;

        [Export]
        Button button;
        [Export]
        ResourceLineVM gainLineVM;

        bool hasGain;

        public override void _Ready()
        {
            button.Pressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            model.existenceComponent.doDestory(ConstructionPrototypeId.DIRT);
        }

        internal void AfterSetModel(BaseDetailBoardController baseDetailBoard)
        {
            this.baseDetailBoard = baseDetailBoard;
            this.model = baseDetailBoard.model;

            hasGain = model.existenceComponent.destoryGainPack.baseValues.Count > 0;
            gainLineVM.Visible = hasGain;
            if (hasGain)
            {
                gainLineVM.AfterSetModel(model.existenceComponent.destoryGainPack.descriptionStart);
            }
        }

        public void onLogicFrame()
        {
            if (model.existenceComponent.canDestory())
            {
                button.Disabled = (false);
            }
            else
            {
                button.Disabled = (true);
            }
            if (hasGain)
            {
                gainLineVM.fillOneLine(model.outputComponent.outputGainPack.modifiedValues);
            }
        }
    }
}
