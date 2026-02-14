using Assets.Scripts.DemoGameCore.logic;
using Godot;
using GodotIdleForest.Scripts.godotcore;
using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

public partial class StatusBarController : Node, ILogicFrameListener, IGameAreaChangeListener
{
    [Export]
    Label carbonValue;
    [Export]
    TextureRect[] carbonBars;
    [Export]
    TextureRect carbonStateIcons;
    [Export]
    Texture2D[] carbonStateIconsSprite;
    [Export]
    float[] carbonBarDividingLines; //分界线值

    private float[] barsWidth;

    DemoPlayScreen parent;      // 通过代码绑定
    BaseConstruction governmentConstruction; // 后端数据引用
    BaseConstruction organizationConstruction; // 后端数据引用

    float carbonBarMaxValue, carbonBarMinValue; //碳量最大最小值
    int carbonNowState; //记录现在状态 0：green 1：yellow 2：red

    public override void _EnterTree() 
    {
        parent = GodotUtils.FindParentOfType<DemoPlayScreen>(this);
        carbonBarInit();
    }

    //碳条初始化
    private void carbonBarInit()
    {
        int sizeLines = carbonBarDividingLines.Length;
        int sizeBar = sizeLines - 1;
        carbonNowState = 0;
        barsWidth = new float[sizeBar];
        for (int i = 0; i < sizeBar; i++)
        {
            carbonBars[i].Visible = (i == 0);
            barsWidth[i] = carbonBars[i].Size.X;
        }
        carbonBarMaxValue = carbonBarDividingLines[sizeLines - 1];
        carbonBarMinValue = carbonBarDividingLines[0];
    }

    void IGameAreaChangeListener.onGameAreaChange(string last, string current)
    {
        List<BaseConstruction> newConstructions = parent.game.idleGameplayExport.gameplayContext.constructionManager.getAreaControlableConstructionsOrEmpty(GameArea.AREA_SINGLE);
        this.governmentConstruction = newConstructions
            .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.GOVERNMENT))
            .First();
        this.organizationConstruction = newConstructions
            .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.ORGANIZATION))
            .First();
    }

    void ILogicFrameListener.onLogicFrame()
    {
        // 后端逻辑帧到达，说明后端数据可能有变，让this更新使用最新数据
        long amount = parent.game.idleGameplayExport.gameplayContext.storageManager.getResourceNumOrZero(ResourceType.CARBON);
        CarbonStage carbonStage = BaseIdleForestConstruction.carbonAmountToCarbonStage(amount);

        carbonValue.Text = amount.ToString();
        SetCarbonBarValue(amount);
        SetGovernmentBarValue(governmentConstruction.saveData.proficiency / 100.0f);
        SetEnvironmentalBarValue(organizationConstruction.saveData.proficiency / 100.0f);
    }

    //设置当前碳条
    public void SetCarbonBarValue(float value)
    {
        value = Mathf.Clamp(value, carbonBarMinValue, carbonBarMaxValue); //钳制值在合理范围内
        carbonBars[carbonNowState].Visible = (false);
        for (int i = 0; i < 3; i++)
        {
            if (value > carbonBarDividingLines[i] && value <= carbonBarDividingLines[i + 1])
            {
                // audioManager.MusicChange(i);
                carbonNowState = i;
                break;
            }
        }
        carbonBars[carbonNowState].Visible = (true);
        carbonStateIcons.Texture = carbonStateIconsSprite[carbonNowState];

        // 1. 不需fillAmount
        // 2. localPosition 的等价 (Godot 中 Control 的 Position 即为 localPosition)
        double rate = Mathf.Min(1.0 - value / carbonBarDividingLines[carbonNowState + 1], 0.8);
        carbonBars[carbonNowState].Position = new Vector2(
            (float)(-barsWidth[carbonNowState] * rate * carbonBars[carbonNowState].Scale.X), 
            carbonBars[carbonNowState].Position.Y
            );
    }

    //设置当前政府条
    public void SetGovernmentBarValue(float rate)
    {
        rate = Mathf.Clamp(rate, 0, 1); //钳制值在合理范围内
        // TODO GovernmentBar.transform.Find("Line").GetComponent<Image>().fillAmount = rate;
    }

    //设置当前环保条
    public void SetEnvironmentalBarValue(float rate)
    {
        rate = Mathf.Clamp(rate, 0, 1); //钳制值在合理范围内
        // TODO EnvironmentalBar.transform.Find("Line").GetComponent<Image>().fillAmount = rate;
    }
}
