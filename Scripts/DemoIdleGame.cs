using Assets.Scripts.DemoGameCore;
using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.enginecorelib;

namespace GodotIdleForest.Scripts
{
	public class DemoIdleGame: BaseHundunGame<DemoIdleGame, RootSaveData>
	{
		public const int LOGIC_FRAME_PER_SECOND = 30;
		public IdleGameplayExport idleGameplayExport;
		public ChildGameConfig childGameConfig;

		public DemoIdleGame()
		{
			this.saveHandler = new DemoSaveHandler(frontend, new GodotSaveTool<RootSaveData>());
			this.childGameConfig = new DemoChildGameConfig();
		}

		override protected void createStage1()
		{
			base.createStage1();

			this.idleGameplayExport = new IdleGameplayExport(
					frontend,
					new DemoGameDictionary(),
					new DemoBuiltinConstructionsLoader(),
					new IdleForestAchievementLoader(),
					LOGIC_FRAME_PER_SECOND,
					childGameConfig
					);
			this.saveHandler.registerSubHandler(idleGameplayExport);
			saveHandler.systemSettingLoadOrStarter();
		}

		protected override void createStage2()
		{
			
		}

		protected override void createStage3()
		{
			
		}
	}


}
