using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallTreePrototype : AbstractConstructionPrototype
    {
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .button("Upgrade")
            .output("Consume", "Produce")
            .upgrade("Upgrade cost", "(max)", DescriptionPackageFactory.ONLY_LEVEL_IMP)
            .destroy("Destroy", null, null)
            .proficiency(DescriptionPackageFactory.EN_PROFICIENCY_IMP)
            .build();
        public static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .button("升级")
            .output("自动消耗", "自动产出")
            .upgrade("升级费用", "(已达到最大等级)", DescriptionPackageFactory.CN_ONLY_LEVEL_IMP)
            .destroy("砍伐", null, null)
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public SmallTreePrototype(Language language) : base(ConstructionPrototypeId.SMALL_TREE, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 100
                    ))
            )
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = SmallTreePrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = SmallTreePrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeAuto(prototypeId, id, position, descriptionPackage,
                0, 0f);
            construction.existenceComponent.allowAnyProficiencyDestory = false;

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 100,
                    ResourceType.FLAG_CUT_FOREST, 1
                    )));

            construction.outputComponent.outputCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.CARBON, 400
                    )));
            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 2
                    )));

            return construction;
        }
    }
}
