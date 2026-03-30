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
    public partial class SubProficiencyBoard : Control, ILogicFrameListener
    {
        BaseConstruction model;

        [Export]
        Slider proficiencySlider;
        [Export]
        Label proficiencyValue;

        internal void AfterSetModel(BaseDetailBoardController baseDetailBoard)
        {
            this.model = baseDetailBoard.model;

        }

        public void onLogicFrame()
        {
            proficiencySlider.Value = model.saveData.proficiency;
            proficiencyValue.Text = model.saveData.proficiency + "%";
        }
    }
}
