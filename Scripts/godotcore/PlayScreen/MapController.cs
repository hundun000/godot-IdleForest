using Assets.Scripts.DemoGameCore.logic;
using Godot;
using Godot.Collections;
using GodotIdleForest.Scripts.godotcore;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace GodotIdleForest.Scripts.godotcore 
{
    public class BackendLevelInfo
    {
        public GridPosition startCameraPos;              // 相机初始点位
        public Vector2 islandPos;                             // 相机初始位置相对于指定点位的位移，用于保证地图在岛上的位置
        public List<BaseConstruction> constructions;     // 棋盘信息

        internal static BackendLevelInfo from(List<BaseConstruction> constructions, string stageId)
        {
            var result = new BackendLevelInfo();
            result.constructions = constructions;
            switch (stageId)
            {
                default:
                case StageId.STAGE_1:
                    result.startCameraPos = new GridPosition(0, 0);
                    result.islandPos = new(0.3f, -1.4f);
                    break;
                case StageId.STAGE_2:
                    result.startCameraPos = new GridPosition(0, 0);
                    result.islandPos = new(0f, -0.85f);
                    break;
                case StageId.STAGE_3:
                    result.islandPos = new(0.0f, -1.4f);
                    result.startCameraPos = new GridPosition(0, 0);
                    break;
            }

            return result;
        }
    }

    public partial class CellExtraData
    {
        public MapController mapController; // 父UI引用
        public BaseConstruction construction; // 后端数据引用


        public Vector2I? GetSourceId()
        {
            switch(construction.prototypeId)
            {
                case ConstructionPrototypeId.DIRT:
                    return new Vector2I(0, 0);
                case ConstructionPrototypeId.DESERT:
                    return new Vector2I(2, 0);
                case ConstructionPrototypeId.RUBBISH:
                    return new Vector2I(1, 0);
                case ConstructionPrototypeId.LAKE:
                    return new Vector2I(3, 0);
                case ConstructionPrototypeId.SMALL_TREE:
                    return new Vector2I(0, 1);
                case ConstructionPrototypeId.SMALL_FACTORY:
                    return new Vector2I(0, 2);
                default:
                    return null;
            }
        }

        public void StateChangeTo(MapController mapController, BaseConstruction construction)
        {
            this.mapController = mapController;
            this.construction = construction;

            switch (construction.prototypeId)
            {
                case ConstructionPrototypeId.DIRT:
                    animatorPlay(0);
                    //upperRenderer.sprite = SpriteLoader.field[0];
                    break;
                case ConstructionPrototypeId.RUBBISH:
                    animatorPlay(0);
                    //upperRenderer.sprite = SpriteLoader.field[1];
                    break;
                case ConstructionPrototypeId.SMALL_FACTORY:
                    //upperRenderer.sprite = SpriteLoader.factory[0];
                    animatorPlay(1);
                    break;
                case ConstructionPrototypeId.MID_FACTORY:
                    //upperRenderer.sprite = SpriteLoader.factory[1];
                    animatorPlay(2);
                    break;
                case ConstructionPrototypeId.BIG_FACTORY:
                    //upperRenderer.sprite = SpriteLoader.factory[2];
                    animatorPlay(3);
                    //upperRenderer.color = new Color(0.75f, 0f, 0.5f);
                    break;
                case ConstructionPrototypeId.SMALL_TREE:
                    animatorPlay(0);
                    //upperRenderer.sprite = SpriteLoader.forest[0];
                    //StartCoroutine(nameof(treeGrow));
                    break;
                case ConstructionPrototypeId.LAKE:
                    animatorPlay(0);
                    //upperRenderer.sprite = SpriteLoader.lake;
                    //upperRenderer.color = new Color(0f, 0.5f, 1f);
                    break;
                case ConstructionPrototypeId.DESERT:
                    animatorPlay(0);
                    //upperRenderer.sprite = SpriteLoader.desert;
                    //upperRenderer.color = new Color(0.8f, 0.8f, 0f);
                    break;
                default:
                    break;
            }
        }
        private void animatorPlay(int id)
        {
            // TODO
        }

}



    public partial class MapController : Node,
        ILogicFrameListener,
        IGameAreaChangeListener,
        IConstructionCollectionListener
    {
        public static readonly HashSet<String> specialConstructionPrototypeIds = new HashSet<String>() {
            ConstructionPrototypeId.GOVERNMENT,
            ConstructionPrototypeId.ORGANIZATION
        };

        public TileMapLayer tileMapLayer;
        public Sprite2D focusCircle;       // 选中格位时显示的聚焦圈

        public DemoPlayScreen parent;      // 通过代码绑定
        private System.Collections.Generic.Dictionary<Vector2I, CellExtraData> constructionControlNodes = new();  // Cell即为一种ConstructionControlNode——控制一个设施的UI

        public override void _EnterTree()
        {
            parent = GodotUtils.FindParentOfType<DemoPlayScreen>(this);
            tileMapLayer = GodotUtils.FindFirstChildOfType<TileMapLayer>(this);
            focusCircle = GetNode<Sprite2D>("focusCircle");
        }

        public override void _Ready()
        {
            base._Ready();

            // 让光环绘制在地图之上
            focusCircle.ZIndex = tileMapLayer.ZIndex + 1;

            // 初始隐藏
            focusCircle.Visible = false;
        }

        public void onGameAreaChange(string last, string current)
        {
            // GameArea变化（包括加载后首次进入GameArea），说明设施集合可能有变，使用最新设施集合重建constructionControlNodes
            onConstructionCollectionChange();
        }

        public void onConstructionCollectionChange()
        {
            List<BaseConstruction> constructions = parent.game.idleGameplayExport.gameplayContext.constructionManager.getAreaControlableConstructionsOrEmpty(GameArea.AREA_SINGLE);
            constructions = filterConstructions(constructions);

            BackendLevelInfo backendLevelInfo = BackendLevelInfo.from(constructions, parent.game.idleGameplayExport.stageId);

            BuildBoard(backendLevelInfo);

            parent.game.frontend.log(this.getClass().getSimpleName(), "MapController change to: " + String.Join(", ",
                constructions.Select(construction => $"{construction.name}({construction.saveData.position.x},{construction.saveData.position.y})"))
            );
        }

        private List<BaseConstruction> filterConstructions(List<BaseConstruction> constructions)
        {
            // 过滤移除特殊设施；只管理普通设施
            return constructions
                .Where(it => !specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }


        // 将地图打印到屏幕上
        private void BuildBoard(BackendLevelInfo levelInfo)
        {

            // 清理旧有地图
            tileMapLayer.Clear();
            constructionControlNodes.Clear();

            // 初始化地图
            levelInfo.constructions.ForEach(construction =>
            {
                CellExtraData cell = new();
                cell.StateChangeTo(this, construction);
                Vector2I pos = new Vector2I(construction.saveData.position.x, construction.saveData.position.y);
                tileMapLayer.SetCell(pos, 0, cell.GetSourceId());
                constructionControlNodes.Add(pos, cell);
            });

        }

        void ILogicFrameListener.onLogicFrame()
        {
            // do nothing
        }

        private bool isDragging = false;
        private Vector2 pressPosition;
        private float dragThreshold = 10f; // 像素阈值，可根据需要调整

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent)
            {
                if (mouseEvent.ButtonIndex == MouseButton.Left)
                {
                    if (mouseEvent.Pressed)
                    {
                        // 鼠标按下，记录位置
                        pressPosition = mouseEvent.Position;
                        isDragging = false;
                    }
                    else
                    {
                        // 鼠标释放，判断是否拖拽
                        if (!isDragging)
                        {
                            // 获取鼠标在世界坐标中的位置
                            Vector2 mouseWorldPos = tileMapLayer.GetGlobalMousePosition();

                            // 获取格子左上角本地坐标
                            Vector2 mouseLocalPos = tileMapLayer.ToLocal(mouseWorldPos);

                            // 转换到 TileMap 的本地格子坐标
                            Vector2I gridPos = tileMapLayer.LocalToMap(mouseLocalPos);

                            if (tileMapLayer.GetCellSourceId(gridPos) != -1)
                            {
                                GD.Print($"点击了格子 {gridPos}");

                                // 这里可以执行你的逻辑
                                CellExtraData cell = constructionControlNodes.get(gridPos);
                                parent.boardManager.CallBoard(cell.construction);
                                // 获取格子左上角本地坐标
                                Vector2 cellLocalPos = tileMapLayer.MapToLocal(gridPos);
                                // 转成世界坐标
                                Vector2 forceWorldPos = tileMapLayer.ToGlobal(cellLocalPos);
                                this.focusAppear(forceWorldPos);
                            }
                            else
                            {
                                parent.boardManager.CloseBoard();
                                focusDisappear();
                            }
                        }
                    }
                }
            }
            else if (@event is InputEventMouseMotion motionEvent)
            {
                if (motionEvent.ButtonMask.HasFlag(MouseButtonMask.Left))
                {
                    // 判断移动距离是否超过阈值
                    if (motionEvent.Position.DistanceTo(pressPosition) > dragThreshold)
                    {
                        isDragging = true;
                    }
                }
            }

        }

        // 在指定位置打印聚焦圈
        public void focusAppear(Vector2 destPos)
        {
            focusCircle.GlobalPosition = destPos;
            focusCircle.Visible = (true);
        }

        // 在鼠标点击空位置时隐藏聚焦圈
        private void focusDisappear()
        {
            focusCircle.Visible = false;
            parent.boardManager.CloseBoard();
        }
    }
}
