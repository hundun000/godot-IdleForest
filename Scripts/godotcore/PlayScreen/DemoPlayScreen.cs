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
        public StatusBarController statusBarController;

        protected List<ILogicFrameListener> logicFrameListeners = new List<ILogicFrameListener>();
        protected List<IGameAreaChangeListener> gameAreaChangeListeners = new List<IGameAreaChangeListener>();



        public override void _EnterTree()
        {
            GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_EnterTree 开始");
            base._EnterTree();

            mapController = GodotUtils.FindFirstChildOfType<MapController>(this);
            boardManager = GodotUtils.FindFirstChildOfType<BoardManager>(this);
            statusBarController = GodotUtils.FindFirstChildOfType<StatusBarController>(this);
            // lazyInitUiRootContext() 不再需要(优先使用Godot的开发习惯)，由Godot引擎自行自上而下访问_EnterTree

        }

        public override void _Ready()
        { 
            base._Ready();
            this.logicFrameHelper = new LogicFrameHelper(DemoIdleGame.LOGIC_FRAME_PER_SECOND);

            lazyInitLogicContext();

            // start area
            setAreaAndNotifyChildren(GameArea.AREA_SINGLE);
        }

        protected void lazyInitLogicContext()
        {


            logicFrameListeners.Add(game.idleGameplayExport);
            logicFrameListeners.Add(statusBarController);
            logicFrameListeners.Add(mapController);

            gameAreaChangeListeners.Add(mapController);
            gameAreaChangeListeners.Add(statusBarController);
            this.game.idleGameplayExport.gameplayContext.eventManager.registerListener(mapController);
        }



        public void setAreaAndNotifyChildren(String current)
        {

            foreach (IGameAreaChangeListener gameAreaChangeListener in gameAreaChangeListeners)
            {
                gameAreaChangeListener.onGameAreaChange(current, current);
            }

        }

        protected override void onLogicFrame()
        {
            base.onLogicFrame();


            foreach (ILogicFrameListener logicFrameListener in logicFrameListeners)
            {
                logicFrameListener.onLogicFrame();
            }

            if (logicFrameHelper.clockCount % logicFrameHelper.secondToFrameNum(10) == 0)
            {
                game.saveHandler.gameSaveCurrent();
            }
        }
    }
}
