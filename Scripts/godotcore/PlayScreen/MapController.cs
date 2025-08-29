using Assets.Scripts.DemoGameCore.logic;
using Godot;
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

    public partial class Cell : Node
    {
        public MapController mapController; // 父UI引用
        public BaseConstruction construction; // 后端数据引用


        public Vector2I? GetSourceId()
        {
            switch(construction.prototypeId)
            {
                case ConstructionPrototypeId.DIRT:
                    return new Vector2I(1, 0);
                case ConstructionPrototypeId.DESERT:
                    return new Vector2I(2, 0);
                case ConstructionPrototypeId.RUBBISH:
                    return new Vector2I(0, 0);
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



    public partial class MapController : TileMapLayer,
        ILogicFrameListener,
        IGameAreaChangeListener,
        IConstructionCollectionListener
    {
        public static readonly HashSet<String> specialConstructionPrototypeIds = new HashSet<String>() {
            ConstructionPrototypeId.GOVERNMENT,
            ConstructionPrototypeId.ORGANIZATION
        };

        public DemoPlayScreen parent;      // 通过代码绑定
        private bool firstCome;     // 判断是否是初次加载地图
        private Dictionary<GridPosition, Cell> constructionControlNodes = new();  // Cell即为一种ConstructionControlNode——控制一个设施的UI

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            firstCome = true;

        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
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

            parent.game.frontend.log(this.getClass().getSimpleName(), "MapController change to: " + String.Join(",",
                constructions.Select(construction => construction.name))
            );
        }

        private List<BaseConstruction> filterConstructions(List<BaseConstruction> constructions)
        {
            // 过滤移除特殊设施；只管理普通设施
            return constructions
                .Where(it => !specialConstructionPrototypeIds.Contains(it.saveData.prototypeId))
                .ToList();
        }

        // 换算：预设的二维坐标系坐标 -> 六边形坐标系在屏幕空间的映射
        private static Vector2I CalculatePosition(int gridX, int gridY)
        {
            float y = 0.75f * gridY;
            float x = Mathf.Sqrt(3) / 2 * (gridX - (Math.Abs(gridY) % 2) / 2.0f);
            return new Vector2I((int)x, (int)y);
        }


        // 将地图打印到屏幕上
        private void BuildBoard(BackendLevelInfo levelInfo)
        {
            // 判断是否是初次加载地图
            if (firstCome)
            {
                // 清理旧有地图
                this.Clear();
                constructionControlNodes.Clear();

                // 初始化地图
                levelInfo.constructions.ForEach(construction =>
                {

                    Cell cell = new();
                    cell.StateChangeTo(this, construction);
                    this.SetCell(new Vector2I(construction.saveData.position.x, construction.saveData.position.y), 0, cell.GetSourceId());
                    constructionControlNodes.Add(construction.saveData.position, cell);
                });

                firstCome = false;
            }
            else
            {
                levelInfo.constructions.ForEach(construction =>
                {
                    Cell cell = constructionControlNodes[construction.position];
                    if (cell.construction != construction)
                    {
                        cell.StateChangeTo(this, construction);
                        // TODO boardManager.boardCheckRecall(construction);
                    }
                });
            }
        }

        void ILogicFrameListener.onLogicFrame()
        {
            // do nothing
        }
    }
}
