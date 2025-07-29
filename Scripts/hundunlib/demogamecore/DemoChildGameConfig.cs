using Assets.Scripts.DemoGameCore.logic;
using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore
{
    public class DemoChildGameConfig : ChildGameConfig
    {
        public DemoChildGameConfig()
        {

            //        BuiltinConstructionsLoader builtinConstructionsLoader = new BuiltinConstructionsLoader(game);
            //        this.setConstructions(builtinConstructionsLoader.load());


            Dictionary<String, List<String>> areaControlableConstructionVMPrototypeIds = new Dictionary<String, List<String>>();
            areaControlableConstructionVMPrototypeIds.put(GameArea.AREA_SINGLE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.SMALL_TREE,
                     ConstructionPrototypeId.SMALL_FACTORY,
                     ConstructionPrototypeId.MID_FACTORY,
                     ConstructionPrototypeId.BIG_FACTORY,
                     ConstructionPrototypeId.DESERT,
                     ConstructionPrototypeId.DIRT,
                     ConstructionPrototypeId.RUBBISH,
                     ConstructionPrototypeId.LAKE,
                     ConstructionPrototypeId.GOVERNMENT,
                     ConstructionPrototypeId.ORGANIZATION
            ));


            this.areaControlableConstructionVMPrototypeIds = areaControlableConstructionVMPrototypeIds;

            Dictionary<String, List<String>> areaControlableConstructionPrototypeVMPrototypeIds = new Dictionary<String, List<String>>();
            areaControlableConstructionPrototypeVMPrototypeIds.put(GameArea.AREA_SINGLE, JavaFeatureForGwt.arraysAsList(
                     ConstructionPrototypeId.SMALL_TREE,
                     ConstructionPrototypeId.SMALL_FACTORY
            ));
            this.areaControlableConstructionPrototypeVMPrototypeIds = areaControlableConstructionPrototypeVMPrototypeIds;

            this.areaShowEntityByOwnAmountConstructionPrototypeIds = new Dictionary<String, List<String>>();

            Dictionary<String, List<String>> areaShowEntityByOwnAmountResourceIds = new Dictionary<String, List<String>>();
            this.areaShowEntityByOwnAmountResourceIds = (areaShowEntityByOwnAmountResourceIds);

            Dictionary<String, List<String>> areaShowEntityByChangeAmountResourceIds = new Dictionary<String, List<String>>();
            this.areaShowEntityByChangeAmountResourceIds = (areaShowEntityByChangeAmountResourceIds);

            Dictionary<String, String> screenIdToFilePathMap = JavaFeatureForGwt.mapOf(
                    "DemoMenuScreen", "audio/Loop-Menu.wav",
                    "DemoPlayScreen", "audio /relax.wav"
                );
            this.screenIdToFilePathMap = (screenIdToFilePathMap);

            List<String> achievementPrototypeIds = JavaFeatureForGwt.arraysAsList(
                    IdleForestAchievementId.SMALL_FACTORY_1,
                    IdleForestAchievementId.SMALL_FACTORY_2,
                    IdleForestAchievementId.SMALL_FACTORY_3,
                    IdleForestAchievementId.SMALL_FOREST_1,
                    IdleForestAchievementId.SMALL_FOREST_2,
                    IdleForestAchievementId.SMALL_FOREST_3,
                    IdleForestAchievementId.BIG_FACTORY_1,
                    IdleForestAchievementId.BIG_FACTORY_2,
                    IdleForestAchievementId.BIG_FACTORY_3,
                    IdleForestAchievementId.BIG_FACTORY_4,
                    IdleForestAchievementId.COIN_AMOUNT_2,
                    IdleForestAchievementId.COIN_AMOUNT_3,
                    IdleForestAchievementId.FOREST_CUT_2,
                    IdleForestAchievementId.FOREST_CUT_3,
                    IdleForestAchievementId.MID_FACTORY_1,
                    IdleForestAchievementId.MID_FACTORY_2,
                    IdleForestAchievementId.GOV_REWARD_1,
                    IdleForestAchievementId.GOV_PUNISH_1,
                    IdleForestAchievementId.FOREST_CUT_1,
                    IdleForestAchievementId.COIN_AMOUNT_1
                    );
            this.achievementPrototypeIds = (achievementPrototypeIds);
        }
    }
}
