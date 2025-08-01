using Godot;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;
using hundun.unitygame.gamelib;


namespace GodotIdleForest.Scripts.godotcore
{
    public abstract partial class GodotBaseHundunScreen: Node
    {
        public DemoIdleGame game;

        public LogicFrameHelper logicFrameHelper;

        public override void _EnterTree()
        {
            GD.Print(JavaFeatureExtension.getClass(this).getSimpleName() + "_EnterTree开始");
            this.game = GameContainer.Game;
        }

        public override void _Process(double delta)
        {
            if (logicFrameHelper != null)
            {
                bool isLogicFrame = logicFrameHelper.logicFrameCheck((float)delta);
                if (isLogicFrame)
                {
                    onLogicFrame();
                }
            }
        }

        virtual protected void onLogicFrame()
        {
            // base-class do nothing
        }

    }

}
