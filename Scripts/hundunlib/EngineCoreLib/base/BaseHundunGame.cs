using GodotIdleForest.Scripts.godotcore.adapter;
using hundun.unitygame.gamelib;


namespace hundun.unitygame.enginecorelib
{
	public abstract class BaseHundunGame<T_GAME, T_SAVE> where T_GAME : BaseHundunGame<T_GAME, T_SAVE>
	{



		// ------ init in createStage1(), or keep null ------
		public AbstractSaveHandler<T_SAVE> saveHandler { get; protected set; }
		public GodotFrontend frontend { get; private set; }

		public BaseHundunGame()
		{
			this.frontend = new GodotFrontend();
		}

		/**
		 * 只依赖Gdx static的成员
		 */
		protected virtual void createStage1()
		{
			this.saveHandler.lazyInitOnGameCreate();
		}
		/**
		 * 只依赖Stage1的成员
		 */
		protected abstract void createStage2();
		/**
		 * 自由依赖
		 */
		protected abstract void createStage3();


		public void create()
		{
			createStage1();
			createStage2();
			createStage3();
		}


		// ====== ====== ======



		virtual public void dispose()
		{

		}

	}

}
