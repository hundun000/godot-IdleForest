using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts.godotcore;
using GodotIdleForest.Scripts.godotcore.PlayScreen.boards;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;

public partial class BoardManager : Node
{
    public Dictionary<String, BaseDetailBoardController> cellDetailBoards = new();
    private BaseDetailBoardController CurrentController;

    public float Duration = 0.5f; // 移动所需时间
    public Tween.TransitionType TransitionType = Tween.TransitionType.Linear; // 缓动类型
    public Tween.EaseType EaseType = Tween.EaseType.InOut; // 缓动模式

    private Tween _currentTween; // 存储当前正在运行的Tween实例

    DemoPlayScreen parent;
    public ForestController forestController;
    public FactoryController factoryController;
    public DirtController dirtController;
    public RubbishController rubbishController;
    public LakeController lakeController;
    public DesertController desertController;


    public override void _EnterTree()
    {
        parent = GodotUtils.FindParentOfType<DemoPlayScreen>(this);
        forestController = GodotUtils.FindFirstChildOfType<ForestController>(this);
        factoryController = GodotUtils.FindFirstChildOfType<FactoryController>(this);
        dirtController = GodotUtils.FindFirstChildOfType<DirtController>(this);
        rubbishController = GodotUtils.FindFirstChildOfType<RubbishController>(this);
        lakeController = GodotUtils.FindFirstChildOfType<LakeController>(this);
        desertController = GodotUtils.FindFirstChildOfType<DesertController>(this);

        cellDetailBoards.Add(ConstructionPrototypeId.SMALL_TREE, forestController);
        cellDetailBoards.Add(ConstructionPrototypeId.SMALL_FACTORY, factoryController);
        cellDetailBoards.Add(ConstructionPrototypeId.MID_FACTORY, factoryController);
        cellDetailBoards.Add(ConstructionPrototypeId.BIG_FACTORY, factoryController);
        cellDetailBoards.Add(ConstructionPrototypeId.DIRT, dirtController);
        cellDetailBoards.Add(ConstructionPrototypeId.RUBBISH, rubbishController);
        cellDetailBoards.Add(ConstructionPrototypeId.LAKE, lakeController);
        cellDetailBoards.Add(ConstructionPrototypeId.DESERT, desertController);
    }

    /// <summary>
    /// 根据点击的格位种类，打印详情面板，传入true则仅更新面板数据，不打印新面板
    /// </summary>
    public void CallBoard(BaseConstruction construction)
    {

        this.CurrentController = cellDetailBoards[construction.prototypeId];

        // 当前面板更换
        foreach (var item in cellDetailBoards.Values)
        {
            item.Visible = (false);
        }

        CurrentController.setModel(construction);
        //CurrentController.BoardUpdate();

        // 显示新面板
        boardAppearMove();
    }



    private void boardAppearMove()
    {
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        Vector2 screenCenter = screenSize / 2f;
        Vector2 boardSize = CurrentController.GetRect().Size;
        Vector2 boardBirthPos = new Vector2((screenSize.X - boardSize.X)/ 2f, screenSize.Y);
        Vector2 boardPrintPos = new Vector2((screenSize.X - boardSize.X) / 2f, screenSize.Y - boardSize.Y - 10);


        // **重要：在开始新动画前，先取消（杀死）任何正在运行的旧动画**
        CancelMovement();

        CurrentController.Visible = (true);
        // 1. 动画开始前，立即将目标 Control 实例的位置设置为 ControlBirthPosition
        // 注意：这里需要根据 TargetPropertyName 来设置
        CurrentController.GlobalPosition = boardBirthPos;

        // 创建一个Tween实例
        _currentTween = GetTree().CreateTween();
        // 或者使用CreateTween()，它会在当前节点上创建一个Tween并返回
        // Tween tween = CreateTween();

        // 配置Tween
        _currentTween.SetTrans(TransitionType); // 设置缓动类型 (例如: Tween.TransitionType.Linear, Tween.TransitionType.Quad)
        _currentTween.SetEase(EaseType);       // 设置缓动模式 (例如: Tween.EaseType.InOut, Tween.EaseType.Out)

        // 添加一个属性动画 (从当前位置到目标位置)
        // tween.TweenProperty(object, property_path, final_value, duration)
        _currentTween.TweenProperty(CurrentController, "position", boardPrintPos, Duration);

        // 可以链式调用，例如在移动完成后缩放
        // tween.TweenProperty(this, "scale", Vector3.One * 2, 0.5f);

        // 连接Tween完成信号
        _currentTween.Finished += OnTweenFinished; // Godot 4 的事件订阅语法
        // Godot 3.x 语法: tween.Connect("finished", new Callable(this, nameof(OnTweenFinished)));

    }

    // 新增：取消动画的方法
    public void CancelMovement()
    {
        if (_currentTween != null && _currentTween.IsValid()) // 检查Tween是否存在且有效
        {
            _currentTween.Kill(); // 立即停止并移除Tween
            _currentTween = null; // 清除引用
            GD.Print("Movement cancelled!");
        }
    }

    private void OnTweenFinished()
    {
        GD.Print($"Tween movement finished!");
    }

    public void CloseBoard()
    {
        if (CurrentController != null)
        {
            CurrentController.Visible = false;
            CurrentController = null;
        }
    }
}
