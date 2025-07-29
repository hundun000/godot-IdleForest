using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class SmallFactoryPrototype : AbstractConstructionPrototype
    {
        public static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .button("Upgrade")
            .output("Consume", "Produce")
            .upgrade("Upgrade cost", "(max)", DescriptionPackageFactory.ONLY_LEVEL_IMP)
            .destroy("Destroy", null, null)
            .transform("Transform", "Transform cost", "Can be transformed")
            .proficiency(DescriptionPackageFactory.EN_PROFICIENCY_IMP)
            .build();


        public static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .button("升级")
            .output("自动消耗", "自动产出")
            .upgrade("升级费用", "(已达到最大等级)", DescriptionPackageFactory.CN_ONLY_LEVEL_IMP)
            .destroy("摧毁", "摧毁产出", "摧毁费用")
            .transform("转职", "转职费用", "可以转职")
            .proficiency(DescriptionPackageFactory.CN_PROFICIENCY_IMP)
            .build();

        public SmallFactoryPrototype(Language language) : base(ConstructionPrototypeId.SMALL_FACTORY, language,
            DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.WOOD, 30,
                    ResourceType.COIN, 200
                    ))
            )
        {
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeAuto(prototypeId, id, position, descriptionPackage,
                20, 1f / 6f
                );

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.allowAnyProficiencyDestory = true;

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 30,
                    ResourceType.CARBON, 200
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 1000,
                    ResourceType.WOOD, 100
                    )));
            construction.upgradeComponent.transformCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 5000,
                    ResourceType.WOOD, 500
                    )));
            construction.upgradeComponent.transformConstructionPrototypeId = ConstructionPrototypeId.MID_FACTORY;

            return construction;
        }
    }
}
