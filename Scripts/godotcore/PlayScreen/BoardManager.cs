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
    private BaseDetailBoardController DisapprearingController;

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
    /// 根据点击的格位种类，打印详情面板.
    /// onlyUpdateData: 传入true则仅更新面板数据，不进行面板出现、消失动画
    /// </summary>
    public void CallBoard(BaseConstruction construction)
    {
        if (CurrentController == null || CurrentController.model != construction)
        {
            boardAppear(construction);
            CurrentController.setModel(construction);
        }
    }

    // 显示面板
    private void boardAppear(BaseConstruction construction)
    {
        // 当前面板更换
        foreach (var item in cellDetailBoards.Values)
        {
            item.Visible = (false);
        }
        this.CurrentController = cellDetailBoards[construction.prototypeId];

        // 显示新面板
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        Vector2 boardSize = CurrentController.GetRect().Size;
        Vector2 birthPos = new Vector2((screenSize.X - boardSize.X) / 2f, screenSize.Y);

        CurrentController.Visible = (true);
        CurrentController.GlobalPosition = birthPos;

        StopCoroutineBoardAnyMove();
        boardAppearMove();
    }

    private void boardAppearMove()
    {
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        Vector2 screenCenter = screenSize / 2f;
        Vector2 boardSize = CurrentController.GetRect().Size;
        Vector2 boardPrintPos = new Vector2((screenSize.X - boardSize.X) / 2f, screenSize.Y - boardSize.Y - 10);

        // 创建并配置 Tween
        _currentTween = GetTree().CreateTween();
        _currentTween.SetTrans(TransitionType);
        _currentTween.SetEase(EaseType);

        // 执行位移
        _currentTween.TweenProperty(CurrentController, "position", boardPrintPos, Duration);

        // 连接信号
        _currentTween.Finished += OnTweenFinished;

    }

    private void OnTweenFinished()
    {
        GD.Print($"Tween movement finished!");
    }

    public void CloseBoard()
    {
        if (CurrentController != null)
        {
            StopCoroutineBoardAnyMove();
            DisapprearingController = CurrentController;
            this.CurrentController = null;
            boardDisappearMove();
        }
    }

    private void boardDisappearMove()
    {
        var target = DisapprearingController;

        // 2. 计算目标位置 (对应 Unity 的 boardBirthPos.position)
        Vector2 screenSize = GetViewport().GetVisibleRect().Size;
        Vector2 boardSize = target.GetRect().Size;
        Vector2 targetPos = new Vector2((screenSize.X - boardSize.X) / 2f, screenSize.Y);

        // 3. 创建 Tween (代替 while 循环)
        _currentTween = GetTree().CreateTween();

        // 如果想模仿 SmoothDamp 的平滑感，建议使用 TransQuint 或 TransExpo
        _currentTween.SetTrans(TransitionType);
        _currentTween.SetEase(EaseType);

        // 4. 执行位移 (对应 Vector3.SmoothDamp)
        _currentTween.TweenProperty(target, "global_position", targetPos, Duration);

        // 5. 连接完成回调 (对应循环结束后的 SetActive(false))
        _currentTween.Finished += () =>
        {
            target.Visible = false;
        };
    }

    /// <summary>
    /// 取代unity实现
    /// </summary>
    private void StopCoroutineBoardAnyMove()
    {
        if (_currentTween != null && _currentTween.IsValid()) // 检查Tween是否存在且有效
        {
            _currentTween.Kill(); // 立即停止并移除Tween
            _currentTween = null; // 清除引用
            GD.Print("Movement cancelled!");
        }
    }

    /// <summary>
    /// 收到constructions改变，board更新数据
    /// </summary>
    public void boardCheckRecall(List<BaseConstruction> constructions)
    {
        constructions.ForEach(_construction =>
        {
            if (CurrentController != null && CurrentController.model.saveData.position == _construction.saveData.position)
            {
                CallBoard(_construction);
            }
        });

            
    }
}
