using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class RootSaveData
    {
        public GameplaySaveData gameplaySave;
        public SystemSettingSaveData systemSettingSaveData;

        public RootSaveData(GameplaySaveData gameplaySave, SystemSettingSaveData systemSettingSaveData)
        {
            this.gameplaySave = gameplaySave;
            this.systemSettingSaveData = systemSettingSaveData;
        }
    }

    public class Factory : IRootSaveExtension<RootSaveData, SystemSettingSaveData, GameplaySaveData>
    {

        public static readonly Factory INSTANCE = new Factory();

        public SystemSettingSaveData getSystemSave(RootSaveData rootSaveData)
        {
            return rootSaveData.systemSettingSaveData;
        }

        public GameplaySaveData getGameplaySave(RootSaveData rootSaveData)
        {
            return rootSaveData.gameplaySave;
        }

        public RootSaveData newRootSave(GameplaySaveData gameplaySave, SystemSettingSaveData systemSettingSaveData)
        {
            return new RootSaveData(gameplaySave, systemSettingSaveData);
        }

        public GameplaySaveData newGameplaySave()
        {
            return new GameplaySaveData();
        }

        public SystemSettingSaveData newSystemSave()
        {
            return new SystemSettingSaveData();
        }

    }
}
