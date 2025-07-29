using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class OwnResourceAchievement : AbstractAchievement
    {

        public List<ResourcePair> requireds;


        public OwnResourceAchievement(String id, string name, string description, string congratulationText,
            List<ResourcePair> requireds, Dictionary<String, long> awardResourceMap
            )
                : base(id, name, description, congratulationText, awardResourceMap)
        {
            this.requireds = requireds;
        }


        override public bool checkUnloack()
        {
            if (!gameplayContext.storageManager.isEnough(requireds))
            {
                return false;
            }

            return true;
        }
    }


    public class OwnConstructionAchievement : AbstractAchievement
    {

        public Dictionary<String, KeyValuePair<int, int>> requireds;


        public OwnConstructionAchievement(String id, string name, string description, string congratulationText,
            Dictionary<String, KeyValuePair<int, int>> requireds, Dictionary<String, long> awardResourceMap
            )
                : base(id, name, description, congratulationText, awardResourceMap)
        {
            this.requireds = requireds;
        }


        override public bool checkUnloack()
        {
            var allConstructions = gameplayContext.constructionManager.getConstructions();

            foreach (var requiredEntry in requireds)
            {
                int requiredAmount = requiredEntry.Value.Key;
                int requiredLevel = requiredEntry.Value.Value;
                bool matched = allConstructions
                        .Where(it => it.prototypeId.Equals(requiredEntry.Key))
                        .Where(it => it.saveData.level >= requiredLevel)
                        .Count() >= requiredAmount;
                if (!matched)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class IdleForestAchievementLoader : IBuiltinAchievementsLoader
    {
        private void quickAddOwnConstructionAchievement(Dictionary<String, AbstractAchievement> map, String id, Dictionary<String, List<String>> textMap, Dictionary<String, KeyValuePair<int, int>> requireds, Dictionary<String, long> awardResourceMap)
        {
            AbstractAchievement achievement = new OwnConstructionAchievement(
                    id,
                    textMap[id][0], textMap[id][1], textMap[id][2],
                    requireds,
                    awardResourceMap
                    );
            map.Add(id, achievement);
        }
        private void quickAddOwnResourceAchievement(Dictionary<String, AbstractAchievement> map, String id, Dictionary<String, List<String>> textMap, List<ResourcePair> requireds, Dictionary<String, long> awardResourceMap)
        {
            AbstractAchievement achievement = new OwnResourceAchievement(
                    id,
                    textMap[id][0], textMap[id][1], textMap[id][2],
                    requireds,
                    awardResourceMap
                    );
            map.Add(id, achievement);
        }


        public Dictionary<String, AbstractAchievement> getProviderMap(Language language)
        {
            Dictionary<String, List<String>> textMap = new Dictionary<String, List<String>>();
            switch (language)
            {
                case Language.CN:
                default:
                    textMap.Add(IdleForestAchievementId.SMALL_FACTORY_1, new List<string> {
                    "实业开端",
                    "建造1座初级工厂。",
                    "人们在小岛上建立了最初的定居点."
                    });

                    textMap.Add(IdleForestAchievementId.SMALL_FACTORY_2, new List<string> {
                    "蒸蒸日上",
                    "拥有3座初级工厂。",
                    "随着住民的增长，小岛的政府组织已经成型。"
                    });

                    textMap.Add(IdleForestAchievementId.SMALL_FACTORY_3, new List<string> {
                    "小有所成",
                    "拥有5座初级工厂。",
                    "随着工作坊规模的扩大，现有的经营方式也许已经过时了。"
                    });

                    textMap.Add(IdleForestAchievementId.MID_FACTORY_1, new List<string> {
                    "生产转型",
                    "拥有1座中工厂。",
                    "现在工厂不再招收临时工，失业者纷纷加入环保组织。"
                    });

                    textMap.Add(IdleForestAchievementId.MID_FACTORY_2, new List<string> {
                    "稳定发展",
                    "拥有3座中工厂。",
                    "工厂吸引了许多大学毕业生投递简历，HR表示，许多学生即使专业不对口也愿意进厂打工。"
                    });

                    textMap.Add(IdleForestAchievementId.BIG_FACTORY_1, new List<string> {
                    "更进一步",
                    "拥有1座大工厂。",
                    "小岛的工厂正式突破了岛外某大国的技术封锁，岛民争相庆贺。"
                    });

                    textMap.Add(IdleForestAchievementId.BIG_FACTORY_2, new List<string> {
                    "做大做强",
                    "拥有5座大工厂。",
                    "由于竞争激烈，现在大学生已经很难在工厂中得到实习岗位。"
                    });

                    textMap.Add(IdleForestAchievementId.BIG_FACTORY_3, new List<string> {
                    "工业革命",
                    "拥有7座中工厂。",
                    "现在每一座工厂都有自己的公寓、学校、医院和警局，甚至游乐场。"
                    });

                    textMap.Add(IdleForestAchievementId.BIG_FACTORY_4, new List<string> {
                    "未来已来",
                    "拥有10座大工厂。",
                    "在这片土地上，巨大的工业产能带来了日新月异的生活，工业的发展让环保的观念深入人心。"
                    });

                    textMap.Add(IdleForestAchievementId.SMALL_FOREST_1, new List<string> {
                    "环保之始",
                    "拥有3片树林。",
                    "森林茂盛的绿意给岛民们带来了生机和动力，有些岛民自发组织起了护林行动。"
                    });

                    textMap.Add(IdleForestAchievementId.SMALL_FOREST_2, new List<string> {
"环保卫士",
"拥有7片树林。",
"守林护林的职责交付到了专职守林人的手中，他们日夜巡逻防备森林火灾和盗猎。"
});

                    textMap.Add(IdleForestAchievementId.SMALL_FOREST_3, new List<string> {
"环保先锋",
"拥有20片树林。",
"森林已经覆盖了小岛的大片土地，环保组织向岛上的守林人组织颁发了“地球卫士奖”。"
}); textMap.Add(IdleForestAchievementId.GOV_REWARD_1, new List<string> {
"低碳生活",
"累计3次在低碳状态得到政府奖励。",
"“绿水青山就是金山银山。”"
});
                    textMap.Add(IdleForestAchievementId.GOV_PUNISH_1, new List<string> {
"碳碳得负",
"第一次在碳饱和状态得到政府惩罚。",
"“政府在环境整治上的坚决意志，是捍卫小岛生态的坚实护盾。      宽容仅此一次，今后即便金钱罚尽，亦不姑息。”"
});
                    textMap.Add(IdleForestAchievementId.PEO_PUNISH_1, new List<string> {
"替天行道",
"第一次触发环保主义者行动。",
"“先发展，后治理”，是很难行得通的。"
});
                    textMap.Add(IdleForestAchievementId.FOREST_CUT_1, new List<string> {
"长大成材",
"第一次砍伐树木。",
"“守林人第一次没有敲响警钟，因为脚下的森林注定将不复存在。”"
});
                    textMap.Add(IdleForestAchievementId.FOREST_CUT_2, new List<string> {
"木材产业",
"累计砍伐20次树木。",
"岛上的伐木工人现在聚集起来，形成了初具规模的产业。"
});
                    textMap.Add(IdleForestAchievementId.FOREST_CUT_3, new List<string> {
"木业大亨",
"累计砍伐200次树木。",
"一项调查显示，现今的伐木工人中，有许多曾是守林人中的一员。"
});
                    textMap.Add(IdleForestAchievementId.COIN_AMOUNT_1, new List<string> {
"金融之始",
"拥有5000个金币。",
"小岛上发行了自己的货币，原本捡贝壳为生的流浪汉现在加入了工人培训班。"
});
                    textMap.Add(IdleForestAchievementId.COIN_AMOUNT_2, new List<string> {
                    "富可敌国",
                    "拥有100000个金币。",
                    "岛上的生活日渐丰富，工厂员工现在不止享受五险一金，还能领取包年健身卡和高档住房。"
                    });
                    textMap.Add(IdleForestAchievementId.COIN_AMOUNT_3, new List<string> {
                    "为所欲为",
                    "拥有1000000个金币。",
                    "现在一座工厂的拆除会引起一些小国的经济崩溃，据悉，有些难民已经加入环保组织。"
});
                    break;
            }



            Dictionary<String, AbstractAchievement> map = new Dictionary<string, AbstractAchievement>();
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.SMALL_FACTORY_1,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_FACTORY, new KeyValuePair<int, int>(1, 1)
                        ),
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 100L
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.SMALL_FACTORY_2,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_FACTORY, new KeyValuePair<int, int>(3, 1)
                        ),
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 300L
                        )
            );
            quickAddOwnConstructionAchievement(
            map,
            IdleForestAchievementId.SMALL_FACTORY_3,
            textMap,
            JavaFeatureForGwt.mapOf(
                    ConstructionPrototypeId.SMALL_FACTORY, new KeyValuePair<int, int>(5, 1)
                    ),
            JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 500L
                    )
           );
            quickAddOwnConstructionAchievement(
      map,
      IdleForestAchievementId.MID_FACTORY_1,
      textMap,
      JavaFeatureForGwt.mapOf(
              ConstructionPrototypeId.MID_FACTORY, new KeyValuePair<int, int>(1, 1)
              ),
      JavaFeatureForGwt.mapOf(
              ResourceType.COIN, 500L
              )
     );
            quickAddOwnConstructionAchievement(
map,
IdleForestAchievementId.MID_FACTORY_2,
textMap,
JavaFeatureForGwt.mapOf(
  ConstructionPrototypeId.MID_FACTORY, new KeyValuePair<int, int>(3, 1)
  ),
JavaFeatureForGwt.mapOf(
  ResourceType.COIN, 500L
  )
);
            quickAddOwnConstructionAchievement(
map,
IdleForestAchievementId.BIG_FACTORY_1,
textMap,
JavaFeatureForGwt.mapOf(
  ConstructionPrototypeId.BIG_FACTORY, new KeyValuePair<int, int>(1, 1)
  ),
JavaFeatureForGwt.mapOf(
  ResourceType.COIN, 500L
  )
);
            quickAddOwnConstructionAchievement(
map,
IdleForestAchievementId.BIG_FACTORY_2,
textMap,
JavaFeatureForGwt.mapOf(
  ConstructionPrototypeId.BIG_FACTORY, new KeyValuePair<int, int>(5, 1)
  ),
JavaFeatureForGwt.mapOf(
  ResourceType.COIN, 500L
  )
);
            quickAddOwnConstructionAchievement(
map,
IdleForestAchievementId.BIG_FACTORY_3,
textMap,
JavaFeatureForGwt.mapOf(
  ConstructionPrototypeId.MID_FACTORY, new KeyValuePair<int, int>(7, 1)
  ),
JavaFeatureForGwt.mapOf(
  ResourceType.COIN, 500L
  )
);
            quickAddOwnConstructionAchievement(
map,
IdleForestAchievementId.BIG_FACTORY_4,
textMap,
JavaFeatureForGwt.mapOf(
  ConstructionPrototypeId.BIG_FACTORY, new KeyValuePair<int, int>(10, 1)
  ),
JavaFeatureForGwt.mapOf(
  ResourceType.COIN, 500L
  )
);
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.SMALL_FOREST_1,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_TREE, new KeyValuePair<int, int>(3, 1)
                        ),
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 100L
                        )
            );
            quickAddOwnConstructionAchievement(
                map,
                IdleForestAchievementId.SMALL_FOREST_2,
                textMap,
                JavaFeatureForGwt.mapOf(
                        ConstructionPrototypeId.SMALL_TREE, new KeyValuePair<int, int>(7, 1)
                        ),
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 300L
                        )
            );
            quickAddOwnConstructionAchievement(
    map,
    IdleForestAchievementId.SMALL_FOREST_3,
    textMap,
    JavaFeatureForGwt.mapOf(
            ConstructionPrototypeId.SMALL_TREE, new KeyValuePair<int, int>(20, 1)
            ),
    JavaFeatureForGwt.mapOf(
            ResourceType.COIN, 300L
            )
);
            quickAddOwnResourceAchievement(
                map,
                IdleForestAchievementId.GOV_REWARD_1,
                textMap,
                new List<ResourcePair>() {
                    new ResourcePair(ResourceType.FLAG_GOV_REWARD, 3L)
                },
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 300L
                        )
            );
            quickAddOwnResourceAchievement(
                map,
                IdleForestAchievementId.GOV_PUNISH_1,
                textMap,
                new List<ResourcePair>() {
                    new ResourcePair(ResourceType.FLAG_GOV_PUNISH, 1L)
                },
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 1000L
                        )
            );
            quickAddOwnResourceAchievement(
                map,
                IdleForestAchievementId.FOREST_CUT_1,
                textMap,
                new List<ResourcePair>() {
                    new ResourcePair(ResourceType.FLAG_CUT_FOREST, 1L)
                },
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 100L
                        )
            );
            quickAddOwnResourceAchievement(
    map,
    IdleForestAchievementId.FOREST_CUT_2,
    textMap,
    new List<ResourcePair>() {
                    new ResourcePair(ResourceType.FLAG_CUT_FOREST, 20L)
    },
    JavaFeatureForGwt.mapOf(
            ResourceType.COIN, 100L
            )
);
            quickAddOwnResourceAchievement(
    map,
    IdleForestAchievementId.FOREST_CUT_3,
    textMap,
    new List<ResourcePair>() {
                    new ResourcePair(ResourceType.FLAG_CUT_FOREST, 200L)
    },
    JavaFeatureForGwt.mapOf(
            ResourceType.COIN, 100L
            )
);
            quickAddOwnResourceAchievement(
                map,
                IdleForestAchievementId.COIN_AMOUNT_1,
                textMap,
                new List<ResourcePair>() {
                    new ResourcePair(ResourceType.COIN, 5000L)
                },
                JavaFeatureForGwt.mapOf(
                        ResourceType.COIN, 100L
                        )
            );
            quickAddOwnResourceAchievement(
          map,
          IdleForestAchievementId.COIN_AMOUNT_2,
          textMap,
          new List<ResourcePair>() {
                    new ResourcePair(ResourceType.COIN, 100000L)
          },
          JavaFeatureForGwt.mapOf(
                  ResourceType.COIN, 100L
                  )
      );
            quickAddOwnResourceAchievement(
          map,
          IdleForestAchievementId.COIN_AMOUNT_3,
          textMap,
          new List<ResourcePair>() {
                    new ResourcePair(ResourceType.COIN, 1000000L)
          },
          JavaFeatureForGwt.mapOf(
                  ResourceType.COIN, 100L
                  )
      );
            return map;
        }
    }
}
