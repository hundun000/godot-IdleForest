using Assets.Scripts.DemoGameCore.logic;
using Godot;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotIdleForest.Scripts.godotcore
{
    public partial class DemoPlayScreen : GodotBaseHundunScreen
    {
        public MapController mapController;
        public BoardManager boardManager;

        protected List<ILogicFrameListener> logicFrameListeners = new List<ILogicFrameListener>();
        protected List<IGameAreaChangeListener> gameAreaChangeListeners = new List<IGameAreaChangeListener>();



        public override void _EnterTree()
        {
            GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_EnterTree 开始");
            base._EnterTree();

            mapController = GodotUtils.FindFirstChildOfType<MapController>(this);
            boardManager = GodotUtils.FindFirstChildOfType<BoardManager>(this);
            // lazyInitUiRootContext() 不再需要(优先使用Godot的开发习惯)，由Godot引擎自行自上而下访问_EnterTree

        }

        public override void _Ready()
        { 
            base._Ready();
            lazyInitLogicContext();

            // start area
            setAreaAndNotifyChildren(GameArea.AREA_SINGLE);
        }

        protected void lazyInitLogicContext()
        {


            logicFrameListeners.Add(game.idleGameplayExport);
            logicFrameListeners.Add(mapController);


            gameAreaChangeListeners.Add(mapController);
            this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(mapController);
        }



        public void setAreaAndNotifyChildren(String current)
        {

            foreach (IGameAreaChangeListener gameAreaChangeListener in gameAreaChangeListeners)
            {
                gameAreaChangeListener.onGameAreaChange(current, current);
            }

        }
    }
}
