using Godot;
using hundun.unitygame.enginecorelib;
using hundun.unitygame.gamelib;


namespace GodotIdleForest.Scripts.godotcore
{
    public abstract partial class GodotBaseHundunScreen: Node
    {
        public DemoIdleGame game;

        public LogicFrameHelper logicFrameHelper;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            this.game = GameContainer.Instance;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
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
