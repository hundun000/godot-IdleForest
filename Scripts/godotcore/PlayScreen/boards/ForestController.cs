using Godot;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ForestController : BaseDetailBoardController
{
    [Export]
    Label _inputValue;
    [Export]
    Label _outputValue;
    [Export]
    Button _cutBtn;
    [Export]
    Label _cutOutputValue;
    [Export]
    Slider _proficiencySlider;
    public override void BoardUpdate()
    {
        // ------ update text ------
        _inputValue.Text = model.outputComponent.outputCostPack.modifiedValues[0].amount.ToString();
        _outputValue.Text = model.outputComponent.outputCostPack.modifiedValues[0].amount.ToString();
        _cutOutputValue.Text = model.existenceComponent.destoryGainPack.modifiedValues[0].amount.ToString();

        // ------ update clickable-state ------
        if (model.existenceComponent.canDestory()) _cutBtn.Disabled = (false);
        else _cutBtn.Disabled = (true);

        // ------ update proficiency------
        _proficiencySlider.Value = model.saveData.proficiency;
    }
}
