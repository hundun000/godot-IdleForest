using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class DemoSaveHandler : PairChildrenSaveHandler<RootSaveData, SystemSettingSaveData, GameplaySaveData>
    {

        public DemoSaveHandler(IFrontend frontEnd, ISaveTool<RootSaveData> saveTool) : base(frontEnd, Factory.INSTANCE, saveTool)
        {


        }

        private ConstructionSaveData quickSmallTree(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.SMALL_TREE)
                                    .level(1)
                                    .workingLevel(1)
                                    .proficiency(50)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickDirt(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DIRT)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickRubbish(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.RUBBISH)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickLake(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.LAKE)
                                    .proficiency(50)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private ConstructionSaveData quickDesert(int x, int y)
        {
            return ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.DESERT)
                                    .proficiency(0)
                                    .position(new GridPosition(x, y))
                                    .build();
        }

        private GameplaySaveData quickGameplaySaveData(Dictionary<KeyValuePair<int, int>, ConstructionSaveData> posMap, string stageId)
        {
            posMap.Add(new(100, 100), ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.GOVERNMENT)
                                    .proficiency(10)
                                    .position(new GridPosition(100, 100))
                                    .build());
            posMap.Add(new(100, 101), ConstructionSaveData.builder()
                                    .prototypeId(ConstructionPrototypeId.ORGANIZATION)
                                    .proficiency(0)
                                    .position(new GridPosition(100, 101))
                                    .build());

            var gameplaySaveData = new GameplaySaveData();
            gameplaySaveData.stageId = stageId;
            gameplaySaveData.constructionSaveDataMap = posMap.Values
                .ToDictionary(
                    it => it.prototypeId + System.Guid.NewGuid().ToString(),
                    it => it
                );
            gameplaySaveData.ownResoueces = (new Dictionary<String, long>());
            gameplaySaveData.ownResoueces.Add(ResourceType.COIN, 500);
            gameplaySaveData.ownResoueces.Add(ResourceType.WOOD, 50);
            gameplaySaveData.ownResoueces.Add(ResourceType.CARBON, 4000);
            gameplaySaveData.unlockedResourceTypes = (new HashSet<String>());
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.COIN);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.WOOD);
            gameplaySaveData.unlockedResourceTypes.Add(ResourceType.CARBON);
            gameplaySaveData.unlockedAchievementIds = (new HashSet<String>());
            return gameplaySaveData;
        }

        override protected List<RootSaveData> genereateStarterRootSaveData()
        {


            Dictionary<KeyValuePair<int, int>, ConstructionSaveData> posMap = new();

            // 所有 DIRT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(0, 0), new(1, 0), new(2, 0), new(4, 0), new(-1, 0), new(-3, 0) ,new(-2,1),new(-1,1),new(3,1),
                new(4,1),new(0,2),new(1,2),new(-2,-1),new(-1,-1),new(3,-1),new(4,-1),new(0,-2),new(1,-2)
            }).ForEach(it =>
                posMap.Add(it, quickDirt(it.Key, it.Value))
                );

            // 所有 LAKE 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(1, 1), new(1, -1)
            }).ForEach(it =>
                posMap.Add(it, quickLake(it.Key, it.Value))
                );

            // 所有 DESERT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(-4,0),new(-2,0),new(3,0),new(5,0),new(-2,2),new(3,2),new(1,3),new(-2,-2),new(3,-2),new(1,-3)
            }).ForEach(it =>
                posMap.Add(it, quickDesert(it.Key, it.Value))
                );

            // 所有 RUBBISH 的坐标
            (new List<KeyValuePair<int, int>>() {
               new(-1, 4), new(0, 4),new(1,4),new(2,4),new(-3,3),new(-2,3),new(-1,3),new(0,3),new(2,3),new(3,3),new(4,3),
                new(5,3),new(-4,2),new(-3,2),new(-1,2),new(2,2),new(4,2),new(5,2),new(-4,1),new(-3,1),new(0,1),new(2,1),
                new(5,1),new(6,1),new(-5,0),new(6,0),new(-4,-1),new(-3,-1),new(0,-1),new(2,-1),new(5,-1),new(6,-1),new(-4,-2),
                new(-3,-2),new(-1,-2),new(2,-2),new(4,-2),new(5,-2),new(-3,-3),new(-2,-3),new(-1,-3),new(0,-3),new(2,-3),new(3,-3),
                new(4,-3),new(5,-3),new(-1,-4),new(0,-4),new(1,-4),new(2,-4)
            }).ForEach(it =>
                posMap.Add(it, quickRubbish(it.Key, it.Value))
                );

            //第二关
            Dictionary<KeyValuePair<int, int>, ConstructionSaveData> posMap2 = new();

            // 所有 DIRT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(0, 2),new(0,1),new(1,1),new(-1,0),new(1,0),new(-1,-1),new(0,-1),new(1,-1),new(2,-1),new(-4,3),new(-3,3),new(4,3),new(5,3)
            }).ForEach(it =>
                posMap2.Add(it, quickDirt(it.Key, it.Value))
                );

            // 所有 RUBBISH 的坐标
            (new List<KeyValuePair<int, int>>() {
               new(-5,4),new(-4,4),new(-3,4),new(0,4),new(3,4),new(4,4),new(5,4),new(-5,3),new(-2,3),
               new(0,3),new(1,3),new(3,3),new(6,3),new(-5,2),new(-3,2),new(-1,2),new(1,2),new(3,2),
               new(5,2),new(-4,1),new(-3,1),new(-1,1),new(2,1),new(4,1),new(5,1),new(-2,0),new(2,0),
               new(-2,-1),new(3,-1),new(-3,-2),new(-1,-2),new(1,-2),new(3,-2),new(-3,-3),new(-2,-3),
               new(-1,-3),new(0,-3),new(1,-3),new(2,-3),new(3,-3),new(4,-3)
            }).ForEach(it =>
                posMap2.Add(it, quickRubbish(it.Key, it.Value))
                 );

            // 所有 LAKE 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(-4, 2),new(0,0),new(4,2)
            }).ForEach(it =>
                posMap2.Add(it, quickLake(it.Key, it.Value))
                );

            // 所有 DESERT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(-2,-2),new(0,-2),new(2,-2)
            }).ForEach(it =>
                posMap2.Add(it, quickDesert(it.Key, it.Value))
                );

            //更改后的第三关  相机中心(3,1)
            Dictionary<KeyValuePair<int, int>, ConstructionSaveData> posMap3 = new();

            // 所有 DIRT 的坐标
            (new List<KeyValuePair<int, int>>() {
             new(3,3),new(4,3),new(2,2),new(4,2),new(0,1),new(1,1),new(2,1),new(3,1),new(4,1),new(-1,0),
             new(1,0),new(3,0),new(0,-1),new(1,-1),new(2,-1),new(3,-1),new(4,-1),new(2,-2),new(4,-2),
             new(3,-3),new(4,-3)
            }).ForEach(it =>
                posMap3.Add(it, quickDirt(it.Key, it.Value))
                );

            // 所有 RUBBISH 的坐标
            (new List<KeyValuePair<int, int>>() {
               new(-1,4),new(0,4),new(1,4),new(-4,3),new(-3,3),new(-1,3),new(0,3),new(1,3),new(2,3),
               new(5,3),new(-5,2),new(-4,2),new(-3,2),new(-2,2),new(-1,2),new(1,2),new(5,2),new(-5,1),
               new(-3,1),new(-2,1),new(6,1),new(-4,0),new(-2,0),new(-5,-1),new(-3,-1),new(-2,-1),new(6,-1),
               new(-5,-2),new(-4,-2),new(-3,-2),new(-2,-2),new(-1,-2),new(1,-2),new(5,-2),new(-4,-3),
               new(-3,-3),new(-1,-3),new(0,-3),new(1,-3),new(2,-3),new(5,-3),new(-1,-4),new(0,-4),new(1,-4)
            }).ForEach(it =>
                posMap3.Add(it, quickRubbish(it.Key, it.Value))
                 );

            // 所有 LAKE 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(0,0),new(2,0),new(3,-2),new(3,2)

            }).ForEach(it =>
                posMap3.Add(it, quickLake(it.Key, it.Value))
                );

            // 所有 DESERT 的坐标
            (new List<KeyValuePair<int, int>>() {
                new(-3,0),new(-1,1),new(-1,-1),new(0,2),new(0,-2)
            }).ForEach(it =>
                posMap3.Add(it, quickDesert(it.Key, it.Value))
                );

            var systemSettingSaveData = new SystemSettingSaveData();
            systemSettingSaveData.language = Language.EN;
            return new List<RootSaveData>() {
                new RootSaveData(null, systemSettingSaveData),
                new RootSaveData(quickGameplaySaveData(posMap, StageId.STAGE_1), null),
                new RootSaveData(quickGameplaySaveData(posMap2, StageId.STAGE_2), null),
                new RootSaveData(quickGameplaySaveData(posMap3, StageId.STAGE_3), null)
            };

        }
    }
}
