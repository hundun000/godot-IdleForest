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
        [Export]
        public MapController mapController;

        protected List<ILogicFrameListener> logicFrameListeners = new List<ILogicFrameListener>();
        protected List<IGameAreaChangeListener> gameAreaChangeListeners = new List<IGameAreaChangeListener>();


        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_Ready开始");
            base._Ready();

            lazyInitLogicContext();

            // start area
            setAreaAndNotifyChildren(GameArea.AREA_SINGLE);


        }

        protected void lazyInitLogicContext()
        {
            mapController.parent = this;


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
