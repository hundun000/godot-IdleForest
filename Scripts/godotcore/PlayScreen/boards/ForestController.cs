using Godot;
using GodotIdleForest.Scripts.godotcore;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards.subs;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ForestController : BaseDetailBoardController
{

    SubWorkInfoBoard subWorkInfoBoard;
    SubProficiencyBoard subProficiencyBoard;
    SubDestoryBoard subDestoryBoard;


    public override void _Ready()
    {
        base._Ready();
        this.subWorkInfoBoard = GodotUtils.FindFirstChildOfType<SubWorkInfoBoard>(this);
        this.subProficiencyBoard = GodotUtils.FindFirstChildOfType<SubProficiencyBoard>(this);
        this.subDestoryBoard = GodotUtils.FindFirstChildOfType<SubDestoryBoard>(this);
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
    }

    public override void onLogicFrame()
    {
        subWorkInfoBoard.onLogicFrame();
		subProficiencyBoard.onLogicFrame();
        subDestoryBoard.onLogicFrame();
	}
}
