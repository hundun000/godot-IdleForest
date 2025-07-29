using System;
using System.Collections.Generic;

namespace hundun.idleshare.gamelib
{
    public class GameplaySaveData
    {
        public string stageId;
        public Dictionary<String, long> ownResoueces;
        public HashSet<String> unlockedResourceTypes;
        public Dictionary<String, ConstructionSaveData> constructionSaveDataMap;
        public HashSet<String> unlockedAchievementIds;

    }
}
